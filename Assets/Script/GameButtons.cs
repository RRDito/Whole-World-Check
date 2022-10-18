using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtons : MonoBehaviour
{
	//public GameObject movePlate;	
	
    public void BackButton()
	{
		DestroyMovePlates();		
		
		switch(PlayerPrefs.GetString("GameType"))
		{
			case "Versus":
			SceneManager.LoadScene("MainMenu");
			break;
			case "Campaign":
			SceneManager.LoadScene("Campaign");
			break;
			case "Data":
			PlayerPrefs.SetString("DataPlaying", "No");
			SceneManager.LoadScene("Data");
			break;
			
            default:
			SceneManager.LoadScene("MainMenu");
			break;
		}		
	}

    public void DestroyMovePlates()	
	{	        
		GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
		for (int i=0; i< movePlates.Length; i++)
		{
			Destroy(movePlates[i]);
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
	
}
