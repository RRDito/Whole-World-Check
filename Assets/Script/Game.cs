using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//This game was made by Reinaldo R. Martín Pardo

public class Game : MonoBehaviour
{
	public GameObject Progress;
	public GameObject WStats;
	public GameObject Themes;
    public GameObject chesspiece;
	public GameObject audioManager;
    public GameObject AIManager;
    public GameObject AIPlayButton;
	public GameObject GlowColorManager;
	public GameObject CanvasTurn;
	public GameObject BattleFlag;
    
	public GameObject PieceSelected = null;//this is called from other scripts to know which piece is selected 
    public GameObject LastPieceMoved = null; //this is used to mark the last piece moved	
	public GameObject PieceTargeted = null;//this is called from other scripts to use in battle forecast
		
	//Positions and team for each chesspiece
	private GameObject[,] positions = new GameObject[8,8];
    private GameObject[] playerBlack = new GameObject[16];
	private GameObject[] playerWhite = new GameObject[16];
	private GameObject[] MPlates;	
	
	public string currentPlayer = "White";
	
	//Sets: "US","British","Chinese","German","Russian","Japanese","Italian","French" 
    public string WhiteSet; 
    public string BlackSet; 	
	public int WhiteKills = 0, BlackKills = 0; 
    public bool WhiteMorale = false; 	
	public bool BlackMorale = false;
	public bool BlackAI; 	
	public bool WhiteAI;
	public bool TestGame = false;  //this will be used for implementing 2 AI Test games
	private bool HasAIBeenCalled = false;
    public float AISpeed = 0.25f;    	
		
