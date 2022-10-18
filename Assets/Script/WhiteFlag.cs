using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteFlag : MonoBehaviour
{
    public Sprite FlagUS, FlagBritain, FlagChina, FlagGermany, FlagRussia, FlagJapan, FlagItaly, FlagFrance;
	public Sprite FlagKCA;
	public bool IsThisWhite;
	public string X;
	
	public GameObject controller;
	
    void Start()
    {
		
    }

    void Update()
	{
		WhichFlag();
	}
    
    void WhichFlag()
    {
        Game sc = controller.GetComponent<Game>();
		
		if (IsThisWhite){X=sc.WhiteSet;}
		if (!IsThisWhite){X=sc.BlackSet;}
			
        switch (X)
		{
			case "US": 
			this.GetComponent<SpriteRenderer>().sprite = FlagUS;
			break;
			case "British": 
			this.GetComponent<SpriteRenderer>().sprite = FlagBritain;
			break;
			case "Russian": 
			this.GetComponent<SpriteRenderer>().sprite = FlagRussia;
			break;
			case "Chinese": 
			this.GetComponent<SpriteRenderer>().sprite = FlagChina;
			break;
			case "German": 
			this.GetComponent<SpriteRenderer>().sprite = FlagGermany;
			break;
			case "Japanese": 
			this.GetComponent<SpriteRenderer>().sprite = FlagJapan;
			break;
			case "French": 
			this.GetComponent<SpriteRenderer>().sprite = FlagFrance;
			break;
			case "Italian": 
			this.GetComponent<SpriteRenderer>().sprite = FlagItaly;
			break;
			
			case "KCA": 
			this.GetComponent<SpriteRenderer>().sprite = FlagKCA;
			break;
		}
    }
}
