using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeColors : MonoBehaviour
{
    public int ThemeNumber;
	public GameObject Background;
	public GameObject Title;
	public GameObject Banner1;
	public GameObject Banner2;
	public GameObject AIControlCover;
	public GameObject AIControlBackground;
	public GameObject WhiteSelect;
	public GameObject BlackSelect;	
	public GameObject WhiteHandle;
	public GameObject BlackHandle;
	public GameObject AIOptions;
	public GameObject WToggleBG;
	public GameObject BToggleBG;
	public GameObject WToggleCheckmark;
	public GameObject BToggleCheckmark;
	public GameObject TestToggleBG;
	public GameObject TestToggleCheckmark;
	public GameObject AISpeedBG;
	public GameObject AISpeedHandle;
	public GameObject AIDiffBG;
	public GameObject AIDiffHandle;
	public GameObject CloseAIOptions;
	public GameObject Map;
	
	
	private Image Temp;
	
	Color BgC = new Color(0f, 0f, 0f);  //Background color
    Color TitleC = new Color(0f, 0f, 0f); 	//Title and AI button
	Color LiteC = new Color(0f, 0f, 0f);   //PerksText, Board 
	Color CoverC = new Color(0f, 0f, 0f);	 //Covers
	Color DarkC = new Color(0f, 0f, 0f);    //Covers BG and checkmarks
	Color ViewC = new Color(0f, 0f, 0f);    //Scrollviews of SetSelect
	Color HandleC = new Color(0f, 0f, 0f);   //UI Elements and Handles
	Color WTeamC = new Color(0f, 0f, 0f);    //White team in Stats Banner
	Color BTeamC = new Color(0f, 0f, 0f);    //Black team in Stats Banner
	Color MapC = new Color(0f, 0f, 0f);  // Map, MoveChart
	
	void Start()
	{
		ThemeNumber=PlayerPrefs.GetInt("ThemeNumber");
		
		SetColors();	//this sets the default colors of the theme	
		ChangeThemeOld(); //this applies the default colors
		
	}
	
	public void SetColors()
	{
		switch(ThemeNumber)
		{
		case 0:  break;
		case 1:   //Shades of blue
		BgC = new Color(0.1863f, 0.3272f, 0.45f);
        TitleC = new Color(0.5333f, 0.647f, 0.749f); 	
	    LiteC = new Color(0.3137f, 0.5411f, 0.7490f);
	    CoverC = new Color(0.3843f, 0.4666f, 0.5411f);        
        DarkC = new Color(0.1058f, 0.1803f, 0.2509f);
		ViewC = new Color(0.13f, 0.13f, 0.36f);
		HandleC = new Color(0.45f, 0.36f, 0.23f);
		WTeamC = new Color(0.21f, 0.26f, 0.58f);
	    BTeamC = new Color(0.62f, 0.14f, 0.14f);
		MapC = new Color(0.3215f, 0.2078f, 0.149f);
		break;
		
		case 2:    //Shades of green and grey
		BgC = new Color(0.245f, 0.245f, 0.245f);
        TitleC = new Color(0.4183f, 0.7452f, 0.4505f); 	
	    LiteC = new Color(0.2663f, 0.5943f, 0.2986f);  
	    CoverC = new Color(0.31f, 0.55f, 0.33f);        
        DarkC = new Color(0.05f, 0.05f, 0.05f);
		ViewC = new Color(0.21f, 0.45f, 0.23f);
		HandleC = new Color(0.42f, 0.29f, 0.45f);
		WTeamC = new Color(0.1f, 0.45f, 0.17f);
	    BTeamC = new Color(0.56f, 0.53f, 0.12f);
		MapC = new Color(0.3019f, 0.0745f, 0.1490f);
		break;		
		
		case 3:   //Punk (Black, purple and pink)
		BgC = new Color(0.15f, 0.15f, 0.15f);
        TitleC = new Color(0.64f, 0.09f, 0.42f); 	
	    LiteC = new Color(0.42f, 0.07f, 0.42f);
	    CoverC = new Color(0.70f, 0.31f, 0.55f);        
        DarkC = new Color(0.05f, 0.05f, 0.05f);
		ViewC = new Color(0.44f, 0.06f, 0.29f); 
		HandleC = new Color(0.45f, 0.06f, 0.64f);
		WTeamC = new Color(0.56f, 0.0f, 0.7f);
	    BTeamC = new Color(0.93f, 0.13f, 0.0f);
		MapC = new Color(0.1647f, 0.4196f, 0.0274f);
		break;
		
		case 4:   //Blood (White and red)
		BgC = new Color(0.85f, 0.85f, 0.85f);
        TitleC = new Color(0.85f, 0f, 0f); 	
	    LiteC = new Color(0.8f, 0.5f, 0.5f);
	    CoverC = new Color(0.59f, 0.39f, 0.39f);        
        DarkC = new Color(0.5f, 0.5f, 0.5f);
		ViewC = new Color(0.55f, 0.05f, 0.05f);
		HandleC = new Color(0.87f, 0.26f, 0.26f);
		WTeamC = new Color(0.62f, 0.14f, 0.14f);
	    BTeamC = new Color(0.21f, 0.26f, 0.58f);
		MapC = new Color(0.2156f, 0.5019f, 0.2941f);
		break;
		}
		
	}
	
	public void ChangeThemeOld()
	{		
		ChangeColor(Background, BgC);
		ChangeColor(Title, TitleC);
		ChangeColor(Banner1, LiteC);
		ChangeColor(Banner2, LiteC);
		ChangeColor(AIControlCover, CoverC);
		ChangeColor(AIControlBackground, DarkC);
        ChangeColor(WhiteSelect, ViewC);
		ChangeColor(BlackSelect, ViewC);		
		ChangeColor(WhiteHandle, HandleC);
		ChangeColor(BlackHandle, HandleC);
		ChangeColor(AIOptions, HandleC);
		ChangeColor(WToggleBG, HandleC);
		ChangeColor(BToggleBG, HandleC);
		ChangeColor(WToggleCheckmark, DarkC);
		ChangeColor(BToggleCheckmark, DarkC);
		ChangeColor(TestToggleBG, HandleC);
		ChangeColor(TestToggleCheckmark, DarkC);
		ChangeColor(AISpeedBG, CoverC);
		ChangeColor(AISpeedHandle, HandleC);
		ChangeColor(AIDiffBG, CoverC);
		ChangeColor(AIDiffHandle, HandleC);
		ChangeColor(CloseAIOptions, HandleC);
        ChangeColor(Map, MapC);		
	}		
		
	
	public void ChangeColor(GameObject X, Color Y)
	{
		if (X!=null)
		{
		float alfa = X.GetComponent<Image>().color.a;
		X.GetComponent<Image>().color = Y;
		Temp = X.GetComponent<Image>();
		X.GetComponent<Image>().color = new Color(Temp.color.r, Temp.color.g, Temp.color.b, alfa);
		}
	}
	
	public void ColorTeamWhite(GameObject Z) //this colors Z according to the White Team Color
	{
		ChangeColor (Z, WTeamC);
	}
	
	public void ColorTeamBlack(GameObject Z) //this colors Z accorfint to the Black Team Color
	{
		ChangeColor (Z, BTeamC);
	}
	
	public void SelectTheme(int x)
	{		
		ThemeNumber=x;
		SetColors();
		ChangeThemeOld();		
        PlayerPrefs.SetInt("ThemeNumber", ThemeNumber);		
	}
	
}