	private bool gameOver = false;
    public int Phase = 1;	
	
	
	// Start is called before the first frame update
    void Start()
    {
        //audioManager = GameObject.FindGameObjectWithTag("AudioManager");		
		int RndSong = UnityEngine.Random.Range(0, 2);
		if (RndSong == 0){audioManager.GetComponent<AudioManager>().PlayMusic("Somber", true);}
		else{audioManager.GetComponent<AudioManager>().PlayMusic("Thoughts", true);}
		
		WStats.SetActive(false);		
		
		WhiteSet = PlayerPrefs.GetString("WhiteSet");
		BlackSet = PlayerPrefs.GetString("BlackSet");
		
		//this part check the AI Settings
		AISpeed=PlayerPrefs.GetFloat("AISpeed");
		if (PlayerPrefs.GetString("WhiteAI")=="Yes")  
		    {
			WhiteAI = true;
			GameObject.FindGameObjectWithTag("WhiteAI").GetComponent<Image>().enabled = true;
			}
		if (PlayerPrefs.GetString("BlackAI")=="Yes")  
		    {
			BlackAI = true;
			GameObject.FindGameObjectWithTag("BlackAI").GetComponent<Image>().enabled = true;
		    }
		if (WhiteAI == true && BlackAI == true)
		    {				
				if (PlayerPrefs.GetString("TestGame")=="Yes"){TestGame = true; AIPlayButton.SetActive(true);}				
			}			
		
		
			
		//puts the pieces in the relative positions according to the Player Set
		switch(WhiteSet)
		{
		   case "US":
		playerWhite = new GameObject[]{
		   Create("White_rook",0,0), Create("White_knight",1,0), Create("White_knight",2,0),
		   Create("White_queen",3,0), Create("White_king",4,0), Create("White_knight",5,0),
		   Create("White_knight",6,0), Create("White_rook",7,0),
		   Create("White_pawn",0,1), Create("White_pawn",1,1), Create("White_pawn",2,1),
		   Create("White_pawn",3,1), Create("White_pawn",4,1), Create("White_pawn",5,1),
		   Create("White_pawn",6,1), Create("White_pawn",7,1) };break;
		   
		   case "Russian":
		playerWhite = new GameObject[]{
		   Create("White_rook",0,0), Create("White_knight",1,0), Create("White_bishop",2,0),
		   Create("White_queen",3,0), Create("White_king",4,0), Create("White_bishop",5,0),
		   Create("White_knight",6,0), Create("White_rook",7,0),
		   Create("White_pawn",0,1), Create("White_pawn",1,1), Create("White_pawn",2,1),
		   Create("White_pawn",3,1), Create("White_pawn",4,1), Create("White_pawn",5,1),
		   Create("White_pawn",6,1), Create("White_pawn",7,1) };break;
		 
           case "Chinese":
        playerWhite = new GameObject[]{
		   Create("White_rook",0,0), Create("White_pawn",1,0), Create("White_rook",2,0),
		   Create("White_queen",3,0), Create("White_king",4,0), Create("White_rook",5,0),
		   Create("White_pawn",6,0), Create("White_rook",7,0),
		   Create("White_pawn",0,1), Create("White_pawn",1,1), Create("White_pawn",2,1),
		   Create("White_pawn",3,1), Create("White_pawn",4,1), Create("White_pawn",5,1),
		   Create("White_pawn",6,1), Create("White_pawn",7,1) };break;
        
           case "Italian":
        playerWhite = new GameObject[]{
		   Create("White_rook",0,0), Create("White_bishop",1,0), Create("White_bishop",2,0),
		   Create("White_queen",3,0), Create("White_king",4,0), Create("White_bishop",5,0),
		   Create("White_bishop",6,0), Create("White_rook",7,0),
		   Create("White_pawn",0,1), Create("White_pawn",1,1), Create("White_pawn",2,1),
		   Create("White_pawn",3,1), Create("White_pawn",4,1), Create("White_pawn",5,1),
		   Create("White_pawn",6,1), Create("White_pawn",7,1) };break;
		   
	    default:
        playerWhite = new GameObject[]{
		   Create("White_rook",0,0), Create("White_knight",1,0), Create("White_bishop",2,0),
		   Create("White_queen",3,0), Create("White_king",4,0), Create("White_bishop",5,0),
		   Create("White_knight",6,0), Create("White_rook",7,0),
		   Create("White_pawn",0,1), Create("White_pawn",1,1), Create("White_pawn",2,1),
		   Create("White_pawn",3,1), Create("White_pawn",4,1), Create("White_pawn",5,1),
		   Create("White_pawn",6,1), Create("White_pawn",7,1) };break;
		}
		
		switch(BlackSet)
		{
			case "US":
		playerBlack = new GameObject[]{
		   Create("Black_rook",0,7), Create("Black_knight",1,7), Create("Black_knight",2,7),
		   Create("Black_queen",3,7), Create("Black_king",4,7), Create("Black_knight",5,7),
		   Create("Black_knight",6,7), Create("Black_rook",7,7),
		   Create("Black_pawn",0,6), Create("Black_pawn",1,6), Create("Black_pawn",2,6),
		   Create("Black_pawn",3,6), Create("Black_pawn",4,6), Create("Black_pawn",5,6),
		   Create("Black_pawn",6,6), Create("Black_pawn",7,6) };break;
		
           case "Russian":	
	    playerBlack = new GameObject[]{
		   Create("Black_rook",0,7), Create("Black_knight",1,7), Create("Black_bishop",2,7),
		   Create("Black_queen",3,7), Create("Black_king",4,7), Create("Black_bishop",5,7),
		   Create("Black_knight",6,7), Create("Black_rook",7,7),
		   Create("Black_pawn",0,6), Create("Black_pawn",1,6), Create("Black_pawn",2,6),
		   Create("Black_pawn",3,6), Create("Black_pawn",4,6), Create("Black_pawn",5,6),
		   Create("Black_pawn",6,6), Create("Black_pawn",7,6) }; break;
		
		   case "Chinese":	
	    playerBlack = new GameObject[]{
		   Create("Black_rook",0,7), Create("Black_pawn",1,7), Create("Black_rook",2,7),
		   Create("Black_queen",3,7), Create("Black_king",4,7), Create("Black_rook",5,7),
		   Create("Black_pawn",6,7), Create("Black_rook",7,7),
		   Create("Black_pawn",0,6), Create("Black_pawn",1,6), Create("Black_pawn",2,6),
		   Create("Black_pawn",3,6), Create("Black_pawn",4,6), Create("Black_pawn",5,6),
		   Create("Black_pawn",6,6), Create("Black_pawn",7,6) }; break;
		   
		   case "Italian":	
	    playerBlack = new GameObject[]{
		   Create("Black_rook",0,7), Create("Black_bishop",1,7), Create("Black_bishop",2,7),
		   Create("Black_queen",3,7), Create("Black_king",4,7), Create("Black_bishop",5,7),
		   Create("Black_bishop",6,7), Create("Black_rook",7,7),
		   Create("Black_pawn",0,6), Create("Black_pawn",1,6), Create("Black_pawn",2,6),
		   Create("Black_pawn",3,6), Create("Black_pawn",4,6), Create("Black_pawn",5,6),
		   Create("Black_pawn",6,6), Create("Black_pawn",7,6) }; break;
		   
		   case "KCA":
		   ScenarioCreate(PlayerPrefs.GetInt("Scenario"));		   
		   break;
		   
		default:	
	    playerBlack = new GameObject[]{
		   Create("Black_rook",0,7), Create("Black_knight",1,7), Create("Black_bishop",2,7),
		   Create("Black_queen",3,7), Create("Black_king",4,7), Create("Black_bishop",5,7),
		   Create("Black_knight",6,7), Create("Black_rook",7,7),
		   Create("Black_pawn",0,6), Create("Black_pawn",1,6), Create("Black_pawn",2,6),
		   Create("Black_pawn",3,6), Create("Black_pawn",4,6), Create("Black_pawn",5,6),
		   Create("Black_pawn",6,6), Create("Black_pawn",7,6) }; break;
		}		   
		   
		// Set all piece positions on the position board
        for (int i = 0; i < playerBlack.Length; i++)
		{
			SetPosition(playerBlack[i]);
			SetPosition(playerWhite[i]);
		}		
		
        //this starts the game if White is AI
        if (WhiteAI == true && TestGame== false)
		{			
			CallForAI();
		}

		this.GetComponent<Tutorial>().ShowTutorial("Intro");
    }
    
	
	
			
	
