using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chessman : MonoBehaviour
{
   public GameObject controller;
   public GameObject movePlate;
   public GameObject audioManager;
   public GameObject cpmp; //this is the moveplate that has this piece as a cp (attack moveplate)
   public GameObject mchart;
   public GameObject CurrentTargetPiece; //this is the piece that is being attacked every turn
   //public int CurrentTargetX, CurrentTargetY; //these are the coordinates of CurrentTargetPiece
   
   private List<GameObject> PlayerPieces = new List<GameObject>();//this counts how many pieces has the current player
   private List<GameObject> PlayerPawns = new List<GameObject>();//this counts how many pawns has the current player   
   private GameObject[] MPlates;
   public bool CTPOnSight;
   
   private int xBoard = -1;
   private int yBoard = -1;
   
   public string player;
   public bool HasMoved = false;
   public bool KnightsCanSide = false;  //for US knights after Outnumber 
   public bool QueenCanL = false; //for British Queen after Outnumber
   public bool PawnCanBack = false; // for Chinese pawns after Outnumber
   public bool KingCanSkip = false; // for French Kings after Outnumber
   public bool BishopCanSkipAllies = false; // for Italian Bishops after Outnumber
   public bool RookCanSkipAllies = false; // for Russian Rooks after Outnumber
   public bool PawnCanAttackFront = false; //
       
   public int HP;
   public int ATK;
   public string PieceType;
   public string PieceSet;
   public bool ONBonus = false;
   public bool MBonus = false;
   public int Value; //used by AI algorithms
   
   private int WHP_Queen, WHP_Knight, WHP_Bishop, WHP_King, WHP_Rook, WHP_Pawn;
   private int BHP_Queen, BHP_Knight, BHP_Bishop, BHP_King, BHP_Rook, BHP_Pawn;
   private int WATK_Queen, WATK_Knight, WATK_Bishop, WATK_King, WATK_Rook, WATK_Pawn;
   private int BATK_Queen, BATK_Knight, BATK_Bishop, BATK_King, BATK_Rook, BATK_Pawn;
   
   public GameObject PieceSprite;
   public GameObject PieceSpriteBlink;
   public GameObject PieceSpriteGlow; //not currently used, keep just in case I revert back to this Glow System
   public GameObject AttackingSymbol;
   
   //public Material def, glow;
   public Color SwordColor; //Sword color 
   public Color TargetColor; //Silhouette color   
   public int SwordColorID = 0; //this is the ID of the color of your sword, and the one you give
   public int TargetColorID = 0; //this is the ID of the color you are receiving, your contourn glow
   
   
   public Sprite Black_queen, Black_knight, Black_bishop, Black_king, Black_rook, Black_pawn, Black_marshall, Black_lord, Black_mortar, Black_guard, Black_chancellor, Black_dragon, Black_paladin, Black_cardinal;
   public Sprite White_queen, White_knight, White_bishop, White_king, White_rook, White_pawn, White_marshall, White_lord, White_mortar, White_guard, White_chancellor, White_dragon, White_paladin, White_cardinal;
   public Sprite Black_queen1, Black_knight1, Black_bishop1, Black_king1, Black_rook1, Black_pawn1, Black_marshall1, Black_lord1, Black_mortar1, Black_guard1, Black_chancellor1, Black_dragon1, Black_paladin1, Black_cardinal1;
   public Sprite White_queen1, White_knight1, White_bishop1, White_king1, White_rook1, White_pawn1, White_marshall1, White_lord1, White_mortar1, White_guard1, White_chancellor1, White_dragon1, White_paladin1, White_cardinal1;
   public Sprite glow_queen, glow_knight, glow_bishop, glow_king, glow_rook, glow_pawn, glow_marshall, glow_lord, glow_mortar, glow_guard, glow_chancellor, glow_dragon, glow_paladin, glow_cardinal;

      

   //This is a function to set all HP and ATK stats
   public void SetStats()
   {
	   
	   //This changes the default stats depending on the Player Set
	   Game sc = controller.GetComponent<Game>();
	   
	   WHP_Queen=Sets.PieceStats(sc.WhiteSet,"Queen", "HP");
	   WHP_Knight=Sets.PieceStats(sc.WhiteSet,"Knight", "HP");
	   WHP_Bishop=Sets.PieceStats(sc.WhiteSet,"Bishop", "HP");
	   WHP_King=Sets.PieceStats(sc.WhiteSet,"King", "HP");
	   WHP_Rook=Sets.PieceStats(sc.WhiteSet,"Rook", "HP");
	   WHP_Pawn=Sets.PieceStats(sc.WhiteSet,"Pawn", "HP");
	   
       BHP_Queen=Sets.PieceStats(sc.BlackSet,"Queen", "HP");
	   BHP_Knight=Sets.PieceStats(sc.BlackSet,"Knight", "HP");
	   BHP_Bishop=Sets.PieceStats(sc.BlackSet,"Bishop", "HP");
	   BHP_King=Sets.PieceStats(sc.BlackSet,"King", "HP");
	   BHP_Rook=Sets.PieceStats(sc.BlackSet,"Rook", "HP");
	   BHP_Pawn=Sets.PieceStats(sc.BlackSet,"Pawn", "HP");
	   
       WATK_Queen=Sets.PieceStats(sc.WhiteSet,"Queen", "ATK");
	   WATK_Knight=Sets.PieceStats(sc.WhiteSet,"Knight", "ATK");
	   WATK_Bishop=Sets.PieceStats(sc.WhiteSet,"Bishop", "ATK");
	   WATK_King=Sets.PieceStats(sc.WhiteSet,"King", "ATK");
	   WATK_Rook=Sets.PieceStats(sc.WhiteSet,"Rook", "ATK");
	   WATK_Pawn=Sets.PieceStats(sc.WhiteSet,"Pawn", "ATK");
       
	   BATK_Queen=Sets.PieceStats(sc.BlackSet,"Queen", "ATK");
	   BATK_Knight=Sets.PieceStats(sc.BlackSet,"Knight", "ATK");
	   BATK_Bishop=Sets.PieceStats(sc.BlackSet,"Bishop", "ATK");
	   BATK_King=Sets.PieceStats(sc.BlackSet,"King", "ATK");
	   BATK_Rook=Sets.PieceStats(sc.BlackSet,"Rook", "ATK");
	   BATK_Pawn=Sets.PieceStats(sc.BlackSet,"Pawn", "ATK");	   
	   
   }


   public void Activate()
   {
	   audioManager = GameObject.FindGameObjectWithTag("AudioManager");
	   controller = GameObject.FindGameObjectWithTag("GameController");
	   Game sc = controller.GetComponent<Game>();
       mchart = GameObject.Find("MoveChart");
	   MoveChart mc = mchart.GetComponent<MoveChart>(); 	   
	   
	   //take the instantiated location and adjust the transform
	   SetCoords();
	   SetStats();
	   
	   switch (this.name)
	   {
		case "Black_queen": PieceSprite.GetComponent<SpriteRenderer>().sprite = Black_queen; player = "Black";HP=BHP_Queen;ATK=BATK_Queen;PieceType="Queen";PieceSet=sc.BlackSet; PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = Black_queen1;break;
	    case "Black_knight": PieceSprite.GetComponent<SpriteRenderer>().sprite = Black_knight; player = "Black";HP=BHP_Knight;ATK=BATK_Knight;PieceType="Knight";PieceSet=sc.BlackSet; PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = Black_knight1;break;
	    case "Black_bishop": PieceSprite.GetComponent<SpriteRenderer>().sprite = Black_bishop; player = "Black";HP=BHP_Bishop;ATK=BATK_Bishop;PieceType="Bishop";PieceSet=sc.BlackSet; PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = Black_bishop1;break;
		case "Black_king": PieceSprite.GetComponent<SpriteRenderer>().sprite = Black_king; player = "Black";HP=BHP_King;ATK=BATK_King;PieceType="King";PieceSet=sc.BlackSet; PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = Black_king1;break;
		case "Black_rook": PieceSprite.GetComponent<SpriteRenderer>().sprite = Black_rook; player = "Black";HP=BHP_Rook;ATK=BATK_Rook;PieceType="Rook";PieceSet=sc.BlackSet; PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = Black_rook1;break;
		case "Black_pawn": PieceSprite.GetComponent<SpriteRenderer>().sprite = Black_pawn; player = "Black";HP=BHP_Pawn;ATK=BATK_Pawn;PieceType="Pawn";PieceSet=sc.BlackSet; PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = Black_pawn1;break;
		
		case "White_queen": PieceSprite.GetComponent<SpriteRenderer>().sprite = White_queen; player = "White";HP=WHP_Queen;ATK=WATK_Queen;PieceType="Queen";PieceSet=sc.WhiteSet; PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = White_queen1;break;
	    case "White_knight": PieceSprite.GetComponent<SpriteRenderer>().sprite = White_knight; player = "White";HP=WHP_Knight;ATK=WATK_Knight;PieceType="Knight";PieceSet=sc.WhiteSet; PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = White_knight1;break;
	    case "White_bishop": PieceSprite.GetComponent<SpriteRenderer>().sprite = White_bishop; player = "White";HP=WHP_Bishop;ATK=WATK_Bishop;PieceType="Bishop";PieceSet=sc.WhiteSet; PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = White_bishop1;break;
		case "White_king": PieceSprite.GetComponent<SpriteRenderer>().sprite = White_king; player = "White";HP=WHP_King;ATK=WATK_King;PieceType="King";PieceSet=sc.WhiteSet; PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = White_king1;break;
		case "White_rook": PieceSprite.GetComponent<SpriteRenderer>().sprite = White_rook; player = "White";HP=WHP_Rook;ATK=WATK_Rook;PieceType="Rook";PieceSet=sc.WhiteSet; PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = White_rook1;break;
		case "White_pawn": PieceSprite.GetComponent<SpriteRenderer>().sprite = White_pawn; player = "White";HP=WHP_Pawn;ATK=WATK_Pawn;PieceType="Pawn";PieceSet=sc.WhiteSet; PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = White_pawn1;break;
		
		//To create pieces already promoted
		case "White_marshall": player="White"; PieceSet="US"; Promote(false); PieceSet=sc.WhiteSet; break;
        case "White_lord": player="White"; PieceSet="British"; Promote(false); PieceSet=sc.WhiteSet; break;
        case "White_mortar": player="White"; PieceSet="Russian"; Promote(false); PieceSet=sc.WhiteSet; break;
        case "White_guard": player="White"; PieceSet="Chinese"; Promote(false); PieceSet=sc.WhiteSet; break;
        case "White_chancellor": player="White"; PieceSet="German"; Promote(false); PieceSet=sc.WhiteSet; break;
        case "White_dragon": player="White"; PieceSet="Japanese"; Promote(false); PieceSet=sc.WhiteSet; break;
        case "White_paladin": player="White"; PieceSet="French"; Promote(false); PieceSet=sc.WhiteSet; break;
        case "White_cardinal": player="White"; PieceSet="Italian"; Promote(false); PieceSet=sc.WhiteSet; break;

        case "Black_marshall": player="Black"; PieceSet="US"; Promote(false); PieceSet=sc.BlackSet; break;
        case "Black_lord": player="Black"; PieceSet="British"; Promote(false); PieceSet=sc.BlackSet; break;
        case "Black_mortar": player="Black"; PieceSet="Russian"; Promote(false); PieceSet=sc.BlackSet; break;
        case "Black_guard": player="Black"; PieceSet="Chinese"; Promote(false); PieceSet=sc.BlackSet; break;
        case "Black_chancellor": player="Black"; PieceSet="German"; Promote(false); PieceSet=sc.BlackSet; break;
        case "Black_dragon": player="Black"; PieceSet="Japanese"; Promote(false); PieceSet=sc.BlackSet; break;
        case "Black_paladin": player="Black"; PieceSet="French"; Promote(false); PieceSet=sc.BlackSet; break;
        case "Black_cardinal": player="Black"; PieceSet="Italian"; Promote(false); PieceSet=sc.BlackSet; break;		
	   }

        switch (this.PieceType)
	   {
		case "Queen": PieceSpriteGlow.GetComponent<SpriteRenderer>().sprite = glow_queen; break;
	    case "Knight": PieceSpriteGlow.GetComponent<SpriteRenderer>().sprite = glow_knight;break;
	    case "Bishop": PieceSpriteGlow.GetComponent<SpriteRenderer>().sprite = glow_bishop;break;
		case "King": PieceSpriteGlow.GetComponent<SpriteRenderer>().sprite = glow_king;break;
		case "Rook": PieceSpriteGlow.GetComponent<SpriteRenderer>().sprite = glow_rook;break;
		case "Pawn": PieceSpriteGlow.GetComponent<SpriteRenderer>().sprite = glow_pawn;break;				
	   }

        switch (this.PieceType)
	   {
		case "Queen": this.Value = 130; break;
	    case "Knight": this.Value = 30; break;
	    case "Bishop": this.Value = 35; break;
		case "King": this.Value = 120; break;
		case "Rook": this.Value = 50; break;
		case "Pawn": this.Value = 10; break;				
	   }	   
   }
    
	
	
	//adjust the coordinates to relatives to the position of the board when in 0,0
    public void SetCoords() 
	{
		float x = xBoard;
		float y = yBoard;
		
		
		//this will call the values from VariablesResolutions
		VariablesResolutions varres = controller.GetComponent<VariablesResolutions>();		
		varres.VariablesForCoords (out float a, out float b);
		
		x *= a;  
		y *= a;
		
		x += b;
		y += b;
		
		this.transform.position = new Vector3(x,y,74.0f);
	}
	
	public int GetXBoard()
	{
		return xBoard;
	}
	
	public int GetYBoard()
	{
		return yBoard;
	}
	
	public void SetXBoard(int x)
	{
		xBoard = x;
	}
	
	public void SetYBoard(int y)
	{
		yBoard = y;
	}
	
	public bool ThisInAnAttackMP()
	{
		if (controller.GetComponent<Game>().IsAIPlaying()){return false;}
		if (cpmp==null){return false;}
        if (cpmp!=null && gameObject!=cpmp.GetComponent<MovePlate>().cp){return false;}		
		return true;        		
	}
	
	//this is what happens when you clic on a piece
	public void OnMouseUp()
	{
		//this resets Piece Targeted when you click on another piece			
		if (gameObject!= controller.GetComponent<Game>().PieceTargeted) {controller.GetComponent<Game>().PieceTargeted=null;}
		
		//this is to avoid clicking on the piece behind the moveplate
		//it checks currentAttack so it doesnt bypass the fact that the BoxCollider is deactivated for currentAttack
		if (ThisInAnAttackMP()&& !cpmp.GetComponent<MovePlate>().currentAttack){cpmp.GetComponent<MovePlate>().OnMouseUp();}
		
		//This create new moveplates if game is not over
	    //The moveplates are created for any piece, but in MovePlate script its allowed to play only if the piece is of the player with the turn
		//this won't work if this piece is in an attack moveplate
		if ( !ThisInAnAttackMP() && !controller.GetComponent<Game>().IsGameOver()) 
		{
			DestroyMovePlates();
		
		    InitiateMovePlates();
			
			WriteStats();			
		}

        //this executes the Selected animation
        //the animation is also stopped on Next Turn		
	    if ( !ThisInAnAttackMP() && controller.GetComponent<Game>().PieceSelected!=gameObject)
        {
			if (controller.GetComponent<Game>().PieceSelected==null){controller.GetComponent<Game>().PieceSelected=gameObject;}//this assigns the first selected piece
			controller.GetComponent<Game>().PieceSelected.GetComponent<Animator>().SetBool("isSelected", false); //this stops the selected animation of the previous piece selected
			controller.GetComponent<Game>().PieceSelected=gameObject;
			
		    if (controller.GetComponent<Game>().IsAIPlaying()==false)//this checks if the AI is playing so it doesnt play the animation	
			{this.GetComponent<Animator>().SetBool("isSelected", true);} //this starts the selected animation
		}

		if ((PlayerPrefs.GetString("Tut MoveChart") == "Read") && !controller.GetComponent<Game>().IsAIPlaying() && !controller.GetComponent<Game>().IsAttackPhase() )
		{ controller.GetComponent<Tutorial>().ShowTutorial("Stats"); }

		if ((PlayerPrefs.GetString("Tut Move") == "Read") && !controller.GetComponent<Game>().IsAIPlaying() && !controller.GetComponent<Game>().IsAttackPhase() && this.PieceType == "Pawn")
		{ controller.GetComponent<Tutorial>().ShowTutorial("MoveChart"); } //so it happens the next time you click a piece, only with a pawn so it explains the dots

		if (!controller.GetComponent<Game>().IsAIPlaying() && !controller.GetComponent<Game>().IsAttackPhase() && (this.PieceType == "Pawn" || this.PieceType == "Knight") )
		{ controller.GetComponent<Tutorial>().ShowTutorial("Move"); } //only in Pawns or Knights, cause others wont show a moveplate at start

	}	
	
	
	//This will put the information in the stat window
	public void WriteStats()
	{
		int p = controller.GetComponent<Game>().Phase;		
		bool iap = controller.GetComponent<Game>().IsAIPlaying();
		if( (p!=2) && (p!=4) && (iap == false) )  
		{	
        controller.GetComponent<Game>().WStats.SetActive(true);	    
	    controller.GetComponent<Game>().ColorStats(GameObject.FindGameObjectWithTag("WStats"),gameObject); //this applies ColorStats to the Stats Window
        GameObject.FindGameObjectWithTag("WStatsPortrait").GetComponent<Image>().sprite = PieceSprite.GetComponent<SpriteRenderer>().sprite;
        GameObject.FindGameObjectWithTag("WStatsName").GetComponent<Text>().text =" "+player+" "+this.PieceType;	
        GameObject.FindGameObjectWithTag("WStatsHP").GetComponent<Text>().text =" "+this.HP;
        GameObject.FindGameObjectWithTag("WStatsATK").GetComponent<Text>().text =" "+this.ATK;
		}
	}
	
	public void BlankStats()//this is obsolete
	{
		GameObject.FindGameObjectWithTag("WStatsName").GetComponent<Text>().text ="Dead Piece";	
        GameObject.FindGameObjectWithTag("WStatsHP").GetComponent<Text>().text =" 0";
        GameObject.FindGameObjectWithTag("WStatsATK").GetComponent<Text>().text =" 0";
	}
	
	//This is the function used above to destroy all existing moveplates
	public void DestroyMovePlates()	
	{	        
		GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
		for (int i=0; i< movePlates.Length; i++)
		{
			Destroy(movePlates[i]);
		}
	}
	
	//This is the function that creates moveplates according to pieces movement
	public void InitiateMovePlates()
	{
		MoveChart mc = mchart.GetComponent<MoveChart>();
		mc.CleanMoveChart();
		
		switch (this.PieceType)
		{
			case "Queen":
			     mc.Line("NxSxWxExNENWSWSE");
			     LineMovePlate(1,0,9);
				 LineMovePlate(0,1,9);
				 LineMovePlate(1,1,9);
				 LineMovePlate(-1,0,9);
				 LineMovePlate(0,-1,9);
				 LineMovePlate(-1,-1,9);
				 LineMovePlate(-1,1,9);
				 LineMovePlate(1,-1,9);
				 if (QueenCanL== true){
					LMovePlate();
                    mc.Horse();					
				 }				 
				 break;
			
			case "Knight":
			     LMovePlate();
                 mc.Horse();                 				 
				 if (KnightsCanSide== true){
					PointMovePlate(xBoard + 1, yBoard + 0);
					PointMovePlate(xBoard - 1, yBoard - 0);
                    mc.DotsColor("White"); mc.PaintDot(5,4,0);mc.PaintDot(3,4,0);					
				 }
				 break;
			
			case "Bishop":
			     if (!BishopCanSkipAllies){
				 mc.Line("NENWSWSE"); 	 
			     LineMovePlate(1,1,9);
				 LineMovePlate(1,-1,9);
				 LineMovePlate(-1,1,9);
				 LineMovePlate(-1,-1,9);}
				 if (BishopCanSkipAllies){
				 mc.DotsColor("Blue");
				 mc.Line("NENWSWSE");	 
			     LineMovePlateSkipAllies(1,1,9);
				 LineMovePlateSkipAllies(1,-1,9);
				 LineMovePlateSkipAllies(-1,1,9);
				 LineMovePlateSkipAllies(-1,-1,9);}
			     if (this.PieceSet=="Japanese"){
					PointMovePlate(xBoard + 1, yBoard + 0);
		            PointMovePlate(xBoard - 1, yBoard - 0);
					mc.DotsColor("White"); mc.PaintDot(5,4,0);mc.PaintDot(3,4,0);
				 }
				 break;
					 
			case "King":
			     if (this.PieceSet!="French"){
                    SurroundMovePlate();
					mc.Surround();}
                 if (this.PieceSet=="French"&& KingCanSkip == false){
					mc.Surround2(); 
			        LineMovePlate(1,0,2);
				    LineMovePlate(0,1,2);
				    LineMovePlate(1,1,2);
				    LineMovePlate(-1,0,2);
				    LineMovePlate(0,-1,2);
				    LineMovePlate(-1,-1,2);
				    LineMovePlate(-1,1,2);
				    LineMovePlate(1,-1,2);}
				 if (KingCanSkip == true){
					mc.DotsColor("Golden");
					mc.Surround2();
					LineMovePlateWithSkip(1,0,2);
				    LineMovePlateWithSkip(0,1,2);
				    LineMovePlateWithSkip(1,1,2);
				    LineMovePlateWithSkip(-1,0,2);
				    LineMovePlateWithSkip(0,-1,2);
				    LineMovePlateWithSkip(-1,-1,2);
				    LineMovePlateWithSkip(-1,1,2);
				    LineMovePlateWithSkip(1,-1,2);}  
                 break;   				 
			
			case "Rook":            			
				 if (!RookCanSkipAllies){
				 mc.Line("NxSxExWx");	 
			     LineMovePlate(1,0,9);
				 LineMovePlate(0,1,9);
				 LineMovePlate(-1,0,9);
				 LineMovePlate(0,-1,9);}
				 if (RookCanSkipAllies){
				 mc.DotsColor("Blue");
				 mc.Line("NxSxExWx");	 
			     LineMovePlateSkipAllies(1,0,9);
				 LineMovePlateSkipAllies(0,1,9);
				 LineMovePlateSkipAllies(-1,0,9);
				 LineMovePlateSkipAllies(0,-1,9);}
				 break;
				 
			case "Pawn":
			    Game sc = controller.GetComponent<Game>();
			    if (this.player=="Black"&&this.PieceSet!="German")
				{
					PointMovePlateNoAttack(xBoard, yBoard - 1);
					mc.PaintDot(4,3,1);
					if (!HasMoved||this.PieceSet=="British")
						{
							mc.PaintDot(4,2,1);
						    if(sc.GetPosition(xBoard, yBoard - 1)==null) {PointMovePlateNoAttack(xBoard, yBoard - 2);}												
						}
					PointMovePlateOnlyAttack(xBoard+1, yBoard - 1);
					PointMovePlateOnlyAttack(xBoard-1, yBoard - 1);
                    mc.PaintDot(3,3,2);mc.PaintDot(5,3,2);					
                    if(PawnCanBack==true){PointMovePlate(xBoard, yBoard + 1);mc.PaintDot(4,5,0);}					
				}
				if (this.player=="White"&&this.PieceSet!="German")
				{
					PointMovePlateNoAttack(xBoard, yBoard + 1);
					mc.PaintDot(4,5,1);
					if (!HasMoved||this.PieceSet=="British") 
						{
							mc.PaintDot(4,6,1);	
							if(sc.GetPosition(xBoard, yBoard + 1)==null) {PointMovePlateNoAttack(xBoard, yBoard + 2);}												
						}
					PointMovePlateOnlyAttack(xBoard+1, yBoard + 1);
					PointMovePlateOnlyAttack(xBoard-1, yBoard + 1);
					mc.PaintDot(3,5,2);mc.PaintDot(5,5,2);
                    if(PawnCanBack==true){PointMovePlate(xBoard, yBoard - 1);mc.PaintDot(4,3,0);}					
				}
				//Berlin Pawns
                if (this.player=="Black"&&this.PieceSet=="German")
				{
					PointMovePlateNoAttack(xBoard+1, yBoard - 1);
					PointMovePlateNoAttack(xBoard-1, yBoard - 1);
                    mc.PaintDot(5,3,1);mc.PaintDot(3,3,1);					
					if (!HasMoved){
						 mc.PaintDot(6,2,1);mc.PaintDot(2,2,1);
						if(sc.PositionOnBoard(xBoard+1,yBoard - 1) && sc.GetPosition(xBoard+1, yBoard - 1)==null)
						{PointMovePlateNoAttack(xBoard+2, yBoard - 2);}
						if(sc.PositionOnBoard(xBoard-1,yBoard - 1) && sc.GetPosition(xBoard-1, yBoard - 1)==null)
						{PointMovePlateNoAttack(xBoard-2, yBoard - 2);}
						          }						
					PointMovePlateOnlyAttack(xBoard, yBoard - 1);
                    mc.PaintDot(4,3,2);					
				}
                if (this.player=="White"&&this.PieceSet=="German")
				{					
					PointMovePlateNoAttack(xBoard+1, yBoard + 1);
					PointMovePlateNoAttack(xBoard-1, yBoard + 1);
					mc.PaintDot(5,5,1);mc.PaintDot(3,5,1);
					if (!HasMoved){
						mc.PaintDot(6,6,1);mc.PaintDot(2,6,1);
						if(sc.PositionOnBoard(xBoard+1,yBoard + 1) && sc.GetPosition(xBoard+1, yBoard + 1)==null)
						{PointMovePlateNoAttack(xBoard+2, yBoard + 2);}
						if(sc.PositionOnBoard(xBoard-1,yBoard + 1) && sc.GetPosition(xBoard-1, yBoard + 1)==null)
						{PointMovePlateNoAttack(xBoard-2, yBoard + 2);}
						          }
					PointMovePlateOnlyAttack(xBoard, yBoard + 1);
                    mc.PaintDot(4,5,2);					
				} 
			    break;

             //Promoted Pieces:
            
			case "Marshall":            			
				 //Bishop movement
				 mc.Line("NENWSWSE");
				 LineMovePlate(1,1,9);
				 LineMovePlate(1,-1,9);
				 LineMovePlate(-1,1,9);
				 LineMovePlate(-1,-1,9);
				 //Knight movement
				 LMovePlate();
				 mc.Horse();
				 break;	

            case "Lord":            			
				 //Queen movement with skip
				 mc.DotsColor("Golden");
				 mc.Line("NxSxWxExNENWSWSE");
				 LineMovePlateWithSkip(1,0,9);
				 LineMovePlateWithSkip(0,1,9);
				 LineMovePlateWithSkip(1,1,9);
				 LineMovePlateWithSkip(-1,0,9);
				 LineMovePlateWithSkip(0,-1,9);
				 LineMovePlateWithSkip(-1,-1,9);
				 LineMovePlateWithSkip(-1,1,9);
				 LineMovePlateWithSkip(1,-1,9);
				 break;
            
            case "Mortar":            			
				 //Rook movement with skip
				 mc.DotsColor("Golden");
				 mc.Line("NxSxExWx");
				 LineMovePlateWithSkip(1,0,9);
				 LineMovePlateWithSkip(0,1,9);
				 LineMovePlateWithSkip(-1,0,9);
				 LineMovePlateWithSkip(0,-1,9);
				 break;	

            case "Guard":            			
				 //Move 2 with skip
				 mc.DotsColor("Golden");
				 mc.Surround2();
				 LineMovePlateWithSkip(1,0,2);
				 LineMovePlateWithSkip(0,1,2);
				 LineMovePlateWithSkip(1,1,2);
				 LineMovePlateWithSkip(-1,0,2);
				 LineMovePlateWithSkip(0,-1,2);
				 LineMovePlateWithSkip(-1,-1,2);
				 LineMovePlateWithSkip(-1,1,2);
				 LineMovePlateWithSkip(1,-1,2);
				 break;	

            case "Chancellor":            			
				 //Rook movement
				 mc.Line("NxSxExWx");
				 LineMovePlate(1,0,9);
				 LineMovePlate(0,1,9);
				 LineMovePlate(-1,0,9);
				 LineMovePlate(0,-1,9);
				 //Knight movement
				 mc.Horse();
				 LMovePlate();
				 break;	

            case "Dragon":            			
				 //Rook movement
				 mc.Line("NxSxExWx");
				 LineMovePlate(1,0,9);
				 LineMovePlate(0,1,9);
				 LineMovePlate(-1,0,9);
				 LineMovePlate(0,-1,9);
				 //1 move diagonal
                 mc.PaintDot(3,3,0);mc.PaintDot(3,5,0);mc.PaintDot(5,3,0);mc.PaintDot(5,5,0);				 
		         PointMovePlate(xBoard - 1, yBoard - 1);		         
		         PointMovePlate(xBoard - 1, yBoard + 1);
		         PointMovePlate(xBoard + 1, yBoard - 1);		         
		         PointMovePlate(xBoard + 1, yBoard + 1);
				 break;

			case "Paladin":            			
				 //Knight movement
				 mc.Horse();
				 LMovePlate();
				 //3/2 move
       			 mc.PaintDot(6,7,0);mc.PaintDot(2,7,0);mc.PaintDot(7,6,0);mc.PaintDot(7,2,0);
                 mc.PaintDot(6,1,0);mc.PaintDot(2,1,0);mc.PaintDot(1,6,0);mc.PaintDot(1,2,0);				 
		         PointMovePlate(xBoard + 2, yBoard + 3);
		         PointMovePlate(xBoard - 2, yBoard + 3);
		         PointMovePlate(xBoard + 3, yBoard + 2);
		         PointMovePlate(xBoard + 3, yBoard - 2);
		         PointMovePlate(xBoard + 2, yBoard - 3);
		         PointMovePlate(xBoard - 2, yBoard - 3);
		         PointMovePlate(xBoard - 3, yBoard + 2);
		         PointMovePlate(xBoard - 3, yBoard - 2);
				 break;
 
            case "Cardinal":
				 //Bishop movement with skip
				 mc.DotsColor("Golden");
				 mc.Line("NENWSWSE");
				 LineMovePlateWithSkip(1,1,9);
				 LineMovePlateWithSkip(1,-1,9);
				 LineMovePlateWithSkip(-1,1,9);
				 LineMovePlateWithSkip(-1,-1,9);			 
				 
				 break; 
			
            case "PAWN"://used on Campaign
			     mc.Surround();
                 SurroundMovePlate();
                 break;				 
		}
	}
	
	//This is used by InitiateMovePlates to create line movements, the limit is the amount of squares it can move
	public void LineMovePlate(int xIncrement, int yIncrement, int Limit)
	{
		Game sc = controller.GetComponent<Game>();
		
		if (PlayerPrefs.GetString("Tut MoveChart") == "Read" && !controller.GetComponent<Game>().IsAIPlaying() && !controller.GetComponent<Game>().IsAttackPhase()) 
		    { controller.GetComponent<Tutorial>().ShowTutorial("MoveChartLine"); }
		
		int x = xBoard + xIncrement;
		int y = yBoard + yIncrement;
		int i = 0;
		
		while (sc.PositionOnBoard(x,y) && sc.GetPosition(x,y) == null && i<Limit)
		{
			MovePlateSpawn(x,y);
			x += xIncrement;
			y += yIncrement;
			i = i+1;
		}
		
		if (sc.PositionOnBoard(x,y) && i!=Limit && sc.GetPosition(x,y).GetComponent<Chessman>().player != player)
		{			
			MovePlateAttackSpawn(x,y);
            x += xIncrement;
			y += yIncrement;
			i = i+1;			
		}
		
		while (sc.PositionOnBoard(x,y) && i<Limit)
		{
			MovePlateUnplayableSpawn(x,y);
			x += xIncrement;
			y += yIncrement;
			i = i+1;
		}
		
		
	}
	
	//This is used by InitiateMovePlates to create line movements, but can skip only allies
	public void LineMovePlateSkipAllies(int xIncrement, int yIncrement, int Limit)
	{
		Game sc = controller.GetComponent<Game>();

		if (PlayerPrefs.GetString("Tut MoveChart") == "Read" && !controller.GetComponent<Game>().IsAIPlaying() && !controller.GetComponent<Game>().IsAttackPhase()) 
		{ controller.GetComponent<Tutorial>().ShowTutorial("MoveChartSkipAllies"); }

		int x = xBoard + xIncrement;
		int y = yBoard + yIncrement;
		int i = 0;
		
		while (sc.PositionOnBoard(x,y) && i<Limit && (sc.GetPosition(x,y) == null || sc.GetPosition(x,y).GetComponent<Chessman>().player == player))
		{
			if (sc.GetPosition(x,y) == null){MovePlateSpawn(x,y);}
			else if (sc.GetPosition(x,y).GetComponent<Chessman>().player == player){MovePlateUnplayableSpawn(x,y);}
			x += xIncrement;
			y += yIncrement;
			i = i+1;
		}
		
		if (sc.PositionOnBoard(x,y) && i!=Limit && sc.GetPosition(x,y).GetComponent<Chessman>().player != player)
		{
			MovePlateAttackSpawn(x,y);
			x += xIncrement;
			y += yIncrement;
			i = i+1;
		}		
	}
	
	//This is used by InitiateMovePlates to create line movements, but can skip everything
	public void LineMovePlateWithSkip(int xIncrement, int yIncrement, int Limit)
	{
		if (PlayerPrefs.GetString("Tut MoveChart") == "Read" && !controller.GetComponent<Game>().IsAIPlaying() && !controller.GetComponent<Game>().IsAttackPhase()) 
		{ controller.GetComponent<Tutorial>().ShowTutorial("MoveChartSkip"); }

		Game sc = controller.GetComponent<Game>();
		
		int x = xBoard + xIncrement;
		int y = yBoard + yIncrement;
		int i = 0;
		
		while (sc.PositionOnBoard(x,y) && i<Limit)
		{
			if (sc.GetPosition(x,y) == null) {MovePlateSpawn(x,y);}
			else if (sc.GetPosition(x,y) != null && sc.GetPosition(x,y).GetComponent<Chessman>().player != player) {MovePlateAttackSpawn(x,y);}
			else if (sc.GetPosition(x,y) != null && sc.GetPosition(x,y).GetComponent<Chessman>().player == player) {MovePlateUnplayableSpawn(x,y);}
			x += xIncrement;
			y += yIncrement;
			i = i+1;			 
		}						
	}
		
	//This is used by InitiateMovePlates to create L shaped movements
	public void LMovePlate()
	{
		if (PlayerPrefs.GetString("Tut MoveChart") == "Read" && !controller.GetComponent<Game>().IsAIPlaying() && !controller.GetComponent<Game>().IsAttackPhase()) 
		{ controller.GetComponent<Tutorial>().ShowTutorial("MoveChartSkip"); }

		PointMovePlate(xBoard + 1, yBoard + 2);
		PointMovePlate(xBoard - 1, yBoard + 2);
		PointMovePlate(xBoard + 2, yBoard + 1);
		PointMovePlate(xBoard + 2, yBoard - 1);
		PointMovePlate(xBoard + 1, yBoard - 2);
		PointMovePlate(xBoard - 1, yBoard - 2);
		PointMovePlate(xBoard - 2, yBoard + 1);
		PointMovePlate(xBoard - 2, yBoard - 1);
	}
	
	//This is used by InitiateMovePlates to create the king movement
	public void SurroundMovePlate()
	{
		PointMovePlate(xBoard + 0, yBoard + 1);
		PointMovePlate(xBoard + 0, yBoard - 1);
		PointMovePlate(xBoard - 1, yBoard - 1);
		PointMovePlate(xBoard - 1, yBoard - 0);
		PointMovePlate(xBoard - 1, yBoard + 1);
		PointMovePlate(xBoard + 1, yBoard - 1);
		PointMovePlate(xBoard + 1, yBoard - 0);
		PointMovePlate(xBoard + 1, yBoard + 1);
	
	}
	
	//This Function is for movements that are not lines like Kings and Knights
	//Checks that there is no one else in the square and that is on the board
	public void PointMovePlate (int x, int y)
	{
		Game sc = controller.GetComponent<Game>();
		if (sc.PositionOnBoard(x,y))
		{
			GameObject cp = sc.GetPosition(x,y);
			if (cp == null)
			{
				MovePlateSpawn(x,y);
			}
			
		    else if (cp.GetComponent<Chessman>().player == player)
			{
				MovePlateUnplayableSpawn(x,y);
			}
			
			else if (cp.GetComponent<Chessman>().player != player)
			{
				MovePlateAttackSpawn(x,y);
			}
		}		
	}
	
	//this is a point moveplate with no attack possibility
	public void PointMovePlateNoAttack (int x, int y) 
	{
		Game sc = controller.GetComponent<Game>();
		if (sc.PositionOnBoard(x,y))
		{			
			GameObject cp = sc.GetPosition(x,y);
			if (cp == null)
			{
				MovePlateSpawn(x,y);
			}

            else if (cp.GetComponent<Chessman>().player == player)
			{
				MovePlateUnplayableSpawn(x,y);
			}			
		}
	}
	
	//this is a point moveplate with no move possibility, only attack
	public void PointMovePlateOnlyAttack (int x, int y)
	{
		Game sc = controller.GetComponent<Game>();
		if (sc.PositionOnBoard(x,y))
		{			
			GameObject cp = sc.GetPosition(x,y);
			if (cp != null && cp.GetComponent<Chessman>().player != player)
			{
				MovePlateAttackSpawn(x,y);
			}
		}
	}

	
	//This function is used by all of the above to spawn a movement plate
	public void MovePlateSpawn(int matrixX, int matrixY)
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
		
		GameObject mp = Instantiate(movePlate, new Vector3(x,y, 74.0f), Quaternion.identity, GameObject.FindGameObjectWithTag("Board").transform);
		
		MovePlate mpScript = mp.GetComponent<MovePlate>();
		mpScript.SetReference(gameObject);
		mpScript.SetCoords(matrixX, matrixY);		
	}
	
	//This function is used by all of the above to spawn an attack plate, the conditions for attack are set in the type of movements
	public void MovePlateAttackSpawn(int matrixX, int matrixY)
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
		
		GameObject mp = Instantiate(movePlate, new Vector3(x,y, 74.0f), Quaternion.identity, GameObject.FindGameObjectWithTag("Board").transform);
		
		MovePlate mpScript = mp.GetComponent<MovePlate>();
		mpScript.attack = true;		
		mpScript.SetReference(gameObject);
		mpScript.SetCoords(matrixX, matrixY);		
	}
	
	public void MovePlateUnplayableSpawn(int matrixX, int matrixY)
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
		
		GameObject mp = Instantiate(movePlate, new Vector3(x,y, 74.0f), Quaternion.identity, GameObject.FindGameObjectWithTag("Board").transform);
		
		MovePlate mpScript = mp.GetComponent<MovePlate>();
		mpScript.playable = false;		
		mpScript.SetReference(gameObject);
		mpScript.SetCoords(matrixX, matrixY);		
	}
	
	public void OutnumberBonus()
	{
		controller.GetComponent<Tutorial>().ShowTutorial("OutnumberBonus");

		audioManager = GameObject.FindGameObjectWithTag("AudioManager");
		audioManager.GetComponent<AudioManager>().PlaySound("Outnumber");
		switch (this.PieceSet)
	   {
		    case "US":
                KnightsCanSide = true;			
			break;
			case "British": 
			    QueenCanL = true;
			break;
			case "Russian": 
			    RookCanSkipAllies = true;
			break;
			case "Chinese":
			    PawnCanBack = true;
			break;
			case "German":
                if (this.PieceType=="Pawn"){this.ATK=this.ATK+1;}			
			break;
			case "Japanese": 
			    if (this.PieceType=="King"){this.ATK=this.ATK+2;}
			break;
			case "French": 
			    KingCanSkip = true;
			break;
			case "Italian": 
			    BishopCanSkipAllies = true;
			break; 
	   }        	   
	}
	
	public void MoraleBonus(string OpponentSet)
	{
		controller.GetComponent<Tutorial>().ShowTutorial("MoraleBonus");

		audioManager = GameObject.FindGameObjectWithTag("AudioManager");
		audioManager.GetComponent<AudioManager>().PlaySound("Morale");
		switch (OpponentSet)
	   {
		    case "US":
                if (this.PieceType=="Knight"){this.HP=this.HP-5;}			
			break;
			case "British": 
			    if (this.PieceType=="Bishop"){this.HP=this.HP-5;} 
			break;
			case "Russian": 
			    if (this.PieceType=="Rook"){this.HP=this.HP-10;}
			break;
			case "Chinese":
			    if (this.PieceType=="King"){this.HP=this.HP-5;}
			break;
			case "German":
                if (this.PieceType=="Knight"){this.ATK=this.ATK-1;}		
			break;
			case "Japanese": 
			    if (this.PieceType=="Rook"){this.ATK=this.ATK-1;}
			break;
			case "French": 
			    if (this.PieceType=="King"){this.ATK=this.ATK-1;}
			break;
			case "Italian": 
			    if (this.PieceType=="Bishop"){this.ATK=this.ATK-1;}
			break; 
	   }        	   
	}
	
	//this will check that a promotion is in order
	public void CheckPromotion()
	{
		if (this.PieceType=="Pawn" && this.player == "White" && this.yBoard==7)
		{
			Promote(true);
		}
	    if (this.PieceType=="Pawn" && this.player == "Black" && this.yBoard==0)
		{
			Promote(true);
		}
	}
		
	
	//this will make the promotion
	public void Promote(bool RealPromotion)
	{// RealPromotion == false when is used to create a piece already promoted		

		if (RealPromotion) 
		{
		audioManager = GameObject.FindGameObjectWithTag("AudioManager");
		audioManager.GetComponent<AudioManager>().PlaySound("Promotion");
		
		controller.GetComponent<Tutorial>().ShowTutorial("Promotion");
        // the tutorial only happens when its a RealPromotion
		}
		
		switch (this.PieceSet)
	   {
		    case "US":
                if (this.player=="Black"){PieceSprite.GetComponent<SpriteRenderer>().sprite = Black_marshall;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = Black_marshall1;}
                if (this.player=="White"){PieceSprite.GetComponent<SpriteRenderer>().sprite = White_marshall;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = White_marshall1;}				
				HP=25;ATK=5;PieceType="Marshall";
				this.Value = 70;
				PieceSpriteGlow.GetComponent<SpriteRenderer>().sprite = glow_marshall;
                this.GetComponent<HPBar>().Start();				
			break;
			case "British": 
			    if (this.player=="Black"){PieceSprite.GetComponent<SpriteRenderer>().sprite = Black_lord;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = Black_lord1;}
                if (this.player=="White"){PieceSprite.GetComponent<SpriteRenderer>().sprite = White_lord;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = White_lord1;} 
				HP=5;ATK=1;PieceType="Lord";
				this.Value = 60;
				PieceSpriteGlow.GetComponent<SpriteRenderer>().sprite = glow_lord;
				this.GetComponent<HPBar>().Start();
			break;
			case "Russian": 
			    if (this.player=="Black"){PieceSprite.GetComponent<SpriteRenderer>().sprite = Black_mortar;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = Black_mortar1;}
                if (this.player=="White"){PieceSprite.GetComponent<SpriteRenderer>().sprite = White_mortar;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = White_mortar1;} 
				HP=20;ATK=5;PieceType="Mortar";
				this.Value = 110;
				PieceSpriteGlow.GetComponent<SpriteRenderer>().sprite = glow_mortar;
				this.GetComponent<HPBar>().Start();
			break;
			case "Chinese":
			    if (this.player=="Black"){PieceSprite.GetComponent<SpriteRenderer>().sprite = Black_guard;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = Black_guard1;}
                if (this.player=="White"){PieceSprite.GetComponent<SpriteRenderer>().sprite = White_guard;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = White_guard1;} 
				HP=15;ATK=4;PieceType="Guard";
				this.Value = 45;
				PieceSpriteGlow.GetComponent<SpriteRenderer>().sprite = glow_guard;
				this.GetComponent<HPBar>().Start();
			break;
			case "German":
                if (this.player=="Black"){PieceSprite.GetComponent<SpriteRenderer>().sprite = Black_chancellor;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = Black_chancellor1;}
                if (this.player=="White"){PieceSprite.GetComponent<SpriteRenderer>().sprite = White_chancellor;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = White_chancellor1;} 
				HP=20;ATK=3;PieceType="Chancellor";
				this.Value = 90;
				PieceSpriteGlow.GetComponent<SpriteRenderer>().sprite = glow_chancellor;
                this.GetComponent<HPBar>().Start();				
			break;
			case "Japanese": 
			    if (this.player=="Black"){PieceSprite.GetComponent<SpriteRenderer>().sprite = Black_dragon;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = Black_dragon1;}
                if (this.player=="White"){PieceSprite.GetComponent<SpriteRenderer>().sprite = White_dragon;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = White_dragon1;} 
				HP=20;ATK=4;PieceType="Dragon";
				this.Value = 65;
				PieceSpriteGlow.GetComponent<SpriteRenderer>().sprite = glow_dragon;
				this.GetComponent<HPBar>().Start();
			break;
			case "French": 
			    if (this.player=="Black"){PieceSprite.GetComponent<SpriteRenderer>().sprite = Black_paladin;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = Black_paladin1;}
                if (this.player=="White"){PieceSprite.GetComponent<SpriteRenderer>().sprite = White_paladin;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = White_paladin1;} 
				HP=25;ATK=3;PieceType="Paladin";
				this.Value = 100;
				PieceSpriteGlow.GetComponent<SpriteRenderer>().sprite = glow_paladin;
				this.GetComponent<HPBar>().Start();
			break;
			case "Italian": 
			    if (this.player=="Black"){PieceSprite.GetComponent<SpriteRenderer>().sprite = Black_cardinal;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = Black_cardinal1;}
                if (this.player=="White"){PieceSprite.GetComponent<SpriteRenderer>().sprite = White_cardinal;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = White_cardinal1;} 
				HP=20;ATK=3;PieceType="Cardinal";
				this.Value = 80;
				PieceSpriteGlow.GetComponent<SpriteRenderer>().sprite = glow_cardinal;
				this.GetComponent<HPBar>().Start();
			break;
            case "KCA":
			    if (this.player=="Black"){PieceSprite.GetComponent<SpriteRenderer>().sprite = Black_guard;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = Black_guard1;}
                if (this.player=="White"){PieceSprite.GetComponent<SpriteRenderer>().sprite = White_guard;PieceSpriteBlink.GetComponent<SpriteRenderer>().sprite = White_guard1;} 
				HP=15;ATK=4;PieceType="Guard";
				this.Value = 45;
				PieceSpriteGlow.GetComponent<SpriteRenderer>().sprite = glow_guard;
				this.GetComponent<HPBar>().Start();
            break;			
	   }
		
		CheckPromotePawns();//this checks if the pawns left can be promoted after promoting this piece 
	}
	
	//The KCA check works so the Black set in the campaign (KCA) doesnt get bonuses
	public void Update()
	{
		Game sc = controller.GetComponent<Game>();
		if (this.HP<=0)
		{
			controller.GetComponent<Tutorial>().ShowTutorial("PieceTaken");

			if (this.player=="White"){sc.BlackKills=sc.BlackKills+1;}
		    if (this.player=="Black"){sc.WhiteKills=sc.WhiteKills+1;}

			if ((sc.WhiteKills + sc.BlackKills) == 4) //tutorial about GameOver will appear when 4 pieces are killed
			{
				switch ( PlayerPrefs.GetString("GameType") )
					{
					case "Versus":
						controller.GetComponent<Tutorial>().ShowTutorial("GameOverVersus");
						break;

					case "Campaign":
						controller.GetComponent<Tutorial>().ShowTutorial("GameOverCampaign");
						break;
				    }			
			}

		    if(NumberOfPiecesLeft()==0)//&&this.PieceType!="King")//this calls the winner if you are out of pieces
		    {
			    if (this.player=="White"){sc.Winner("Black");Debug.Log("OutOfPieces");}
		        if (this.player=="Black"){sc.Winner("White");Debug.Log("OutOfPieces");}
		    }
			
			if(this.PieceType=="Pawn")//this only activates when a pawn dies
			{
				CheckPromotePawns();
			}				
			
			if (this.PieceType=="King")//this calls the Winner when the king dies
			{
				if (this.player=="White"){sc.Winner("Black");}
		        if (this.player=="Black"){sc.Winner("White");}
			}
			audioManager.GetComponent<AudioManager>().PlaySoundRndPitch("Death",0.75f,1.25f);
			Destroy(gameObject);
		}		
		
		switch (this.player)
		{
			case "White":
			  if (sc.WhiteKills>=6 && this.ONBonus == false){OutnumberBonus();this.ONBonus = true;
			  GameObject.Find("WhiteOutnumberIcon").GetComponent<SpriteRenderer>().enabled = true;}
			  break;
			case "Black":
			  if (sc.BlackKills>=6 && this.ONBonus == false && this.PieceSet!="KCA"){OutnumberBonus();this.ONBonus = true; //the KCA check is for Campaign
			  GameObject.Find("BlackOutnumberIcon").GetComponent<SpriteRenderer>().enabled = true;}
			  break;
		}
		
		//this function occurs in king and activates the morale bonus checker in game
		if (this.PieceType=="King" && this.HP<=25) 
		{
			switch (this.player)
		    {
			case "White":
			    sc.WhiteMorale = true;
                GameObject.Find("WhiteMoraleIcon").GetComponent<SpriteRenderer>().enabled = true;				
			  break;
			case "Black":
			    sc.BlackMorale = true; 
				GameObject.Find("BlackMoraleIcon").GetComponent<SpriteRenderer>().enabled = true;
			  break;
		    }
		}
		//this function is to activate the morale bonus on all opposing pieces
		switch (this.player)
		{
			case "White":
			  if (sc.BlackMorale == true && this.MBonus == false){MoraleBonus(sc.BlackSet);this.MBonus = true;} 
			  break;
			case "Black":
			  if (sc.WhiteMorale == true && this.MBonus == false){MoraleBonus(sc.WhiteSet);this.MBonus = true;} 
			  break;
		}		
        
		ColorSword();
		ColorSilhouette();		
		ToggleAttackingSymbol();		
		
		CheckPromotion();
		IdlingAnimation();
		
	}
	
	public void IdlingAnimation()
	{
		int IdlingNum = UnityEngine.Random.Range(0, 8000);
		this.GetComponent<Animator>().SetInteger("Idling", IdlingNum);
	}
		
	public int NumberOfPiecesLeft()//this is used to declare a Winner when there are 0 pieces    
	{                              //doesnt count this one as this one dies
		PlayerPieces.Clear();
	    PlayerPieces.TrimExcess();		
		GameObject[] AllPieces = GameObject.FindGameObjectsWithTag("Chessman");
		for (int i=0; i< AllPieces.Length; i++)
		{
			if ((AllPieces[i].GetComponent<Chessman>().player == this.player)&& AllPieces[i]!= gameObject)
			{
				PlayerPieces.Add (AllPieces[i]);				
		    }		
		}		
        return 	PlayerPieces.Count;	
	}
	
    public int NumberOfPawnsLeft()//this is used to check how many pawns are, for promoting
	{                             //doesnt count this one as this one dies
		PlayerPawns.Clear();
	    PlayerPawns.TrimExcess();
		GameObject[] AllPieces = GameObject.FindGameObjectsWithTag("Chessman");
		for (int i=0; i< AllPieces.Length; i++)
		{
			if ((AllPieces[i].GetComponent<Chessman>().player == this.player)&&(AllPieces[i].GetComponent<Chessman>().PieceType == "Pawn")&& AllPieces[i]!= gameObject)
			{
				PlayerPawns.Add (AllPieces[i]);				
		    }		
		}		
        return 	PlayerPawns.Count;
		
	}
	
	public void CheckPromotePawns()
	{
		if (NumberOfPawnsLeft()==1&&this.PieceSet!="Chinese")
				{					
					PlayerPawns[0].GetComponent<Chessman>().Promote(true);
				}
				if (NumberOfPawnsLeft()==2&&this.PieceSet=="Chinese")
				{
					PlayerPawns[0].GetComponent<Chessman>().Promote(true);
					PlayerPawns[1].GetComponent<Chessman>().Promote(true);
				}
	}
	
	////////////CTP and Glow Color functions
	public void ColorSword()
	{
		SwordColor = controller.GetComponent<Game>().GlowColorManager.GetComponent<GlowColorManager>().GetSpecificColor(SwordColorID);
		AttackingSymbol.GetComponent<SpriteRenderer>().color = SwordColor;
	}
	
	public void ColorSilhouette()
	{
		TargetColor = controller.GetComponent<Game>().GlowColorManager.GetComponent<GlowColorManager>().GetSpecificColor(TargetColorID);
		//PieceSpriteGlow.GetComponent<SpriteRenderer>().color = TargetColor;
		PieceSprite.GetComponent<SpriteRenderer>().material.SetColor("_GlowColor", TargetColor); 
		PieceSpriteBlink.GetComponent<SpriteRenderer>().material.SetColor("_GlowColor", TargetColor);
	}	
	
	public void ToggleAttackingSymbol()
    {
		if(CurrentTargetPiece==null)
		{
			AttackingSymbol.SetActive(false);
			this.SwordColorID=0;
	    }
        if(CurrentTargetPiece!=null)
		{
			AttackingSymbol.SetActive(true);
            if (!CTPOnSight)
			{				
		        SpriteRenderer Temp = AttackingSymbol.GetComponent<SpriteRenderer>();				
		        AttackingSymbol.GetComponent<SpriteRenderer>().color = new Color(Temp.color.r, Temp.color.g, Temp.color.b, 0.3f);

				if (PlayerPrefs.GetString("Tut AttackColors") == "Read") { controller.GetComponent<Tutorial>().ShowTutorial("AttackDodge"); }
			}			
	    }		
	}	
	
	IEnumerator CheckCTPOnSight()
	{
		GameObject t = null;
		InitiateMovePlates();
		yield return null;
		MPlates = GameObject.FindGameObjectsWithTag("MovePlate");		
		
		if (CurrentTargetPiece!=null)
		{
			t = Array.Find(MPlates, r => (r.GetComponent<MovePlate>().currentAttack && r.GetComponent<MovePlate>().cp == CurrentTargetPiece) );
		}		
		if (t!=null){DestroyMovePlates(); CTPOnSight = true;}
		else {DestroyMovePlates(); CTPOnSight = false;}
	}
	
	public void DoCheckCTPOnSight()
	{
		StartCoroutine( CheckCTPOnSight() );
	}
	
}
