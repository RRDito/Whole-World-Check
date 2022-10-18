using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataScreen : MonoBehaviour
{
	
    void Start()
	{
		PlayerPrefs.SetString("WhiteAI", "Yes");
		PlayerPrefs.SetString("BlackAI", "Yes");		
		PlayerPrefs.SetFloat("AISpeed", 0.1f);		
		//RandomDiff();
		PlayerPrefs.SetFloat("AIDifficulty", 4);
        RandomWSet();
		//PlayerPrefs.SetString("WhiteSet","US");
        RandomBSet();
		//PlayerPrefs.SetString("BlackSet","Russian");
		
		if (PlayerPrefs.GetString("DataPlaying") == "Yes")
		{
			SceneManager.LoadScene("Game");
		}
	}
	
	void RandomDiff() 
	{
		int x = UnityEngine.Random.Range(1, 5);
		float y = (float)x;
		PlayerPrefs.SetFloat("AIDifficulty", y);
	}
	
	void RandomWSet()
	{
		int w = UnityEngine.Random.Range(0, 8);
		switch(w)
		{
			case 0: PlayerPrefs.SetString("WhiteSet","US");break;
			case 1: PlayerPrefs.SetString("WhiteSet","British");break;
			case 2: PlayerPrefs.SetString("WhiteSet","Russian");break;
			case 3: PlayerPrefs.SetString("WhiteSet","Chinese");break;
			case 4: PlayerPrefs.SetString("WhiteSet","German");break;
			case 5: PlayerPrefs.SetString("WhiteSet","Japanese");break;
			case 6: PlayerPrefs.SetString("WhiteSet","French");break;
			case 7: PlayerPrefs.SetString("WhiteSet","Italian");break;
		}
	}
	
	void RandomBSet()
	{
		int b = UnityEngine.Random.Range(0, 8);
		switch(b)
		{
			case 0: PlayerPrefs.SetString("BlackSet","US");break;
			case 1: PlayerPrefs.SetString("BlackSet","British");break;
			case 2: PlayerPrefs.SetString("BlackSet","Russian");break;
			case 3: PlayerPrefs.SetString("BlackSet","Chinese");break;
			case 4: PlayerPrefs.SetString("BlackSet","German");break;
			case 5: PlayerPrefs.SetString("BlackSet","Japanese");break;
			case 6: PlayerPrefs.SetString("BlackSet","French");break;
			case 7: PlayerPrefs.SetString("BlackSet","Italian");break;
		}
	}
	
	public void BackButton()
	{
		SceneManager.LoadScene("MainMenu");
	}
	
	public void CollectButton()
	{
		PlayerPrefs.SetString("DataPlaying", "Yes");
		SceneManager.LoadScene("Game");
	}
}