	//Defines Create, used above
	//The functions used are created in the Chessman script
	public GameObject Create(string name, int x, int y)
	{        
		GameObject obj = Instantiate(chesspiece, new Vector3(0,0,0), Quaternion.identity, GameObject.FindGameObjectWithTag("Board").transform);
		Chessman cm = obj.GetComponent<Chessman>();
		cm.name = name;
		cm.SetXBoard(x);
		cm.SetYBoard(y);
		cm.Activate();
		return obj;
	}


    public void SetPosition(GameObject obj)
	{
		Chessman cm = obj.GetComponent<Chessman>();
		
		positions[cm.GetXBoard(),cm.GetYBoard()] = obj;
	}
	
	public void SetPositionEmpty(int x, int y)
	{
		positions [x,y] = null;
	}
	
	public GameObject GetPosition(int x, int y)
	{
		return positions[x,y];
	}
	
	
	
	//This function allows to see if the position is on board by checking the lengths of the position array, defined above
	public bool PositionOnBoard(int x, int y)
	{
		if (x < 0 || y < 0 || x >= positions.GetLength(0) || y>= positions.GetLength(1)) return false;
		return true;
	}
	
	//This function tells who the current player is
	public string GetCurrentPlayer()
	{
		return currentPlayer;
	}
	
	public bool IsGameOver()
	{
		return gameOver;
	}
	
	public bool IsAIPlaying() //this checks if its turn for AI
	{
		if((BlackAI == true && currentPlayer == "Black") || (WhiteAI == true && currentPlayer == "White")) return true;
		return false;
	}

	public bool IsAttackPhase() //this checks if its an attack Phase
	{
		if ((Phase == 2) || (Phase == 4)) return true;
		return false;
	}


