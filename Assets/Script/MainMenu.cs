using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public GameObject audioManager;
	
	public bool BlackAI;
	public bool WhiteAI;
    public bool TestAI;
    public bool UnplayableMoves;	
	public GameObject Progress;
	public GameObject AISpeed;
	public GameObject AISpeedText;
	public GameObject AIWhiteIcon;
	public GameObject AIBlackIcon;
	public GameObject AIDifficulty;
	public GameObject AIDifficultyText;
	public GameObject AITestGame;
    public GameObject UnplayMoves;
	public GameObject MVolume;
	public GameObject SVolume;
	
    private GameObject AllOptions;    
	
	private string DifficultyName;
	
	public InputField Reset;
	public GameObject ResetCampaignObj;
	public GameObject ResetConfirmation;
		
	void Awake()
	{
		FirstRunEver();
	}
	
	void FirstRunEver() 
	{
		if (!PlayerPrefs.HasKey("SoundMultiplicator")){PlayerPrefs.SetFloat("SoundMultiplicator", 1);}
		if (!PlayerPrefs.HasKey("MusicMultiplicator")){PlayerPrefs.SetFloat("MusicMultiplicator", 1);}
		if (!PlayerPrefs.HasKey("MusicTime")){PlayerPrefs.SetFloat("MusicTime", 0);}
		if (!PlayerPrefs.HasKey("ThemeNumber")){PlayerPrefs.SetInt("ThemeNumber",1);}
		
		if (!PlayerPrefs.HasKey("WhiteSet")){PlayerPrefs.SetString("WhiteSet","US");}
		if (!PlayerPrefs.HasKey("BlackSet")){PlayerPrefs.SetString("BlackSet","US");}
		//add other prefs that should be default the first time you run the game		
	}
		
	void Start()
	{		
		audioManager = GameObject.FindGameObjectWithTag("AudioManager");		
		audioManager.GetComponent<AudioManager>().ContinueMusic("Main", true, PlayerPrefs.GetFloat("MusicTime"));
		
		//this makes sure that when you come back from a game this variables are reset
		WhiteAI= false;
		BlackAI= false;
		PlayerPrefs.SetString("WhiteAI", "No");
		PlayerPrefs.SetString("BlackAI", "No");
		PlayerPrefs.SetString("TestGame", "No");
		PlayerPrefs.SetFloat("AISpeed", 0.1f);
		PlayerPrefs.SetFloat("AIDifficulty", 1);		
		if (PlayerPrefs.GetString("BlackSet")=="KCA"){PlayerPrefs.SetString("BlackSet","US");}//this is for when coming back from Campaign
		if (PlayerPrefs.GetString("ShowUnplayableMoves")!="Yes"){UnplayMoves.GetComponent<Toggle>().isOn=false;}
            else {UnplayMoves.GetComponent<Toggle>().isOn=true;}		
	}
	
    public void StartGame()
	{		
		SceneManager.LoadScene("Game");
		PlayerPrefs.SetString("GameType","Versus");
	}
	
	public void StartCampaign()
	{
        PlayerPrefs.SetFloat("MusicTime", audioManager.GetComponent<AudioManager>().GetMusicTime("Main"));		
		SceneManager.LoadScene("Campaign");		
	}
	
	public void QuitGame()
	{
		Debug.Log("QUIT");
		PlayerPrefs.SetFloat("MusicTime", 0);
		Application.Quit();
	}	
	
	
	//AI BUTTONS ---------------------------------------------------------
	
	public void BlackAIToggle()
	{
		if (!BlackAI)
		{
			BlackAI= true;
			PlayerPrefs.SetString("BlackAI", "Yes");
			AIBlackIcon.SetActive(true);
		}
		else 
		{
			BlackAI= false;
			PlayerPrefs.SetString("BlackAI", "No");	
            AIBlackIcon.SetActive(false);			
		}
		EnableTestGame();
	}
	
	public void WhiteAIToggle()
	{
		if (!WhiteAI)
		{
			WhiteAI= true;
			PlayerPrefs.SetString("WhiteAI", "Yes");
            AIWhiteIcon.SetActive(true);			
		}
		else 
		{
			WhiteAI= false;
			PlayerPrefs.SetString("WhiteAI", "No");
			AIWhiteIcon.SetActive(false);			
		}
		EnableTestGame();
	}
	
	public void EnableTestGame()
	{
		if (WhiteAI && BlackAI)
		{
		AITestGame.GetComponent<Toggle>().interactable = true;
		}
		else{AITestGame.GetComponent<Toggle>().interactable = false;}
	}
		
	
	public void TestGame()
	{	
        if (!TestAI) {PlayerPrefs.SetString("TestGame", "Yes"); TestAI=true;}
        else {PlayerPrefs.SetString("TestGame", "No"); TestAI=false;} 		
	}
	 
	public void AISpeedDisable()
	{
		AISpeed.GetComponent<Slider>().interactable = !AISpeed.GetComponent<Slider>().interactable;
	}

    public void AISpeedValue()
	{
		float speed = AISpeed.GetComponent<Slider>().value;
		speed = speed/100f;
		PlayerPrefs.SetFloat("AISpeed", speed);		
		AISpeedText.GetComponent<Text>().text= "AI Speed = "+speed+"s";
	}
	
	public void SetDifficultyName(float xdiff)
	{
		switch(xdiff)
		{
			case 1: DifficultyName = "Toddler"; break;
			case 2: DifficultyName = "Angry Toddler"; break;
			case 3: DifficultyName = "Trained Monkey"; break;
			case 4: DifficultyName = "Noob Kid"; break;
		}
	}
	
	public void AIDifficultyValue()
	{
		float difficulty = AIDifficulty.GetComponent<Slider>().value;
		PlayerPrefs.SetFloat("AIDifficulty", difficulty);
        SetDifficultyName(difficulty);		
		AIDifficultyText.GetComponent<Text>().text= DifficultyName;
	}
	
	public void ResetCampaign()
	{
		
		if (Reset.text=="reset")
		{
	    PlayerPrefs.SetInt("Scenario",1);
		Progress.GetComponent<ProgressData>().Sc1Won = false;
		Progress.GetComponent<ProgressData>().Sc2Won = false;
		Progress.GetComponent<ProgressData>().Sc3Won = false;
		Progress.GetComponent<ProgressData>().Sc4Won = false;
		Progress.GetComponent<ProgressData>().Sc5Won = false;
		Progress.GetComponent<ProgressData>().Sc6Won = false;
		Progress.GetComponent<ProgressData>().Sc7Won = false;
		Progress.GetComponent<ProgressData>().Sc8Won = false;
		Progress.GetComponent<ProgressData>().Sc9Won = false;        
	    Progress.GetComponent<ProgressData>().ScWinners[0] = "";
		Progress.GetComponent<ProgressData>().ScWinners[1] = "";
		Progress.GetComponent<ProgressData>().ScWinners[2] = "";
		Progress.GetComponent<ProgressData>().ScWinners[3] = "";
		Progress.GetComponent<ProgressData>().ScWinners[4] = "";
		Progress.GetComponent<ProgressData>().ScWinners[5] = "";
		Progress.GetComponent<ProgressData>().ScWinners[6] = "";
		Progress.GetComponent<ProgressData>().ScWinners[7] = "";
		Progress.GetComponent<ProgressData>().ScWinners[8] = "";		
        SaveSystem.SaveData(Progress.GetComponent<ProgressData>());		
		
		Reset.text="";
        ResetCampaignObj.SetActive(false);		
		ResetConfirmation.SetActive(true);
		}
		
		if (Reset.text=="RRDito")
		{
        Progress.GetComponent<ProgressData>().Sc1Won = true;
		Progress.GetComponent<ProgressData>().Sc2Won = true;
		Progress.GetComponent<ProgressData>().Sc3Won = true;
		Progress.GetComponent<ProgressData>().Sc4Won = true;
		Progress.GetComponent<ProgressData>().Sc5Won = true;
		Progress.GetComponent<ProgressData>().Sc6Won = true;
		Progress.GetComponent<ProgressData>().Sc7Won = true;
		Progress.GetComponent<ProgressData>().Sc8Won = true;
		Progress.GetComponent<ProgressData>().Sc9Won = true;        
	    Progress.GetComponent<ProgressData>().ScWinners[0] = "";
		Progress.GetComponent<ProgressData>().ScWinners[1] = "";
		Progress.GetComponent<ProgressData>().ScWinners[2] = "";
		Progress.GetComponent<ProgressData>().ScWinners[3] = "";
		Progress.GetComponent<ProgressData>().ScWinners[4] = "";
		Progress.GetComponent<ProgressData>().ScWinners[5] = "";
		Progress.GetComponent<ProgressData>().ScWinners[6] = "";
		Progress.GetComponent<ProgressData>().ScWinners[7] = "";
		Progress.GetComponent<ProgressData>().ScWinners[8] = "";
		SaveSystem.SaveData(Progress.GetComponent<ProgressData>());
		
		Reset.text="";
        ResetCampaignObj.SetActive(false);		
		ResetConfirmation.SetActive(true);
		}
		
		if (Reset.text=="Data")
		{	    
        
		PlayerPrefs.SetString("GameType","Data");
        SceneManager.LoadScene("Data");				
		}		
	}
	
	public void UnplayableMovesToggle() //this options is to determine if unplayable moves are shown or not in MovePlate.cs
	{
		if (UnplayMoves.GetComponent<Toggle>().isOn){PlayerPrefs.SetString("ShowUnplayableMoves", "Yes");}
		    else {PlayerPrefs.SetString("ShowUnplayableMoves", "No");}		
	}
	
	public void HideActualOptions()
	{
		GameObject[] ActualOptions = GameObject.FindGameObjectsWithTag("Options");
		for (int i=0; i< ActualOptions.Length; i++)
		{
			ActualOptions[i].SetActive(false);		
		}
	}
	
	public void UpdateMusicAndSoundSliders()
	{
		MVolume.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicMultiplicator");
		SVolume.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SoundMultiplicator");
	}
	
	public void MusicVolume()
	{
		float MusicMultiplicator = MVolume.GetComponent<Slider>().value;		
		PlayerPrefs.SetFloat("MusicMultiplicator", MusicMultiplicator);
		//the next code is to update the volume of the music in the main menu
		audioManager = GameObject.FindGameObjectWithTag("AudioManager");		
		audioManager.GetComponent<AudioManager>().UpdateMusicVolume("Main");
	}
	
	public void SoundVolume()
	{
		float SoundMultiplicator = SVolume.GetComponent<Slider>().value;		
		PlayerPrefs.SetFloat("SoundMultiplicator", SoundMultiplicator);		
	}
	
}
