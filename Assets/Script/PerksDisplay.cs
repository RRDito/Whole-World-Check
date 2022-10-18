using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerksDisplay : MonoBehaviour
{
    public string Set;     
	public bool ThisIsWhite;
	private string PerksText;	
	public GameObject PerkOneIcon;
	public GameObject PerkTwoIcon;
	public GameObject PromotionIcon;
	public GameObject PromotionText;    	
	public Sprite King, Queen, Knight, Pawn, Bishop, Rook, Marshall, Lord, Mortar, Guard, Chancellor, Dragon, Paladin, Cardinal;
	private int FSize=18;
	private int FSizeTemp=18;

	public GameObject controller;

	public void Start()
	{
		controller = GameObject.FindGameObjectWithTag("GameController");

		if (ThisIsWhite) {Set = PlayerPrefs.GetString("WhiteSet");
		PerkOneIcon = GameObject.FindGameObjectWithTag("Perk1White");
		PerkTwoIcon = GameObject.FindGameObjectWithTag("Perk2White");
		PromotionIcon = GameObject.FindGameObjectWithTag("PromotionWhite");
		PromotionText = GameObject.FindGameObjectWithTag("PromotionTextWhite");
		}
		if (!ThisIsWhite) {Set = PlayerPrefs.GetString("BlackSet");
		PerkOneIcon = GameObject.FindGameObjectWithTag("Perk1Black");
		PerkTwoIcon = GameObject.FindGameObjectWithTag("Perk2Black");
		PromotionIcon = GameObject.FindGameObjectWithTag("PromotionBlack");
		PromotionText = GameObject.FindGameObjectWithTag("PromotionTextBlack");
		}
		ChangeIcons();
		if ( (!ThisIsWhite) && (PlayerPrefs.GetString("GameType")=="Campaign") ){gameObject.SetActive(false);}
	}

    public void ChangeIcons()
	{
		switch (Set)
		{
			case "US": 
			PerkOneIcon.GetComponent<Image>().sprite = Knight; PerkTwoIcon.GetComponent<Image>().sprite = Knight; 
			PromotionIcon.GetComponent<Image>().sprite = Marshall;
			break;						
			case "British": PerkOneIcon.GetComponent<Image>().sprite = Queen; PerkTwoIcon.GetComponent<Image>().sprite = Pawn;
			PromotionIcon.GetComponent<Image>().sprite = Lord;
			break;			
			case "Russian": PerkOneIcon.GetComponent<Image>().sprite = Rook; PerkTwoIcon.GetComponent<Image>().sprite = Rook;
			PromotionIcon.GetComponent<Image>().sprite = Mortar;
			break;			
			case "Chinese": PerkOneIcon.GetComponent<Image>().sprite = Pawn; PerkTwoIcon.GetComponent<Image>().sprite = Rook;
			PromotionIcon.GetComponent<Image>().sprite = Guard;
			break;			
			case "German": PerkOneIcon.GetComponent<Image>().sprite = King; PerkTwoIcon.GetComponent<Image>().sprite = Pawn;
			PromotionIcon.GetComponent<Image>().sprite = Chancellor;
			break;
			case "Japanese": PerkOneIcon.GetComponent<Image>().sprite = Bishop; PerkTwoIcon.GetComponent<Image>().sprite = Bishop;
			PromotionIcon.GetComponent<Image>().sprite = Dragon;
			break;
			case "French": PerkOneIcon.GetComponent<Image>().sprite = Knight; PerkTwoIcon.GetComponent<Image>().sprite = King;
			PromotionIcon.GetComponent<Image>().sprite = Paladin;
			break;
			case "Italian": PerkOneIcon.GetComponent<Image>().sprite = Bishop; PerkTwoIcon.GetComponent<Image>().sprite = Bishop;
			PromotionIcon.GetComponent<Image>().sprite = Cardinal;
			break;
		}
	}    
	
    public void Update()
	{
        if (ThisIsWhite) {GameObject.FindGameObjectWithTag("WhitePerks").GetComponent<Text>().text = PerksText;
		                  GameObject.FindGameObjectWithTag("WhitePerks").GetComponent<Text>().fontSize=FSizeTemp;  }
        if (!ThisIsWhite) {GameObject.FindGameObjectWithTag("BlackPerks").GetComponent<Text>().text = PerksText;
		                  GameObject.FindGameObjectWithTag("BlackPerks").GetComponent<Text>().fontSize=FSizeTemp;  }        		
	}
	
	public void PerkOne()
	{
		controller.GetComponent<Tutorial>().ShowTutorial("PerkDisplay");

		PromotionText.GetComponent<Text>().text = "";
        FSizeTemp = FSize;		
		switch (Set)
		{
			case "US": PerksText="Knights for Bishops";  
			break;						
			case "British": PerksText="Queen +3 HP";
			break;			
			case "Russian": PerksText="Rooks +5 HP";
			break;			
			case "Chinese": PerksText="Pawns +5 HP, Last 2 Pawns Promote"; FSizeTemp = FSize-6;
			break;			
			case "German": PerksText="King +20 HP";
			break;
			case "Japanese": PerksText="Bishops +2 ATK";
			break;
			case "French": PerksText="Knights +5 HP";
			break;
			case "Italian": PerksText="Bishops +5 HP";
			break;
		}
	}
	
	public void PerkTwo()
	{
		controller.GetComponent<Tutorial>().ShowTutorial("PerkDisplay");

		PromotionText.GetComponent<Text>().text = "";
		FSizeTemp = FSize;
		switch (Set)
		{
			case "US": PerksText="Knights +2 ATK";
			break;
			case "British": PerksText="Pawns +1 Move forward";
			break;
			case "Russian": PerksText="Rooks +2 ATK";
			break;
			case "Chinese": PerksText="Rooks for Bishops, Pawns for Knights"; FSizeTemp = FSize-6;
			break;
			case "German": PerksText="Pawns move diagonally & attack front"; FSizeTemp = FSize-6;
			break;
			case "Japanese": PerksText="Bishops move sideways";
			break;
			case "French": PerksText="King +1 move";
			break;
			case "Italian": PerksText="Bishops for Knights";
			break;
		}
	}
	
	public void BonusO()
	{
		if ((PlayerPrefs.GetString("Tut PerkDisplay") == "Read")) { controller.GetComponent<Tutorial>().ShowTutorial("OutnumberBonus"); }
		
		controller.GetComponent<Tutorial>().ShowTutorial("PerkDisplay");

		PromotionText.GetComponent<Text>().text = "";
		FSizeTemp = FSize;
		switch (Set)
		{
			case "US": PerksText="Knights move sideways";
			break;
			case "British": PerksText="Queen can move as Knights";
			break;
			case "Russian": PerksText="Rooks can skip own pieces";
			break;
			case "Chinese": PerksText="Pawns can move back";
			break;
			case "German": PerksText="Pawns +1 ATK";
			break;
			case "Japanese": PerksText="King +2 ATK";
			break;
			case "French": PerksText="King can skip";
			break;
			case "Italian": PerksText="Bishops can skip own pieces";
			break;
		}		
	}
	
	public void BonusM()
	{
		if ((PlayerPrefs.GetString("Tut PerkDisplay") == "Read")) { controller.GetComponent<Tutorial>().ShowTutorial("MoraleBonus"); }

		controller.GetComponent<Tutorial>().ShowTutorial("PerkDisplay");

		PromotionText.GetComponent<Text>().text = "";
		FSizeTemp = FSize;
		switch (Set)
		{
			case "US": PerksText="Enemy Knights -5 HP";
			break;
			case "British": PerksText="Enemy Bishops -5 HP";
			break;
			case "Russian": PerksText="Enemy Rooks -10 HP";
			break;
			case "Chinese": PerksText="Enemy King -5 HP";
			break;
			case "German": PerksText="Enemy Knights -1 ATK";
			break;
			case "Japanese": PerksText="Enemy Rooks -1 ATK";
			break;
			case "French": PerksText="Enemy King -1 ATK";
			break;
			case "Italian": PerksText="Enemy Bishops -1 ATK";
			break;
		} //use this to win fast to test things       
		//GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>().Winner("White"); 					
	}
	
	public void PromotionUnit()
	{
		if ((PlayerPrefs.GetString("Tut PerkDisplay") == "Read")) { controller.GetComponent<Tutorial>().ShowTutorial("Promotion"); }

		controller.GetComponent<Tutorial>().ShowTutorial("PerkDisplay");

		switch (Set)
		{
			case "US": PerksText="Marshall HP 25  ATK 5";
			PromotionText.GetComponent<Text>().text = "*Moves like a Bishop and a Knight ";
			break;
			case "British": PerksText="Lord HP 5  ATK 1";
			PromotionText.GetComponent<Text>().text = "*Moves like a Queen but can skip any piece";
			break;
			case "Russian": PerksText="Mortar HP 20  ATK 5";
			PromotionText.GetComponent<Text>().text = "*Moves like a Rook but can skip any piece";
			break;
			case "Chinese": PerksText="Guard HP 15  ATK 4";
			PromotionText.GetComponent<Text>().text = "*Moves 2 squares in any direction, can skip any piece";
			break;
			case "German": PerksText="Chancellor HP 20  ATK 3";
			PromotionText.GetComponent<Text>().text = "*Moves like a Rook and a Knight";
			break;
			case "Japanese": PerksText="Dragon HP 20  ATK 4";
			PromotionText.GetComponent<Text>().text = "*Moves like a Rook and 1 square diagonally ";
			break;
			case "French": PerksText="Paladin HP 25  ATK 3";
			PromotionText.GetComponent<Text>().text = "*Moves like a Knight(a 2/1 L) and a 3/2 L ";
			break;
			case "Italian": PerksText="Cardinal HP 20  ATK 3";
			PromotionText.GetComponent<Text>().text = "*Moves like a Bishop but can skip any piece";
			break;
		}
	}
	
	
	
}