	////////////////////////////////////////NEXT TURN AND AI FUNCTIONS///////////////////////////
	
	
	IEnumerator PerformAttacks()//every turn this performs every attack that has been done
	{
		GameObject[] AllThePieces = GameObject.FindGameObjectsWithTag("Chessman");
		//first fill all the pieces		
		
		for (int i=0; i< AllThePieces.Length; i++)
		{
			AllThePieces[i].GetComponent<Chessman>().DoCheckCTPOnSight();
			//then check for all of them if the CTP is on Sight
			yield return null;
		}
		
				
		yield return null;		
		
		GameObject t = Array.Find(AllThePieces, r => (r.GetComponent<Chessman>().player==currentPlayer && r.GetComponent<Chessman>().CTPOnSight==true) );
		// t is used to check if a piece of this player can attack		
		
		if (t!=null) //then the Battle flag is shown
		{
			CanvasTurn.SetActive(true);	
			BattleFlag.GetComponent<BattleFlag>().Start();					
		    audioManager.GetComponent<AudioManager>().PlaySound("BattleDrums");
			yield return new WaitForSeconds(1.5f);
			CanvasTurn.SetActive(false);
		}

		if (PieceTargeted!=null){PieceTargeted.GetComponent<HPBar>().UnPreview();}
		PieceTargeted=null; //this removes the Piece targeted as result of the previous check
		
		for (int i=0; i< AllThePieces.Length; i++)
		{	
			if (AllThePieces[i]!=null)
			{
				
				string p = AllThePieces[i].GetComponent<Chessman>().player;
				bool c = AllThePieces[i].GetComponent<Chessman>().CTPOnSight;
				GameObject g = AllThePieces[i].GetComponent<Chessman>().CurrentTargetPiece;
			
				//for pieces of the current player and that have a CurrentTargetPiece on Sight
				if ((p == currentPlayer)&&(c == true)&& (g!=null))
				{
					GameObject AtkGO = AllThePieces[i];
					Chessman Attacker = AllThePieces[i].GetComponent<Chessman>();
					GameObject VicGO = Attacker.CurrentTargetPiece;
					Chessman Victim = Attacker.CurrentTargetPiece.GetComponent<Chessman>();				
				
				    //BattleForecast and WriteStats are done in a forced way here cause they are usually hidden in Battle Phases
					//This is to avoid blinking in the DoCheckCTPOnSight done above
					//this is a forced BattleForecast
					PieceTargeted = VicGO; //necessary so the animation stays 
					VicGO.GetComponent<HPBar>().Preview(Attacker.ATK);
					GameObject bforecast = GameObject.FindGameObjectWithTag("BattleForecast");					
					bforecast.GetComponent<BattleForecast>().Forecast(VicGO, AtkGO, Victim.HP, Attacker.ATK);
					
					//this is a forced WriteStats
					WStats.SetActive(true);	    
					ColorStats(GameObject.FindGameObjectWithTag("WStats"),AtkGO); //this applies ColorStats to the Stats Window
					GameObject.FindGameObjectWithTag("WStatsPortrait").GetComponent<Image>().sprite = Attacker.PieceSprite.GetComponent<SpriteRenderer>().sprite;
					GameObject.FindGameObjectWithTag("WStatsName").GetComponent<Text>().text =" "+Attacker.player+" "+Attacker.PieceType;	
					GameObject.FindGameObjectWithTag("WStatsHP").GetComponent<Text>().text =" "+Attacker.HP;
					GameObject.FindGameObjectWithTag("WStatsATK").GetComponent<Text>().text =" "+Attacker.ATK;
				    
					//this is the actual attack
					int xHP = Victim.HP;
					int xATK = Attacker.ATK;
					int VictimX = Victim.GetXBoard();
					int VictimY = Victim.GetYBoard();						
					AllThePieces[i].GetComponent<Chessman>().CurrentTargetPiece.GetComponent<Chessman>().HP = xHP - xATK;
					audioManager.GetComponent<AudioManager>().PlaySoundRndPitch("Attack",0.75f,1.25f);
					Attacker.GetComponent<Animator>().SetTrigger("Attack");
					
					yield return new WaitForSecondsRealtime(0.4f);
					
					if (((xHP-xATK)<=0)&&(GetPosition(VictimX,VictimY)==null))					
					//what happens if there is a kill, a version of DoTheMove  //checks that an allied piece isnt already there
					{							                                                       						
						SetPositionEmpty(Attacker.GetXBoard(),Attacker.GetYBoard()); //empties the attacker position
						Attacker.SetXBoard(VictimX);    //gets the victim coordinates
						Attacker.SetYBoard(VictimY);
						Attacker.SetCoords();        //gives the victim coordinates to the attacker, and its position
						SetPosition(AllThePieces[i]);							
						Attacker.HasMoved = true;		
					}				
				
				}
            }			
		}        
		NextTurn();		
	}	
	
	
	public void NextPhase()//useful to organize functions according to phases
	{
		Phase++;
		if (PieceTargeted!=null){PieceTargeted.GetComponent<HPBar>().UnPreview();}
        PieceTargeted=null; //this removes the Piece targeted for every phase shift		
		switch (Phase)
		{
			//White Move Phase, ends with a Move
		case 1: break;
		    //White Attack Phase
		case 2: StartCoroutine(PerformAttacks()); break;
			//Black Move Phase, ends with a Move
		case 3: break;
			//Black Attack Phase
		case 4: StartCoroutine(PerformAttacks()); break;
		
		case 5: Phase = 1; break;
		}
	}	

