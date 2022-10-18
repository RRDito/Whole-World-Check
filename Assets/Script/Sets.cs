using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sets
{	
    public static int PieceStats(string Set, string Piece, string Stat)	
	{
		int HP_Queen=5, HP_Knight=20, HP_Bishop=15, HP_King=50, HP_Rook=25, HP_Pawn=10;
        int ATK_Queen=8, ATK_Knight=3, ATK_Bishop=4, ATK_King=2, ATK_Rook=3, ATK_Pawn=2;
        
	    
		switch (Set)
	    {
		    case "US": ATK_Knight = ATK_Knight+2;
			break;
			case "British": HP_Queen=HP_Queen+3;
			break;
			case "Russian": HP_Rook=HP_Rook+5; ATK_Rook=ATK_Rook+2;
			break;
			case "Chinese": HP_Pawn=HP_Pawn+5;
			break;
			case "German": HP_King=HP_King+20;
			break;
			case "Japanese": ATK_Bishop=ATK_Bishop+2;
			break;
			case "French": HP_Knight=HP_Knight+5; 
			break;
			case "Italian": HP_Bishop=HP_Bishop+5;
			break; 
	    }
		
		switch (Piece)
	    {
		    case "Queen": if (Stat=="HP"){return HP_Queen;} 
			              if (Stat=="ATK"){return ATK_Queen;}
			break;
			case "Knight": if (Stat=="HP"){return HP_Knight;} 
			               if (Stat=="ATK"){return ATK_Knight;}
			break;
			case "Bishop": if (Stat=="HP"){return HP_Bishop;} 
			               if (Stat=="ATK"){return ATK_Bishop;}
			break;
			case "King": if (Stat=="HP"){return HP_King;} 
			             if (Stat=="ATK"){return ATK_King;}
			break;
			case "Rook": if (Stat=="HP"){return HP_Rook;} 
			             if (Stat=="ATK"){return ATK_Rook;}
			break;
			case "Pawn": if (Stat=="HP"){return HP_Pawn;} 
			             if (Stat=="ATK"){return ATK_Pawn;}
			break;            			
	    }
		
		return 0;
	}
}
