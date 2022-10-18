using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
	public GameObject controller;
	public GameObject AllPieces;
	//public GameObject MovePlates //test if this solves getting rid of some returns
	
	
	public float AIDifficulty;
	public bool IsCampaign;
	public int StuckCounter=0;
	
	private List<GameObject> AIPieces = new List<GameObject>();//all of the pieces of the AI
	private List<GameObject> EnemyPieces = new List<GameObject>();//all of the pieces of the enemy of the AI
	private List<GameObject> Moves = new List<GameObject>();//all of the moves of a certain piece of the AI, only playable ones
	private List<GameObject> AIPiecesThatCanAttack = new List<GameObject>();//all of the pieces that can attack
	private List<GameObject> EnemyPiecesThatCanAttack = new List<GameObject>();//all of the enemy pieces that can attack
    private List<GameObject> EnemyPiecesThatCanBeAttacked = new List<GameObject>();//all of the enemy pieces that can be attacked
    private List<GameObject> AIPiecesThatCanBeAttacked = new List<GameObject>();//all of the AI pieces that can be attacked	by the enemy	
	private List<int> DangerSquares = new List<int>();//coords of squares endangered by enemy pieces
	private List<GameObject> SafeSquares = new List<GameObject>();//coords of squares endangered by enemy pieces
	
	public int TargetNumber;
	public int KingNumber;
	public bool isKingInDanger;
	public bool KillKingAttacker;
	public bool currentAttackMoveDetected;
	
	public GameObject CurrentTarget;
	public GameObject KingAttacker;
	
	public string currentAIPlayer;	
	
	
    // Start is called before the first frame update
    void Awake()
    {	
		AIDifficulty = PlayerPrefs.GetFloat("AIDifficulty"); 
        if (PlayerPrefs.GetString("GameType")=="Campaign"){IsCampaign=true;}
        else{IsCampaign=false;}		
    }
 	
	public void AIMakeAPlay(string AIPlayer)
	{
		currentAIPlayer = AIPlayer;
		switch(AIDifficulty)
		{
			case 1: StartCoroutine(AIRandomMoving()); break;   //moves randomly
			case 2: StartCoroutine(AIRandomAttacking()); break;  //attacks every time it cans
			case 3: StartCoroutine(AIWeighedAttacking()); break;  //attacks the most valuable piece every time it can
			case 4: StartCoroutine(AINoobKid()); break;  //now it also checks if his king is in danger, moves it or kills the piece			
		}		
	}
	
	//CHECKING ALGORITHMS
	
    void CheckCurrentPieces() //this checks the current pieces and adds them to a list
	{
		AIPieces.Clear();
	    AIPieces.TrimExcess();		
		GameObject[] AllPieces = GameObject.FindGameObjectsWithTag("Chessman");
		for (int i=0; i< AllPieces.Length; i++)
		{
			if (AllPieces[i].GetComponent<Chessman>().player == currentAIPlayer)
			{
				AIPieces.Add (AllPieces[i]);				
		    }		
		}		
	}
	
	void CheckEnemyPieces() //this checks all of the current pieces of the enemy
	{
		EnemyPieces.Clear();
	    EnemyPieces.TrimExcess();		
		GameObject[] AllPieces = GameObject.FindGameObjectsWithTag("Chessman");
		for (int i=0; i< AllPieces.Length; i++)
		{
			if (AllPieces[i].GetComponent<Chessman>().player != currentAIPlayer)
			{
				EnemyPieces.Add (AllPieces[i]);				
		    }		
		}	
	}
	
	void CheckMoves() //this checks the current possible moves and adds them to a list, only playable ones
	{		
		currentAttackMoveDetected=false;
		Moves.Clear();
	    Moves.TrimExcess();		
		GameObject[] MovePlates = GameObject.FindGameObjectsWithTag("MovePlate");
		for (int i=0; i< MovePlates.Length; i++)
		{
			if (MovePlates[i].GetComponent<MovePlate>().currentAttack){currentAttackMoveDetected=true;}
			
			if (MovePlates[i].GetComponent<MovePlate>().playable && !MovePlates[i].GetComponent<MovePlate>().currentAttack)
			{				
			Moves.Add(MovePlates[i]);
			}			
		}
        //Debug.Log(Moves.Count+" Moves found ");
		if (IsCampaign==true&&Moves.Count!=0){StuckCounter=0;}// this resets the counter if it finds a move
		if (IsCampaign==true&&Moves.Count==0){StuckCounter++;}
		if (StuckCounter==100){controller.GetComponent<Game>().Winner("White");Debug.Log("Win by Stuck");}//this gives the player the win in campaign if the AI cant play
        //the counter has to be reset because at every moment there are pieces checked with 0 moves
        //the counter will only reach an abnormal number if there is absolutely no move, which means AI will default to RandomMoving and will be caught in a loop
        //trying to find something to move		
	}
	
	IEnumerator CheckDangerSquares() // checks all of the squares endangered by enemy pieces, has to be adjusted for pawns
	{
        DangerSquares.Clear();
	    DangerSquares.TrimExcess();		
		CheckEnemyPieces();
		for (int i=0; i< EnemyPieces.Count; i++)
		{
			EnemyPieces[i].GetComponent<Chessman>().OnMouseUp();
			yield return null;
			CheckMoves();			
			for (int j=0; j< Moves.Count; j++)
		    {
			    int coord = 10*Moves[j].GetComponent<MovePlate>().matrixX + Moves[j].GetComponent<MovePlate>().matrixY;
                DangerSquares.Add(coord);
                //Debug.Log(coord);				
		    }
		}
	}

    IEnumerator CheckPiecesThatCanAttack()//this is only used in AIRandomMoving
	{		
	    AIPiecesThatCanAttack.Clear();
		AIPiecesThatCanAttack.TrimExcess();
		CheckCurrentPieces();			 
		for (int j=0 ; j < AIPieces.Count; j++)
		{													
			AIPieces[j].GetComponent<Chessman>().OnMouseUp();
			yield return null;                    					
        	CheckMoves();  //CheckMoves doesn't register a CurrentAttack moveplate
            yield return null;	
			if (Moves.Count!=0)
			{
				for (int k=0; k<Moves.Count; k++)
				{					    
				if (Moves[k].GetComponent<MovePlate>().attack)
					{						    							
						AIPiecesThatCanAttack.Add (AIPieces[j]);
						//Debug.Log(AIPieces[j]+" can attack "+Time.time);
                        k=Moves.Count; //so it only adds the piece once
						yield return null;
					}							
				}                  
			}					
        }		
		//Debug.Log(AIPiecesThatCanAttack.Count+" pieces can attack "+Time.time);
        //yield return null;		
	}
	
	IEnumerator CheckEnemyPiecesThatCanAttack()//this is never used as CheckAIPiecesThatCanBeAttacked contains it 
	{		
	    EnemyPiecesThatCanAttack.Clear();
		EnemyPiecesThatCanAttack.TrimExcess();
		CheckEnemyPieces();			 
		for (int j=0 ; j < EnemyPieces.Count; j++)
		{													
			EnemyPieces[j].GetComponent<Chessman>().OnMouseUp();
			yield return null;                    					
        	CheckMoves();
            yield return null;	
			if (Moves.Count!=0)
			{
				for (int k=0; k<Moves.Count; k++)
				{					    
				if (Moves[k].GetComponent<MovePlate>().attack)
					{						    							
						EnemyPiecesThatCanAttack.Add (EnemyPieces[j]);
                        k=Moves.Count; //so it only adds the piece once	
						//Debug.Log(EnemyPieces[j]+" "+Time.time);
						//yield return null;
					}							
				}                  
			}					
        }        		
	}
	
	
	IEnumerator CheckEnemyPiecesThatCanBeAttacked()//contains check current pieces and check aipieces that can attack
	{		
	    AIPiecesThatCanAttack.Clear();
		AIPiecesThatCanAttack.TrimExcess();
		EnemyPiecesThatCanBeAttacked.Clear();
		EnemyPiecesThatCanBeAttacked.TrimExcess();
		CheckCurrentPieces();			 
		for (int j=0 ; j < AIPieces.Count; j++)
		{													
			AIPieces[j].GetComponent<Chessman>().OnMouseUp();
			yield return null;                    					
        	CheckMoves();
            yield return null;	
			if (Moves.Count!=0)
			{
				for (int k=0; k<Moves.Count; k++)
				{					    
					if (Moves[k].GetComponent<MovePlate>().attack)
					{						    							
						AIPiecesThatCanAttack.Add (AIPieces[j]);
						//Debug.Log(AIPieces[j]);
						Moves[k].GetComponent<MovePlate>().OnMouseUp();
						EnemyPiecesThatCanBeAttacked.Add(Moves[k].GetComponent<MovePlate>().cp);
						//Debug.Log(Moves[k].GetComponent<MovePlate>().cp);                        	
					}							
				}                  
			}					
        }		
	}
	
	IEnumerator CheckAIPiecesThatCanBeAttacked()//contains check enemy pieces and check enemypieces that can attack
	{		
	    EnemyPiecesThatCanAttack.Clear();
		EnemyPiecesThatCanAttack.TrimExcess();
		AIPiecesThatCanBeAttacked.Clear();
		AIPiecesThatCanBeAttacked.TrimExcess();
		CheckEnemyPieces();			 
		for (int j=0 ; j < EnemyPieces.Count; j++)
		{													
			EnemyPieces[j].GetComponent<Chessman>().OnMouseUp();
			yield return null;                    					
        	CheckMoves();
            yield return null;	
			if (Moves.Count!=0)
			{
				for (int k=0; k<Moves.Count; k++)
				{					    
					if (Moves[k].GetComponent<MovePlate>().attack)
					{						    							
						EnemyPiecesThatCanAttack.Add (EnemyPieces[j]);
						//Debug.Log(EnemyPieces[j]+" can attack "+Time.time);
						Moves[k].GetComponent<MovePlate>().OnMouseUp();
						AIPiecesThatCanBeAttacked.Add(Moves[k].GetComponent<MovePlate>().cp);
						//Debug.Log(Moves[k].GetComponent<MovePlate>().cp+" "+Time.time);                        	
					}							
				}                  
			}
            if (currentAttackMoveDetected) //this adds to both lists if a current attack is being made
			{
				EnemyPiecesThatCanAttack.Add (EnemyPieces[j]);
				AIPiecesThatCanBeAttacked.Add (EnemyPieces[j].GetComponent<Chessman>().CurrentTargetPiece);   
			}				
        }		
	}
	
	
    //-------------------------------------------------EVALUATING ALGORITHMS -------------------------------------------------------
		
	public void KingSituation()// needs AIPiecesThatCanBeAttacked and EnemyPiecesThatCanBeAttacked to be called
    {
		KillKingAttacker=false;
		isKingInDanger=false;
		for (int a=0; a<AIPiecesThatCanBeAttacked.Count; a++)
		{
			if(AIPiecesThatCanBeAttacked[a].GetComponent<Chessman>().PieceType=="King")
			{
				isKingInDanger=true;
				KingNumber=a;
				KingAttacker=EnemyPiecesThatCanAttack[a];
			}				
		}
		if(isKingInDanger)
		{
			for (int b=0; b<EnemyPiecesThatCanBeAttacked.Count; b++)
		    {
			    if(EnemyPiecesThatCanBeAttacked[b]==KingAttacker)
				{
					KillKingAttacker=true;
					TargetNumber=b;
				}
		    }
		}		
	}
    
	//tries to find a square that is not endangered by the enemy pieces
    public void FindSafeSquare()//needs a checkmove, a piece touched and a check danger square issued
	{
		SafeSquares.Clear();
		SafeSquares.TrimExcess();
		for (int i=0; i< Moves.Count; i++) //for every move of the piece touched
		{
			int coord = 10*Moves[i].GetComponent<MovePlate>().matrixX + Moves[i].GetComponent<MovePlate>().matrixY; // the coord of the moveplate
			bool isSquareDangerous=false;
            for (int j=0; j< DangerSquares.Count; j++) //check every possible enemy movement
		    {
				if (coord==DangerSquares[j]) {isSquareDangerous=true;} //if a match is found then the square is dangerous
			}
			if (isSquareDangerous==false && Moves[i].GetComponent<MovePlate>().attack==false) //checks that the move is not an attack
			{
				SafeSquares.Add(Moves[i]); //if no match is found then the square is supposedly safe
	            //Debug.Log(coord+" is a safe square"+Time.time);
			}			
		}
		
	}	

	
	//-------------------------------------------------PLAYING ALGORITHMS -------------------------------------------------------
	private bool IsAIMovePhase()//this checks that we are in the move phase of AI, otherwise AI will keep running both in MovePhase and AttackPhase
	{                           //as NextPhase is at the end of NextTurn which is at the end of PerformAttacks
		bool x = false;
		if ((currentAIPlayer == "White")&&(controller.GetComponent<Game>().Phase == 1)){x = true;}
		if ((currentAIPlayer == "Black")&&(controller.GetComponent<Game>().Phase == 3)){x = true;}
		return x;
	}
	
	IEnumerator AIRandomMoving()//this is the algorithm for completely random moves
	{
		while (controller.GetComponent<Game>().GetCurrentPlayer()== currentAIPlayer && IsAIMovePhase())
		{
		    CheckCurrentPieces();						
		    int RndNumPiece = UnityEngine.Random.Range(0, AIPieces.Count);
            //Debug.Log(RndNumPiece+" "+Time.time);            			
		    AIPieces[RndNumPiece].GetComponent<Chessman>().OnMouseUp();            			
			  			
		    yield return null;                    					
        	CheckMoves();
            yield return null;            
            if (Moves.Count!=0)
			{			        
                int RndNumMove = UnityEngine.Random.Range(0, Moves.Count);
                //Debug.Log(AIPieces[RndNumPiece]+" "+Time.time);				
		        if(Moves[RndNumMove]!=null) {Moves[RndNumMove].GetComponent<MovePlate>().OnMouseUp();}				
                if (Moves[RndNumMove]!=null&&Moves[RndNumMove].GetComponent<MovePlate>().attack)
		        {
					Moves[RndNumMove].GetComponent<MovePlate>().attackconfirmation=true;
					Moves[RndNumMove].GetComponent<MovePlate>().OnMouseUp();					
		        }				
			}                     			
		}		
	}
	
    IEnumerator AIRandomAttacking()//this will attack anything on sight	
    {			
		yield  return StartCoroutine(CheckPiecesThatCanAttack());            			
        //Debug.Log(AIPiecesThatCanAttack.Count+" pieces can attack "+Time.time);
		if (AIPiecesThatCanAttack.Count!=0) 
		{
			//Debug.Log(AIPiecesThatCanAttack.Count+" pieces can attack "+Time.time);
			for (int a=0; a<AIPiecesThatCanAttack.Count; a++)
			{
				AIPiecesThatCanAttack[a].GetComponent<Chessman>().OnMouseUp();
				//yield return null;                    					
				CheckMoves(); //CheckMoves doesn't register a CurrentAttack moveplate
				//yield return null;				
				GameObject item = Moves.Find(x=> x.GetComponent<MovePlate>().attack == true);
				item.GetComponent<MovePlate>().attackconfirmation=true;
				item.GetComponent<MovePlate>().OnMouseUp();
                yield return null;				
			}
			
            yield return StartCoroutine(AIRandomMoving());			
		}		
		else {yield return StartCoroutine(AIRandomMoving());}
	}
	
    IEnumerator WeighedAttacking()//this is the procedure that is called to declare the attacks
	{
		for (int a=0; a<AIPiecesThatCanAttack.Count; a++)
		{
			AIPiecesThatCanAttack[a].GetComponent<Chessman>().OnMouseUp();
			//yield return null;                    					
			CheckMoves(); //CheckMoves doesn't register a CurrentAttack moveplate
			//yield return null;

            if (Moves.Count!=0)
			{
				int tvalue = 0;
				int xvalue = 0;
					
				if (currentAttackMoveDetected)
				{   //Debug.Log(AIPiecesThatCanAttack[a]+""+Time.time);
					if (AIPiecesThatCanAttack[a].GetComponent<Chessman>().CurrentTargetPiece!=null)
					{tvalue=AIPiecesThatCanAttack[a].GetComponent<Chessman>().CurrentTargetPiece.GetComponent<Chessman>().Value;}					
				}
					
				for (int k=0; k<Moves.Count; k++)
				{					    
					if (Moves[k].GetComponent<MovePlate>().attack)
					{
						Moves[k].GetComponent<MovePlate>().OnMouseUp();							
						xvalue = Moves[k].GetComponent<MovePlate>().cp.GetComponent<Chessman>().Value;
														
						if (tvalue <= xvalue)
						{
							tvalue = xvalue;
							CurrentTarget = Moves[k];
						} 
					}					
				}					
			}		
            if (CurrentTarget!=null)
			{			
				CurrentTarget.GetComponent<MovePlate>().attackconfirmation=true;
				CurrentTarget.GetComponent<MovePlate>().OnMouseUp();
			}
            yield return null;				
			}
	}
	
	
	IEnumerator AIWeighedAttacking()//this will attack the most valuable piece on sight
	{
		yield  return StartCoroutine(CheckPiecesThatCanAttack());         		
        		
        if (AIPiecesThatCanAttack.Count!=0) 
		{
			yield return StartCoroutine(WeighedAttacking());			
            
			yield return StartCoroutine(AIRandomMoving());			
		}		
		else {yield return StartCoroutine(AIRandomMoving());}
	}
	
	
	IEnumerator AINoobKid()//this will attack the most valuable piece on sight but will care for king
	{
		yield  return StartCoroutine(CheckPiecesThatCanAttack());         		
        		
        if (AIPiecesThatCanAttack.Count!=0) 
		{
			yield return StartCoroutine(WeighedAttacking());            		
		}
		//This only declares the attacks, it doesnt do any move
		
		yield  return StartCoroutine(CheckAIPiecesThatCanBeAttacked());
		yield  return StartCoroutine(CheckEnemyPiecesThatCanBeAttacked());
		KingSituation();		
		
		if  (isKingInDanger==true)  //this moves the king if its in danger
		{
			yield  return StartCoroutine(CheckDangerSquares());
			
			AIPiecesThatCanBeAttacked[KingNumber].GetComponent<Chessman>().OnMouseUp();           			
		    yield return null;                    					
        	CheckMoves();
            //yield return null; 			
            if (Moves.Count!=0)
			{		
		        FindSafeSquare(); //this will attempt to find a safe square to move to
				if (SafeSquares.Count!=0)
				{
					int RndNumMove = UnityEngine.Random.Range(0, SafeSquares.Count);	            			
		            SafeSquares[RndNumMove].GetComponent<MovePlate>().OnMouseUp();                    
				}
		        else if (SafeSquares.Count==0)
				{//moves the king if there is nowhere safe to go	
                    GameObject item = Moves.Find(x=> x.GetComponent<MovePlate>().attack == false); 
					//if there is a possible move that is not an attack
					if (item!=null){item.GetComponent<MovePlate>().OnMouseUp();}
					//if all of the moves are attacks
                    if (item==null){yield return StartCoroutine(AIRandomMoving());}					
				}				
			}
			else if (Moves.Count==0)
			{//this should only happen if the king cant move or attack anywhere
				yield return StartCoroutine(AIRandomMoving());
			}
		}
		if  (isKingInDanger==false)   //this moves regularly if king its not in danger
		{
			yield return StartCoroutine(AIRandomMoving()); 
		}
	}					
                
}