	//This function swaps the turn
	public void NextTurn()	{        	
	    
		if (PieceSelected!=null){PieceSelected.GetComponent<Animator>().SetBool("isSelected", false);}
        PiecesEnabled(true);  //this should enable pieces when AI ends playing
		
		if (currentPlayer == "White")
		{ currentPlayer = "Black"; }
		else { currentPlayer = "White"; }
		
        if (BlackAI || WhiteAI)
		{			
		if(TestGame == false) //this checks if its not a testgame so it calls AI automatically
		{
			if ( IsAIPlaying() == true ) //this checks if its turn for AI
			StartCoroutine(DelayCallForAI());
	    }    
		if(TestGame == true) {AIPlayButton.GetComponent<Button>().interactable=true;} //this is so AIPlayButton can only be pressed after AI ends up playing 
		}
		
		if (gameOver == false)
		{
			switch (currentPlayer)
			{
			case "White":
			GameObject.FindGameObjectWithTag("WhiteTurn").GetComponent<Image>().enabled = true;
			GameObject.FindGameObjectWithTag("BlackTurn").GetComponent<Image>().enabled = false;
			break;
			case "Black":
			GameObject.FindGameObjectWithTag("BlackTurn").GetComponent<Image>().enabled = true;
			GameObject.FindGameObjectWithTag("WhiteTurn").GetComponent<Image>().enabled = false;
			break;	
			}			
	    }

        NextPhase();		
	}
		
	IEnumerator DelayCallForAI()
    {
		PiecesEnabled(false); //this should disable the pieces while AI plays, before the delay so it cannot be exploited
		yield return new WaitForSecondsRealtime(AISpeed);
		if (gameOver == false) {CallForAI();}        		
    }
	
