using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleFlag : MonoBehaviour
{
	public GameObject Themes;
	public GameObject controller;
	public GameObject Flag;
	public GameObject Text1;
	public GameObject Text2;
	
	private string cplayer;
    
    public void Start()
    {
		cplayer = controller.GetComponent<Game>().currentPlayer; 
        if (cplayer == "White")
		{
			Themes.GetComponent<ThemeColors>().ColorTeamWhite(Flag);			
		}
		if (cplayer == "Black")
		{
			Themes.GetComponent<ThemeColors>().ColorTeamBlack(Flag);
		} 
		
		Text1.GetComponent<Text>().text = cplayer+" attacks!";
		Text2.GetComponent<Text>().text = cplayer+" attacks!";
    }

    
}
