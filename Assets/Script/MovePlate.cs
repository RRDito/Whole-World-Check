using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;
	public GameObject movePlate;
	public GameObject LMIndicator;
	public GameObject bforecast;
	public GameObject audioManager;
	public GameObject cp;
		
	public GameObject reference = null;
	public GameObject OldReference = null;
	
	// Board positions, not world positions
	public int matrixX;
	public int matrixY;
	Vector3 Old;
	
	// false: movement, true: attacking
	public bool attack = false;
	public bool attackconfirmation = false;
	// true: attack and move  false: non reachable moves
	public bool playable = true;
	public bool currentAttack = false;
	
	
	public Sprite RedCross, UnplayableCross, Sword; 
	public Sprite LastMoveStart, LastMoveEnd, LastMoveAttacker, LastMoveTarget;
		
	
	public void Start()
	{
		controller = GameObject.FindGameObjectWithTag("GameController");
		audioManager = GameObject.FindGameObjectWithTag("AudioManager");		
		
		if (attack)
		{
			if ( !controller.GetComponent<Game>().IsAIPlaying() && !controller.GetComponent<Game>().IsAttackPhase() ) 
			{ controller.GetComponent<Tutorial>().ShowTutorial("Attack"); }

			//cp is the objective piece of the moveplate attack
			cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
			cp.GetComponent<Chessman>().cpmp = gameObject; //this assign this moveplate to the var cpmp stored in cp 
			
			//change sprite to the red cross sprite
			gameObject.GetComponent<SpriteRenderer>().sprite = RedCross;
			//Change to green
			gameObject.transform.localScale = new Vector3(1.25f, 1.25f);
			gameObject.GetComponent<SpriteRenderer>().color = new Color (0.0f, 1.0f, 0.0f, 1.0f);
			if (reference.GetComponent<Chessman>().CurrentTargetPiece==cp)//this is to check if this attack mp corresponds to the current attack
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = Sword;
				gameObject.transform.localScale = new Vector3(1.5f, 1.5f);
				
				int scid = reference.GetComponent<Chessman>().SwordColorID;
				Color C = controller.GetComponent<Game>().GlowColorManager.GetComponent<GlowColorManager>().GetSpecificColor(scid);
				gameObject.GetComponent<SpriteRenderer>().color = C;
				
				gameObject.GetComponent<BoxCollider2D>().enabled = false;
				currentAttack = true;
				controller.GetComponent<Game>().PieceTargeted=cp;
				DoTheForecast();
			}
            //HideAIMovePlates();           			
		}
        
        if (!playable)
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = UnplayableCross;
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
			gameObject.GetComponent<SpriteRenderer>().color = new Color (0.15f, 0.15f, 0.15f, 0.7f);
			HideUnplayableMoves();
			//HideAIMovePlates();
		}
		
		HideAIMovePlates();
        HideMovesInAttackPhase();		
	}
	
	public void CheckForPromotion()
	{
		if (reference.GetComponent<Chessman>().player=="White" && reference.GetComponent<Chessman>().PieceType =="Pawn" && matrixY == 7 && !attack)
		{gameObject.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 0.3f, 1f);}
	
	    if (reference.GetComponent<Chessman>().player=="Black" && reference.GetComponent<Chessman>().PieceType =="Pawn" && matrixY == 0 && !attack)
		{gameObject.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 0.3f, 1f);}	 
	}
	
	public void Update ()
	{	
        CheckForPromotion(); 	
		if (controller.GetComponent<Game>().GetCurrentPlayer() != reference.GetComponent<Chessman>().player && !currentAttack)
		{
			gameObject.GetComponent<SpriteRenderer>().color = new Color (1.0f, 0.0f, 0.0f, 0.35f);		
		} 
        if (!playable){HideUnplayableMoves();}
        HideAIMovePlates();
        HideMovesInAttackPhase();		
	}
	
	public void HideMovesInAttackPhase()//this may be obsolete
	{
		if((controller.GetComponent<Game>().Phase == 2)||(controller.GetComponent<Game>().Phase == 4))
		{
			gameObject.GetComponent<SpriteRenderer>().color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
		}
	}
	
    public void HideAIMovePlates()//this is used to hide the moveplates when AI is playing
    {
		if (controller.GetComponent<Game>().IsAIPlaying()){gameObject.GetComponent<SpriteRenderer>().color = new Color (0.0f, 0.0f, 0.0f, 0.0f);}
	}

    public void HideUnplayableMoves()
    {
		if (PlayerPrefs.GetString("ShowUnplayableMoves")!= "Yes") {gameObject.GetComponent<SpriteRenderer>().color = new Color (0.0f, 0.0f, 0.0f, 0.0f);};
	}	
	
	public void OnMouseUp()
	{
		controller = GameObject.FindGameObjectWithTag("GameController");
		audioManager = GameObject.FindGameObjectWithTag("AudioManager");
		
		//If you spawn moveplates of the player whose turn is not, destroy those moveplates after OnMouseUp
		if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() != reference.GetComponent<Chessman>().player)
		{
			reference.GetComponent<Chessman>().DestroyMovePlates();			
	    }
		
		//This checks if the moveplate touched corresponds to a piece of the player allowed to play
		//reference.GetComponent<Chessman>().player is the variable player of the piece that is moving
		if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == reference.GetComponent<Chessman>().player)
		{
		   if (attack)			   
		   {
			   //this was moved to start, but needs to stay here for AI purposes
			   cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
			   cp.GetComponent<Chessman>().cpmp = gameObject; //this assign this moveplate to the var cpmp stored in cp
			   
			   controller.GetComponent<Game>().PieceTargeted=cp;
			   			   
			   if (attackconfirmation)
			   {

				if (!controller.GetComponent<Game>().IsAIPlaying()) { controller.GetComponent<Tutorial>().ShowTutorial("AttackReady"); }

				//this lines of code is what assigns the CurrentTargetPiece for the attacking piece				   
			    reference.GetComponent<Chessman>().SwordColorID = 0; //this clears the SwordColor so it wont alter the AssignColorID function
				controller.GetComponent<Game>().GlowColorManager.GetComponent<GlowColorManager>().ClearUnusedTargetColors();
				//this clears the unused target colors before picking a new SwordColorID
				//this line was removed cause it caused the Target Piece to stay highlighted, as SwordColorID would take the same value
				reference.GetComponent<Chessman>().CurrentTargetPiece = cp;				
				reference.GetComponent<Chessman>().CTPOnSight = true; //so the SwordColor has alpha = 1
				
					if (cp.GetComponent<Chessman>().TargetColorID == 0)//if cp has no TargetColorID assign a new one
						{
							int x = controller.GetComponent<Game>().GlowColorManager.GetComponent<GlowColorManager>().AssignColorID();
							reference.GetComponent<Chessman>().SwordColorID = x;
							cp.GetComponent<Chessman>().TargetColorID = x;					
						}
					if (cp.GetComponent<Chessman>().TargetColorID != 0)//if cp has a TargetColor already then take it
						{
							reference.GetComponent<Chessman>().SwordColorID = cp.GetComponent<Chessman>().TargetColorID;                    
						}
			    			   
			   	
				//this puts the Last Move Indicators
				DestroyLMIndicators();
				SpawnLastMoveIndicators(reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard(), LastMoveAttacker); //for the initial square
				SpawnLastMoveIndicators(matrixX, matrixY, LastMoveTarget); //for the last square of the move
                
				
			    //this is what changes the turn if it was an attack, thats why it was removed from DoTheMove			    
		        reference.GetComponent<Chessman>().DestroyMovePlates();	 
                reference.GetComponent<Chessman>().HasMoved = true;                
				
				//controller.GetComponent<Game>().NextPhase();
                controller.GetComponent<Game>().PieceTargeted=null; // this removes BattleForecast after the attack	is declared			
			   
			   
			   }
			   //if attack true but not attack confirmation
			   else
			   {
				    //this should run the attack forecast during attack confirmation					
					DoTheForecast();
				   
				    //this gets rid of all the others moveplates and changes the moveplate for attack confirmation				    	
					Old = gameObject.transform.position;
			        OldReference = reference;
			        				   
			        reference.GetComponent<Chessman>().DestroyMovePlates();
							   	   
			        GameObject mp = Instantiate(movePlate, Old, Quaternion.identity);
				   
			        MovePlate mpScript = mp.GetComponent<MovePlate>();
		            mpScript.attack = true;
			        mpScript.attackconfirmation=true;
			        mpScript.reference = OldReference;
			        mp.transform.localScale = new Vector3(2.5f, 2.5f);
			        mp.GetComponent<SpriteRenderer>().color = new Color (1.0f, 0.0f, 0.0f, 1.0f);
                    mp.GetComponent<BoxCollider2D>().enabled = true;        
 
		       }
		   }
		   
		   
           //This makes so it only moves the piece if it wasnt an attack moveplate 
           if (attack == false) 
		   {			   
			   //reference.GetComponent<Chessman>().CurrentTargetPiece = null; //this resets CurrentTargetPiece if you move the piece			   
			   
			   DoTheMove();				  
			   audioManager.GetComponent<AudioManager>().PlaySoundRndPitch("Move",0.75f,1.25f);               
			   controller.GetComponent<Game>().NextPhase();              			  
	       }
		  
		}
	}
	
	//this makes the actual move, no longer changes the turn because it would change the turn twice if its an attack
	public void DoTheMove()
	{
		//this puts the Last Move Indicators
		DestroyLMIndicators();
		SpawnLastMoveIndicators(reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard(), LastMoveStart); //for the initial square
		SpawnLastMoveIndicators(matrixX, matrixY, LastMoveEnd); //for the last square of the move
		
		
		//This is supposed to empty the reference square with the function SetPositionEmpty, with the coordinates of the reference 
		controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());
			   
	    reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();
	    controller.GetComponent<Game>().SetPosition(reference);		
			
        reference.GetComponent<Chessman>().DestroyMovePlates();	 
        reference.GetComponent<Chessman>().HasMoved = true;		
	}
	
	
	public void SetCoords(int x, int y)
	{
		matrixX = x;
		matrixY = y;
	}
	
	public void SetReference(GameObject obj)
	{
		reference = obj;
	}
	
	public GameObject GetReference()
	{
		return reference;
	}
	
	public void DoTheForecast()
	{
		if (!controller.GetComponent<Game>().IsAIPlaying()) { controller.GetComponent<Tutorial>().ShowTutorial("BattleForecast"); }

		int p = controller.GetComponent<Game>().Phase;		
		bool iap = controller.GetComponent<Game>().IsAIPlaying();
		if( (p!=2) && (p!=4) && (iap == false) )  
		{		
		cp.GetComponent<HPBar>().Preview(reference.GetComponent<Chessman>().ATK);
		bforecast = GameObject.FindGameObjectWithTag("BattleForecast");					
		bforecast.GetComponent<BattleForecast>().Forecast(cp, reference, cp.GetComponent<Chessman>().HP, reference.GetComponent<Chessman>().ATK);
		}
	}
	
	public void SpawnLastMoveIndicators(int matrixX, int matrixY, Sprite XSprite)
	{
		float x = matrixX;
		float y = matrixY;
		
		//this will call the values from VariablesResolutions
		VariablesResolutions varres = controller.GetComponent<VariablesResolutions>();		
		varres.VariablesForCoords (out float a, out float b);
		
		x *= a;
		y *= a;
		
		x += b;
		y += b;
		
		GameObject lm = Instantiate(LMIndicator, new Vector3(x,y, 74.0f), Quaternion.identity, GameObject.FindGameObjectWithTag("Board").transform);
        
		lm.GetComponent<SpriteRenderer>().sprite = XSprite;		
	}
	
	public void DestroyLMIndicators()	
	{	        
		GameObject[] LMind = GameObject.FindGameObjectsWithTag("LastMoveIndicator");
		for (int i=0; i< LMind.Length; i++)
		{
			Destroy(LMind[i]);
		}
	}
}