	public void CallForAI()//this is used to call for AI if there is one
	{
		if(TestGame == true) {PiecesEnabled(false);}  //this deactivates the pieces if you are using TestGame, otherwise it happens in DelayCallForAI
		HasAIBeenCalled=false;//this is important so White doesnt play at the same time with Black when there are 2 AIs		
		
		if (BlackAI == true && currentPlayer == "Black")
		{			
			AIManager.GetComponent<AI>().AIMakeAPlay(currentPlayer);
			HasAIBeenCalled=true;
		}
		
		if (WhiteAI == true && currentPlayer == "White" && HasAIBeenCalled==false)
		{			
			AIManager.GetComponent<AI>().AIMakeAPlay(currentPlayer);			
		}
	}
	
	
	//This resets the scene Game after pressing a button
	public void Update()
	{
		//this updates the scores when someone dies in Chessman
		GameObject.FindGameObjectWithTag("WhiteKills").GetComponent<Text>().text = ""+WhiteKills;
		GameObject.FindGameObjectWithTag("BlackKills").GetComponent<Text>().text = ""+BlackKills;		       	
		
		if (gameOver == true && Input.GetMouseButtonDown(0))
		{
			gameOver = false;
			
			switch(PlayerPrefs.GetString("GameType"))
		    {			
			case "Campaign":
			SceneManager.LoadScene("Campaign");
			break;
			
            default:
			SceneManager.LoadScene("MainMenu");
			break;
		    }			
		}
		
		if (gameOver == true && PlayerPrefs.GetString("GameType")=="Data")
		{
			SceneManager.LoadScene("Data");
		}
	}
	
	
	//This declares the winner when the king is killed in MovePlate, sets gameOver to true
	public void Winner(string playerWinner)
	{
		gameOver = true;		
				
		GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().text = playerWinner + " wins!!!";
		//audioManager = GameObject.FindGameObjectWithTag("AudioManager");		
		audioManager.GetComponent<AudioManager>().PlaySound("GameOver");
		if ( (PlayerPrefs.GetString("GameType")=="Campaign") && playerWinner=="White"){CampaignWin();}
		
		/////////////////////////DATA GAMES///////////////////////// U, B, R, C, G, J, F, I;
		if (PlayerPrefs.GetString("GameType")=="Data")
		{
			string DataVar = playerWinner +"_w"+ WhiteSet + "_v_" +"b"+ BlackSet +"_d"+ PlayerPrefs.GetFloat("AIDifficulty");
			// Example "White_wUS_v_bChinese_d1"			
			
			int w = PlayerPrefs.GetInt(DataVar)+1;
			PlayerPrefs.SetInt(DataVar, w);
			PlayerPrefs.SetString("DataPlaying", "Yes");
            Debug.Log(DataVar+" "+PlayerPrefs.GetInt(DataVar));			
		}
	}
	
	public void AIButton()
	{
		//this works for testing 2 AI
		if (TestGame == true) 
        {
            CallForAI();	
            AIPlayButton.GetComponent<Button>().interactable=false;		   
        }
	}
	    
    public void PiecesEnabled(bool enabler) //this enables the collider of pieces
	{
		GameObject[] AllThePieces = GameObject.FindGameObjectsWithTag("Chessman");
		for (int i=0; i< AllThePieces.Length; i++)
		{
			AllThePieces[i].GetComponent<BoxCollider2D>().enabled = enabler;		
		}
	} 
	
	public void ColorStats(GameObject Z, GameObject Teampiece) //this function asigns the color for stats or forecast window(z) depending on the team of Teampiece
	{
		if (Teampiece.GetComponent<Chessman>().player == "White"){Themes.GetComponent<ThemeColors>().ColorTeamWhite(Z);}
		if (Teampiece.GetComponent<Chessman>().player == "Black"){Themes.GetComponent<ThemeColors>().ColorTeamBlack(Z);}
	}	
      	
	
	////////////////////////////////////////////////////////CAMPAIGN SCENARIO//////////////////////////////////////////////////
	public void CampaignWin()
	{
		switch(PlayerPrefs.GetInt("Scenario"))
		{
			case 1: 
			Progress.GetComponent<ProgressData>().Sc1Won = true;
			WriteWinnerSets(0);
			break;
			case 2: 
			Progress.GetComponent<ProgressData>().Sc2Won = true;
			WriteWinnerSets(1);
			break;
			case 3: 
			Progress.GetComponent<ProgressData>().Sc3Won = true;
			WriteWinnerSets(2);
			break;
			case 4: 
			Progress.GetComponent<ProgressData>().Sc4Won = true;
			WriteWinnerSets(3);
			break;
			case 5: 
			Progress.GetComponent<ProgressData>().Sc5Won = true;
			WriteWinnerSets(4);
			break;
			case 6: 
			Progress.GetComponent<ProgressData>().Sc6Won = true;
			WriteWinnerSets(5);
			break;
			case 7: 
			Progress.GetComponent<ProgressData>().Sc7Won = true;
			WriteWinnerSets(6);
			break;
			case 8: 
			Progress.GetComponent<ProgressData>().Sc8Won = true;
			WriteWinnerSets(7);
			break;
			case 9: 
			Progress.GetComponent<ProgressData>().Sc9Won = true;
			WriteWinnerSets(8);
			SceneManager.LoadScene("Cinematics");
			break;		
		}
		
		SaveSystem.SaveData(Progress.GetComponent<ProgressData>()); 				
		
		int sn = PlayerPrefs.GetInt("Scenario")+1;
		if (sn<10){PlayerPrefs.SetInt("Scenario",sn);}
		else {PlayerPrefs.SetInt("Scenario",1);}
				
	}
	
