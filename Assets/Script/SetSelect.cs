using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSelect : MonoBehaviour
{
	private string Set;
	public bool IsThisWhite;
	
	public GameObject SetName;
	public GameObject PerkOneText;
	public GameObject PerkTwoText;
	public GameObject BonusOText;
	public GameObject BonusMText;
	public GameObject PromotionUnitName;
	public GameObject PromotionText;
	public GameObject HeaderBonusO;
	public GameObject HeaderBonusM;
	public GameObject HeaderPromotion;
	public GameObject USFlag, BritishFlag, RussianFlag, ChineseFlag, GermanFlag, JapaneseFlag, FrenchFlag, ItalianFlag;
	public GameObject WaterMarkFlag;
	public GameObject WaterMarkPiece;
	
	public Sprite USBig, BritishBig, RussianBig, ChineseBig, GermanBig, JapaneseBig, FrenchBig, ItalianBig;
	public Sprite Marshall, Lord, Mortar, Guard, Chancellor, Dragon, Paladin, Cardinal;

    private int FSize=18;	
	
	
	public void US(){WritePerksTexts("US");}
	public void British(){WritePerksTexts("British");}
	public void Russian(){WritePerksTexts("Russian");}
	public void Chinese(){WritePerksTexts("Chinese");}
	public void German(){WritePerksTexts("German");}
	public void Japanese(){WritePerksTexts("Japanese");}
	public void French(){WritePerksTexts("French");}
	public void Italian(){WritePerksTexts("Italian");}
	
	void Start()
	{
		if (IsThisWhite) {Set=PlayerPrefs.GetString("WhiteSet");}
		else {Set=PlayerPrefs.GetString("BlackSet");}
		if (Set==null){Set="US";}
		WritePerksTexts(Set);		
	}
	
	
	public void WritePerksTexts(string xSet)
	{
		if (IsThisWhite) {PlayerPrefs.SetString("WhiteSet",xSet);}
		else {PlayerPrefs.SetString("BlackSet",xSet);}
		Set= xSet;
		SetName.GetComponent<Text>().text = Set + " Set:";
		PerkOne();
		PerkTwo();
		BonusO();
		BonusM();
		PromotionUnit();
        HeaderBonusO.GetComponent<Text>().text="Outnumber Bonus:";	
        HeaderBonusM.GetComponent<Text>().text="Morale Bonus:";
		HeaderPromotion.GetComponent<Text>().text="Promotion Unit:"; 
        FlagSelected(Set);
        Watermarks(Set);		
	}	
	
	public void Watermarks(string zSet)
	{
		switch (zSet)
		{
			case "US":
            WaterMarkFlag.GetComponent<Image>().sprite=USBig;			
            WaterMarkPiece.GetComponent<Image>().sprite=Marshall;			
			break;						
			case "British": 
			WaterMarkFlag.GetComponent<Image>().sprite=BritishBig;			
            WaterMarkPiece.GetComponent<Image>().sprite=Lord;
			break;			
			case "Russian": 
			WaterMarkFlag.GetComponent<Image>().sprite=RussianBig;			
            WaterMarkPiece.GetComponent<Image>().sprite=Mortar;
			break;			
			case "Chinese": 
			WaterMarkFlag.GetComponent<Image>().sprite=ChineseBig;			
            WaterMarkPiece.GetComponent<Image>().sprite=Guard;
			break;			
			case "German": 
			WaterMarkFlag.GetComponent<Image>().sprite=GermanBig;			
            WaterMarkPiece.GetComponent<Image>().sprite=Chancellor;
			break;
			case "Japanese": 
			WaterMarkFlag.GetComponent<Image>().sprite=JapaneseBig;			
            WaterMarkPiece.GetComponent<Image>().sprite=Dragon;
			break;
			case "French": 
			WaterMarkFlag.GetComponent<Image>().sprite=FrenchBig;			
            WaterMarkPiece.GetComponent<Image>().sprite=Paladin;
			break;
			case "Italian": 
			WaterMarkFlag.GetComponent<Image>().sprite=ItalianBig;			
            WaterMarkPiece.GetComponent<Image>().sprite=Cardinal;
			break;
		}
	}
	
	public void FlagSelected(string ySet)
	{
		USFlag.transform.localScale = new Vector3(1f, 1f);
		ColorBlock cb1 = USFlag.GetComponent<Button>().colors;
		cb1.normalColor = new Color(1f, 1f, 1f, 0.8f);
        USFlag.GetComponent<Button>().colors = cb1;
		
		BritishFlag.transform.localScale = new Vector3(1f, 1f);
		ColorBlock cb2 = BritishFlag.GetComponent<Button>().colors;
		cb2.normalColor = new Color(1f, 1f, 1f, 0.8f);
        BritishFlag.GetComponent<Button>().colors = cb2;
		
		RussianFlag.transform.localScale = new Vector3(1f, 1f);
		ColorBlock cb3 = RussianFlag.GetComponent<Button>().colors;
		cb3.normalColor = new Color(1f, 1f, 1f, 0.8f);
        RussianFlag.GetComponent<Button>().colors = cb3;
		
		ChineseFlag.transform.localScale = new Vector3(1f, 1f);
		ColorBlock cb4 = ChineseFlag.GetComponent<Button>().colors;
		cb4.normalColor = new Color(1f, 1f, 1f, 0.8f);
        ChineseFlag.GetComponent<Button>().colors = cb4;
		
		GermanFlag.transform.localScale = new Vector3(1f, 1f);
		ColorBlock cb5 = GermanFlag.GetComponent<Button>().colors;
		cb5.normalColor = new Color(1f, 1f, 1f, 0.8f);
        GermanFlag.GetComponent<Button>().colors = cb5;
		
		JapaneseFlag.transform.localScale = new Vector3(1f, 1f);
		ColorBlock cb6 = JapaneseFlag.GetComponent<Button>().colors;
		cb6.normalColor = new Color(1f, 1f, 1f, 0.8f);
        JapaneseFlag.GetComponent<Button>().colors = cb6;
		
		FrenchFlag.transform.localScale = new Vector3(1f, 1f);
		ColorBlock cb7 = FrenchFlag.GetComponent<Button>().colors;
		cb7.normalColor = new Color(1f, 1f, 1f, 0.8f);
        FrenchFlag.GetComponent<Button>().colors = cb7;
		
		ItalianFlag.transform.localScale = new Vector3(1f, 1f);
		ColorBlock cb8 = ItalianFlag.GetComponent<Button>().colors;
		cb8.normalColor = new Color(1f, 1f, 1f, 0.8f);
        ItalianFlag.GetComponent<Button>().colors = cb8;
		
		switch (ySet)
		{
			case "US": USFlag.transform.localScale = new Vector3(1.3f, 1.3f);            
		    cb1.normalColor = new Color(1f, 1f, 1f, 1f);
            USFlag.GetComponent<Button>().colors = cb1;			
			break;						
			case "British": BritishFlag.transform.localScale = new Vector3(1.3f, 1.3f);
			cb2.normalColor = new Color(1f, 1f, 1f, 1f);
            BritishFlag.GetComponent<Button>().colors = cb2;
			break;			
			case "Russian": RussianFlag.transform.localScale = new Vector3(1.3f, 1.3f);
			cb3.normalColor = new Color(1f, 1f, 1f, 1f);
            RussianFlag.GetComponent<Button>().colors = cb3;
			break;			
			case "Chinese": ChineseFlag.transform.localScale = new Vector3(1.3f, 1.3f);
			cb4.normalColor = new Color(1f, 1f, 1f, 1f);
            ChineseFlag.GetComponent<Button>().colors = cb4;
			break;			
			case "German": GermanFlag.transform.localScale = new Vector3(1.3f, 1.3f);
			cb5.normalColor = new Color(1f, 1f, 1f, 1f);
            GermanFlag.GetComponent<Button>().colors = cb5;
			break;
			case "Japanese": JapaneseFlag.transform.localScale = new Vector3(1.3f, 1.3f);
			cb6.normalColor = new Color(1f, 1f, 1f, 1f);
            JapaneseFlag.GetComponent<Button>().colors = cb6;
			break;
			case "French": FrenchFlag.transform.localScale = new Vector3(1.3f, 1.3f);
			cb7.normalColor = new Color(1f, 1f, 1f, 1f);
            FrenchFlag.GetComponent<Button>().colors = cb7;
			break;
			case "Italian": ItalianFlag.transform.localScale = new Vector3(1.3f, 1.3f);
			cb8.normalColor = new Color(1f, 1f, 1f, 1f);
            ItalianFlag.GetComponent<Button>().colors = cb8;
			break;
		}
	}
	
	
	public void PerkOne()
	{
		PerkOneText.GetComponent<Text>().fontSize=FSize;
		switch (Set)
		{
			case "US": PerkOneText.GetComponent<Text>().text="Knights for Bishops";  
			break;						
			case "British": PerkOneText.GetComponent<Text>().text="Queen +3 HP";
			break;			
			case "Russian": PerkOneText.GetComponent<Text>().text="Rooks +5 HP";
			break;			
			case "Chinese": PerkOneText.GetComponent<Text>().fontSize=FSize-4;
							PerkOneText.GetComponent<Text>().text="Pawns +5 HP, Last 2 Pawns Promote";
			break;			
			case "German": PerkOneText.GetComponent<Text>().text="King +20 HP";
			break;
			case "Japanese": PerkOneText.GetComponent<Text>().text="Bishops +2 ATK";
			break;
			case "French": PerkOneText.GetComponent<Text>().text="Knights +5 HP";
			break;
			case "Italian": PerkOneText.GetComponent<Text>().text="Bishops +5 HP";
			break;
		}
	}
	
	public void PerkTwo()
	{
        PerkTwoText.GetComponent<Text>().fontSize=FSize;		
		switch (Set)
		{
			case "US": PerkTwoText.GetComponent<Text>().text="Knights +2 ATK";
			break;
			case "British": PerkTwoText.GetComponent<Text>().text="Pawns +1 Move forward";
			break;
			case "Russian": PerkTwoText.GetComponent<Text>().text="Rooks +2 ATK";
			break;
			case "Chinese": PerkTwoText.GetComponent<Text>().fontSize=FSize-4;
			                PerkTwoText.GetComponent<Text>().text="Rooks for Bishops, Pawns for Knights";
			break;
			case "German":  PerkTwoText.GetComponent<Text>().fontSize=FSize-4;
			                PerkTwoText.GetComponent<Text>().text="Pawns move diagonally & attack front";
			break;
			case "Japanese": PerkTwoText.GetComponent<Text>().text="Bishops move sideways";
			break;
			case "French": PerkTwoText.GetComponent<Text>().text="King +1 move";
			break;
			case "Italian": PerkTwoText.GetComponent<Text>().text="Bishops for Knights";
			break;
		}
	}
	
	public void BonusO()
	{	
        BonusOText.GetComponent<Text>().fontSize=FSize;	
		switch (Set)
		{
			case "US": BonusOText.GetComponent<Text>().text="Knights move sideways";
			break;
			case "British": BonusOText.GetComponent<Text>().text="Queen can move as Knights";
			break;
			case "Russian": BonusOText.GetComponent<Text>().text="Rooks can skip own pieces";
			break;
			case "Chinese": BonusOText.GetComponent<Text>().text="Pawns can move back";
			break;
			case "German": BonusOText.GetComponent<Text>().text="Pawns +1 ATK";
			break;
			case "Japanese": BonusOText.GetComponent<Text>().text="King +2 ATK";
			break;
			case "French": BonusOText.GetComponent<Text>().text="King can skip";
			break;
			case "Italian": BonusOText.GetComponent<Text>().text="Bishops can skip own pieces";
			break;
		}
	}
	
	public void BonusM()
	{
        BonusMText.GetComponent<Text>().fontSize=FSize;		
		switch (Set)
		{
			case "US": BonusMText.GetComponent<Text>().text="Enemy Knights -5 HP";
			break;
			case "British": BonusMText.GetComponent<Text>().text="Enemy Bishops -5 HP";
			break;
			case "Russian": BonusMText.GetComponent<Text>().text="Enemy Rooks -10 HP";
			break;
			case "Chinese": BonusMText.GetComponent<Text>().text="Enemy King -5 HP";
			break;
			case "German": BonusMText.GetComponent<Text>().text="Enemy Knights -1 ATK";
			break;
			case "Japanese": BonusMText.GetComponent<Text>().text="Enemy Rooks -1 ATK";
			break;
			case "French": BonusMText.GetComponent<Text>().text="Enemy King -1 ATK";
			break;
			case "Italian": BonusMText.GetComponent<Text>().text="Enemy Bishops -1 ATK";
			break;
		}
	}
	
	public void PromotionUnit()
	{		
		switch (Set)
		{
			case "US": PromotionUnitName.GetComponent<Text>().text="Marshall HP 25  ATK 5";
			PromotionText.GetComponent<Text>().text = "*Moves like a Bishop and a Knight ";
			break;
			case "British": PromotionUnitName.GetComponent<Text>().text="Lord HP 5  ATK 1";
			PromotionText.GetComponent<Text>().text = "*Moves like a Queen but can skip any piece";
			break;
			case "Russian": PromotionUnitName.GetComponent<Text>().text="Mortar HP 20  ATK 5";
			PromotionText.GetComponent<Text>().text = "*Moves like a Rook but can skip any piece";
			break;
			case "Chinese": PromotionUnitName.GetComponent<Text>().text="Guard HP 15  ATK 4";
			PromotionText.GetComponent<Text>().text = "*Moves 2 squares in any direction, can skip any piece";
			break;
			case "German": PromotionUnitName.GetComponent<Text>().text="Chancellor HP 20  ATK 3";
			PromotionText.GetComponent<Text>().text = "*Moves like a Rook and a Knight";
			break;
			case "Japanese": PromotionUnitName.GetComponent<Text>().text="Dragon HP 20  ATK 4";
			PromotionText.GetComponent<Text>().text = "*Moves like a Rook and 1 square diagonally ";
			break;
			case "French": PromotionUnitName.GetComponent<Text>().text="Paladin HP 25  ATK 3";
			PromotionText.GetComponent<Text>().text = "*Moves like a Knight(a 2/1 L) and a 3/2 L ";
			break;
			case "Italian": PromotionUnitName.GetComponent<Text>().text="Cardinal HP 20  ATK 3";
			PromotionText.GetComponent<Text>().text = "*Moves like a Bishop but can skip any piece";
			break;
		}
	}
	
    
}
