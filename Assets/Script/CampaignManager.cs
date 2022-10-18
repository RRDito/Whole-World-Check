using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CampaignManager : MonoBehaviour
{
	public GameObject audioManager;
	public GameObject ScenarioText;
	public GameObject ScenarioTitle, ScenarioTitle2;
	public GameObject Map;
	public GameObject Scenario1, Scenario2, Scenario3, Scenario4, Scenario5, Scenario6, Scenario7, Scenario8, Scenario9, EndCinematic;
	public GameObject Go1to2,Go2to1,Go2to3,Go3to2,Go3to4,Go4to3,Go4to5,Go5to4,Go5to6,Go6to5,Go6to7,Go7to6,Go7to8,Go8to7,Go8to9,Go9to8;
	public bool Sc1Won, Sc2Won, Sc3Won, Sc4Won, Sc5Won, Sc6Won, Sc7Won, Sc8Won, Sc9Won;
	public GameObject US, British, Russian, Chinese, German, Japanese, French, Italian;
	public string [] ScWinners = new string[9];	
	public Sprite Black_pawn1, Black_marshall1, Black_lord1, Black_mortar1, Black_guard1, Black_chancellor1, Black_dragon1, Black_paladin1, Black_cardinal1;
	
	void Start()
	{
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");		
		audioManager.GetComponent<AudioManager>().ContinueMusic("Main", true, PlayerPrefs.GetFloat("MusicTime"));        
		
		//this makes sure that when you come back from a game this variables are reset        	
		PlayerPrefs.SetString("WhiteAI", "No");
		PlayerPrefs.SetString("BlackAI", "Yes"); //in this mode we need Black AI
		PlayerPrefs.SetString("BlackSet","KCA");
		PlayerPrefs.SetString("TestGame", "No");
		PlayerPrefs.SetFloat("AISpeed", 0.1f);
		PlayerPrefs.SetFloat("AIDifficulty", 3); //so far Im only using this AI difficulty
		
		LoadCampaignData();
		CheckScenarioStart();		
		CheckCampaignState();		
				
	}
	
	public void LoadCampaignData()
	{
		PlayerData data = SaveSystem.LoadData();
			
            if (data!=null)
			{			
			Sc1Won = data.Sc1Won;
		    Sc2Won = data.Sc2Won;
		    Sc3Won = data.Sc3Won;
		    Sc4Won = data.Sc4Won;
		    Sc5Won = data.Sc5Won;
		    Sc6Won = data.Sc6Won;
		    Sc7Won = data.Sc7Won;
		    Sc8Won = data.Sc8Won;
		    Sc9Won = data.Sc9Won;
		
		    ScWinners[0]= data.ScWinners[0];
		    ScWinners[1]= data.ScWinners[1];
			ScWinners[2]= data.ScWinners[2];
			ScWinners[3]= data.ScWinners[3];
			ScWinners[4]= data.ScWinners[4];
			ScWinners[5]= data.ScWinners[5];
			ScWinners[6]= data.ScWinners[6];
			ScWinners[7]= data.ScWinners[7];
			ScWinners[8]= data.ScWinners[8];	    
			}
 			else  //this happens if no file is found
			{
			Sc1Won = false;
			Sc2Won = false;
			Sc3Won = false;
			Sc4Won = false;
			Sc5Won = false;
			Sc6Won = false;
			Sc7Won = false;
			Sc8Won = false;
			Sc9Won = false;        
			ScWinners[0] = "";
			ScWinners[1] = "";
			ScWinners[2] = "";
			ScWinners[3] = "";
			ScWinners[4] = "";
			ScWinners[5] = "";
			ScWinners[6] = "";
			ScWinners[7] = "";
			ScWinners[8] = "";				
			}
		
	}
	
	public void CheckScenarioStart()
	{
		if (!PlayerPrefs.HasKey("Scenario")){PlayerPrefs.SetInt("Scenario",1);}
				
		int sn = PlayerPrefs.GetInt("Scenario");
		
		switch(sn)
		{
			default:
			MoveMap(Scenario1);
			break;
			case 1:
			MoveMap(Scenario1);
			break;
			case 2:
			MoveMap(Scenario2);
			break;
			case 3:
			MoveMap(Scenario3);
			break;
			case 4:
			MoveMap(Scenario4);
			break;
			case 5:
			MoveMap(Scenario5);
			break;
			case 6:
			MoveMap(Scenario6);
			break;
			case 7:
			MoveMap(Scenario7);
			break;
			case 8:
			MoveMap(Scenario8);
			break;
			case 9:
			MoveMap(Scenario9);            			
			break;
		}
		
		SelectScenario(sn);
		WriteScenarioText(sn);
	}
	
	public void BackButton()
	{		
	    PlayerPrefs.SetFloat("MusicTime", audioManager.GetComponent<AudioManager>().GetMusicTime("Main"));
		SceneManager.LoadScene("MainMenu");
	}
	
	public void CinematicButton()
	{        		
	    PlayerPrefs.SetString("GameType","Campaign");
		SceneManager.LoadScene("Cinematics");
	}
	
    public void StartGame()
	{		
		SceneManager.LoadScene("Game");
		PlayerPrefs.SetString("GameType","Campaign");
	}
	
	public void SelectScenario(int x)
	{
		PlayerPrefs.SetString("BlackSet","KCA");
		PlayerPrefs.SetInt("Scenario",x);
		
		AllButtonsNotInteractable();

        switch(x)
		{
			case 1:
			Go1to2.GetComponent<Button>().interactable=true;
			break;
			case 2:
			Go2to1.GetComponent<Button>().interactable=true;
			Go2to3.GetComponent<Button>().interactable=true;
			break;
			case 3:
			Go3to2.GetComponent<Button>().interactable=true;
			Go3to4.GetComponent<Button>().interactable=true;
			break;
			case 4:
			Go4to3.GetComponent<Button>().interactable=true;
			Go4to5.GetComponent<Button>().interactable=true;
			break;
			case 5:
			Go5to4.GetComponent<Button>().interactable=true;
			Go5to6.GetComponent<Button>().interactable=true;
			break;
			case 6:
			Go6to5.GetComponent<Button>().interactable=true;
			Go6to7.GetComponent<Button>().interactable=true;
			break;
			case 7:
			Go7to6.GetComponent<Button>().interactable=true;
			Go7to8.GetComponent<Button>().interactable=true;
			break;
			case 8:
			Go8to7.GetComponent<Button>().interactable=true;
			Go8to9.GetComponent<Button>().interactable=true;
			break;
			case 9:
			Go9to8.GetComponent<Button>().interactable=true;            			
			break;
		}		
	}
	
	public void AllButtonsNotInteractable()
	{
		    Go1to2.GetComponent<Button>().interactable=false;			
			Go2to1.GetComponent<Button>().interactable=false;
			Go2to3.GetComponent<Button>().interactable=false;			
			Go3to2.GetComponent<Button>().interactable=false;
			Go3to4.GetComponent<Button>().interactable=false;			
			Go4to3.GetComponent<Button>().interactable=false;
			Go4to5.GetComponent<Button>().interactable=false;			
			Go5to4.GetComponent<Button>().interactable=false;
			Go5to6.GetComponent<Button>().interactable=false;			
			Go6to5.GetComponent<Button>().interactable=false;
			Go6to7.GetComponent<Button>().interactable=false;			
			Go7to6.GetComponent<Button>().interactable=false;
			Go7to8.GetComponent<Button>().interactable=false;			
			Go8to7.GetComponent<Button>().interactable=false;
			Go8to9.GetComponent<Button>().interactable=false;			
			Go9to8.GetComponent<Button>().interactable=false;			
	}
	
	public void MoveMap(GameObject X)
	{	
        float x = -X.transform.localPosition.x;
		float y = -X.transform.localPosition.y;	
				
		Map.transform.localPosition = new Vector3(x,y,0f);
	}
		
	public void CheckCampaignState()
	{
		if (Sc1Won){Scenario2.SetActive(true);Go1to2.SetActive(true);Go2to1.SetActive(true);Scenario1.GetComponent<Image>().sprite = Black_pawn1;}
		if (Sc2Won){Scenario3.SetActive(true);Go2to3.SetActive(true);Go3to2.SetActive(true);Scenario2.GetComponent<Image>().sprite = Black_guard1;}
		if (Sc3Won){Scenario4.SetActive(true);Go3to4.SetActive(true);Go4to3.SetActive(true);Scenario3.GetComponent<Image>().sprite = Black_lord1;}
		if (Sc4Won){Scenario5.SetActive(true);Go4to5.SetActive(true);Go5to4.SetActive(true);Scenario4.GetComponent<Image>().sprite = Black_chancellor1;}
		
		if (Sc5Won)
		    {
			Scenario5.GetComponent<Image>().sprite = Black_cardinal1;	
			Scenario6.SetActive(true);Scenario7.SetActive(true);Scenario8.SetActive(true);
			Go5to6.SetActive(true);Go6to5.SetActive(true);
			Go6to7.SetActive(true);Go7to6.SetActive(true);
			Go7to8.SetActive(true);Go8to7.SetActive(true);
			}
		if (Sc6Won){Scenario6.GetComponent<Image>().sprite = Black_marshall1;}
        if (Sc7Won){Scenario7.GetComponent<Image>().sprite = Black_paladin1;}
        if (Sc8Won){Scenario8.GetComponent<Image>().sprite = Black_dragon1;}
		
	    if(Sc6Won&&Sc7Won&&Sc8Won){Scenario9.SetActive(true);Go8to9.SetActive(true);Go9to8.SetActive(true);}
		
		if (Sc9Won){Scenario9.GetComponent<Image>().sprite = Black_mortar1; EndCinematic.SetActive(true);}
	}
	
	public void WriteScenarioWinners(int X)
	{
		string test = ScWinners[X];
		if (test.Contains("US")) {US.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);}
		if (!test.Contains("US")) {US.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 0.15f);}
		if (test.Contains("British")) {British.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);}
		if (!test.Contains("British")) {British.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 0.15f);}
		if (test.Contains("Russian")) {Russian.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);}
		if (!test.Contains("Russian")) {Russian.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 0.15f);}
		if (test.Contains("Chinese")) {Chinese.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);}
		if (!test.Contains("Chinese")) {Chinese.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 0.15f);}
		if (test.Contains("German")) {German.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);}
		if (!test.Contains("German")) {German.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 0.15f);}
		if (test.Contains("Japanese")) {Japanese.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);}
		if (!test.Contains("Japanese")) {Japanese.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 0.15f);}
		if (test.Contains("French")) {French.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);}
		if (!test.Contains("French")) {French.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 0.15f);}
		if (test.Contains("Italian")) {Italian.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);}
		if (!test.Contains("Italian")) {Italian.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 0.15f);}        		
	}
    
	
	public void WriteScenarioText(int x)
	{
		switch(x)
		{
			case 1: ScenarioTitle.GetComponent<Text>().text="Scenario 1: \n An Unexpected Attack";
			        ScenarioText.GetComponent<Text>().text="Ok, typically we would write the story of the game here, right? This game doesn't really have a story, so I'm gonna have to create one...\n We need a villain, every good story has one. But I don't wanna get too political or insult anyone... \n So... Let's say they are from Central Antarctica. And this place is cold, very cold. Yes, they are going to be the villains of this story! \n And you... You can use whatever Set you prefer. Thing is, you have an army in a military base in Antarctica, and out of nowhere you get attacked by a group of people with a Flag you have never seen... ";
			        WriteScenarioWinners(0);
			break;
			case 2: ScenarioTitle.GetComponent<Text>().text="Scenario 2: \n Following The Tracks";
			        ScenarioText.GetComponent<Text>().text="That was easy, right? These guys were no match for you! (I mean, I'm trying to compliment you, but let's be honest, it was really easy)\n Now your army wants to know where did these people came from, after all, we are in Antarctica. So you gather some troops and follow the tracks, far from the shores into the heart of the icy continent, where no one lives and temperatures are inhuman. \n The tracks lead you to a big Wall... of Ice... na, not ice, that's already been done. It would make sense, we are in Antarctica, but... Anyway, when you get near the wall, an army cuts your way! They really like to attack without asking questions...";
			        WriteScenarioWinners(1);
			break;
			case 3: ScenarioTitle.GetComponent<Text>().text="Scenario 3: \n Inside the Kingdom";
			        ScenarioText.GetComponent<Text>().text="After the guards are defeated you get a chance to speak. You claim to come in peace, and just want an audience with whoever the boss is. You get invited inside the walls, and is time for an explanation. The King receives you in a hall. You are now in the Kingdom of Central Antarctica, a civilization that grew in the deeps of the continent, uncommunicated with the external world. The last few years scarcity has beeen the norm, that is why they had explorers adventuring far from the Kingdom, to find new resources and food (that explains Scenario 1, see?). When he finishes talking you get sorrounded by soldiers. It doesn't seem like they ever intended to let you leave...";
			        WriteScenarioWinners(2);
			break;
			case 4: ScenarioTitle.GetComponent<Text>().text="Scenario 4: \n A Boring Meeting";
			        ScenarioText.GetComponent<Text>().text="There is no other option, you flee Antarctica. There is no use on being there with such hostility. Besides... technically... you were trespassing their territory. Not knowingly, but you were. Now you tell the rest of the world about this Kingdom that has been discovered. \n An international organization that is tasked with uniting the nations, lets call it the Whole World Council, they have invited a representation of the Kingdom of Central Antarctica to a meeting in US, to discuss its inclusion on the WWC and frontiers, and politics and all the boring stuff. But there is nothing boring when they are involved, in the middle of the meeting, out of the blue, they attack...";
			        WriteScenarioWinners(3);
			break;
			case 5: ScenarioTitle.GetComponent<Text>().text="Scenario 5: \n The Invasion Starts";
			        ScenarioText.GetComponent<Text>().text="Why would they attack? Also... What were you expecting? Haven't you learned already? You interrogate the defeated representants of the Kingdom of Central Antarctica. \n It turns out that, now that they know of the world outside Antarctica, they don't want to stay there anymore. They are a military kingdom, born out of several warring clans, that have endured the harshest conditions during their whole history. And now they intend to conquer the rest of the World. They are also under the impression that the Whole World Council are the kings of the world, and are attacking their central offices, in Switzerland. You will not allow it, and send your army, to stop them... ";
			        WriteScenarioWinners(4);
			break;
			case 6: ScenarioTitle.GetComponent<Text>().text="Scenario 6: \n Battle of Brazil";
			        ScenarioText.GetComponent<Text>().text="The Invasion has already started. They have swiftly attacked and conquered the closest landmasses they could find, and made them their advantage points: South America, South of Africa and South East Asia. \n Brazil must be freed. Its the hub of their Invasion to the Americas. Granted, Chile and Argentina may be closer to Antarctica... but this Campaign Icon looked better on Brazil. \n You send an army, which quickly makes contact with some resistance forces in the region. With their help you are able to get to the headquarters of the invading forces and face their fiercest warriors. I mean, their fiercest warriors that are in Brazil...";
			        WriteScenarioWinners(5);
			break;
			case 7: ScenarioTitle.GetComponent<Text>().text="Scenario 7: \n Battle of South Africa";
			        ScenarioText.GetComponent<Text>().text="The Invasion has already started. They have swiftly attacked and conquered the closest landmasses they could find, and made them their advantage points: South America, South of Africa and South East Asia. \n South Africa is their main base of operations in the african continent. From there, they are launching attacks to the rest of Africa but havent been able to conquer much. Their front is divided, fighting in the frontiers with Namibia, Botswana, Zimbabwe and Mozambique (I'm learning a lot of geography with this game). \n You seize an opportunity and disembark some troops in the south, catching them off guard... but still they will put a fight...";
			        WriteScenarioWinners(6);
			break;
			case 8: ScenarioTitle.GetComponent<Text>().text="Scenario 8: \n Battle of China";
			        ScenarioText.GetComponent<Text>().text="The Invasion has already started. They have swiftly attacked and conquered the closest landmasses they could find, and made them their advantage points: South America, South of Africa and South East Asia. \n They have conquered most of South East Asia and they have now set their sights on China. This has proven to be a costly mistake. South East Asia has been very difficult to maintain under control and they are quickly losing territories that they thought they had already controlled. Now they are surrounded in Hubei (which was the easiest province for me to spell). It is time to attack them, and end with the last remnants of their troops in Asia...";
			        WriteScenarioWinners(7);
			break;
			case 9: ScenarioTitle.GetComponent<Text>().text="Scenario 9: \n Operation Sky Guy";
			        ScenarioText.GetComponent<Text>().text="The Kingdom of Central Antarctica has been succesfully defeated in all of the fronts of their invasion. Or so you thought. Australia has been awfully quiet during all of this, and after a few unreturned calls the Whole World Council realizes that Australia is under the full control of the Antarticians (Antarticans maybe?). \n A coalition is formed and now is time to free Australia. Armies from all the world prepare to fight, in a united effort, called Operation Sky Guy (In honor of the first hero to beat this Campaign). Be prepared, they have their strongest forces here, and it will be very hard... obviously, cause this is the final scenario...";
			        WriteScenarioWinners(8);
			break;			
		}
        ScenarioTitle2.GetComponent<Text>().text=ScenarioTitle.GetComponent<Text>().text;		
	}
}