	public void WriteWinnerSets(int X)// this should add the name of the winning set to a var containing all winning sets
	{
		string test = Progress.GetComponent<ProgressData>().ScWinners[X];		
		if (!test.Contains(WhiteSet)){test += WhiteSet;}
		Progress.GetComponent<ProgressData>().ScWinners[X] = test;		
	}
	
	public void ArmyCreate(string a,string b,string c,string d,string e,string f,string g,string h, string a1,string b1,string c1,string d1,string e1,string f1,string g1,string h1)
	{
		string[] x = new string[] {a,b,c,d,e,f,g,h,a1,b1,c1,d1,e1,f1,g1,h1};
		int row; int col;
		GameObject[] pB = new GameObject[16];
		//private GameObject[] playerBlack = new GameObject[16];
		
		for (int i=0; i< 16; i++)
		{
			if (i<8){row=7;col=i;}
			else {row=6;col=i-8;}
			
			switch(x[i])
			{
				case "p": pB[i]= Create("Black_pawn",col,row); break;
				case "k": pB[i]=Create("Black_knight",col,row); break;
				case "b": pB[i]=Create("Black_bishop",col,row); break;
				case "R": pB[i]=Create("Black_rook",col,row); break;
				case "Q": pB[i]=Create("Black_queen",col,row); break;
				case "Ki": pB[i]=Create("Black_king",col,row); break;
				case "g": pB[i]=Create("Black_guard",col,row); break;
				case "m": pB[i]=Create("Black_marshall",col,row); break;
				case "L": pB[i]=Create("Black_lord",col,row); break;
				case "Mt": pB[i]=Create("Black_mortar",col,row); break;
				case "Ch": pB[i]=Create("Black_chancellor",col,row); break;
				case "Dr": pB[i]=Create("Black_dragon",col,row); break;
				case "Ca": pB[i]=Create("Black_cardinal",col,row); break;
				case "Pa": pB[i]=Create("Black_paladin",col,row); break;
			}
		}
		
		playerBlack = new GameObject[]{pB[0],pB[1],pB[2],pB[3],pB[4],pB[5],pB[6],pB[7],pB[8],pB[9],pB[10],pB[11],pB[12],pB[13],pB[14],pB[15]};
	}
	
	public void ScenarioCreate(int x)
	{
		switch(x)
		{
			case 1://done
		    ArmyCreate("p","p","p","p","p","p","p","p",  "p","p","p","p","p","p","p","p");break;
		   
		    case 2://done
		    ArmyCreate("R","k","k","g","g","k","k","R",  "p","p","p","p","p","p","p","p");break;

            case 3://done
		    ArmyCreate("b","L","L","g","g","L","L","b",  "g","p","p","p","p","p","p","g");break;

            case 4://done
		    ArmyCreate("R","b","b","Ch","Ch","b","b","R",  "k","k","p","p","p","p","k","k");break;
    
            case 5://done
		    ArmyCreate("b","L","b","Ca","Ca","b","L","b",  "k","p","p","g","g","p","p","k");break;
 
            case 6://done
		    ArmyCreate("Ch","k","m","m","m","m","k","Ch",  "p","p","g","g","g","g","p","p");break;
 
            case 7://done
		    ArmyCreate("b","b","Pa","Pa","Pa","Pa","b","b",  "k","p","p","g","g","p","p","k");break;
 
            case 8://done
		    ArmyCreate("Dr","Dr","k","Q","Q","k","Dr","Dr",  "p","p","g","g","g","g","p","p");break;

            case 9://done
		    ArmyCreate("Pa","Mt","g","Q","Q","g","Mt","Pa",  "p","p","g","g","g","g","p","p");break;			
		    
		}
	}
	

	
	
}
