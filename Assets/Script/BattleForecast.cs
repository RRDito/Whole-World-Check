using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleForecast : MonoBehaviour
{
	private GameObject controller;
	private GameObject objective;
	
	public GameObject Themes;	
	public GameObject TargetImage;	
	public GameObject HPText;	
	public GameObject ResultText;	
	public GameObject TargetAll;
	
	
    // Start is called before the first frame update
    void Start()
    {	
        controller = GameObject.FindGameObjectWithTag("GameController");        		
		TargetAll.SetActive(false);
    }
	
	void Update()
	{
		if(controller.GetComponent<Game>().PieceTargeted!=objective)
        {
			Start();
		}
	}
	
	void Activate()
	{		
		TargetAll.SetActive(true);
	}

    public void Forecast(GameObject Target, GameObject Attacker, int HPTarget, int ATKAttacker)
	{
		Activate();
		controller.GetComponent<Game>().ColorStats(TargetAll, Target); //this applies ColorStats to the Forecast Window
		objective = Target;
		TargetImage.GetComponent<Image>().sprite = Target.GetComponent<Chessman>().PieceSprite.GetComponent<SpriteRenderer>().sprite;		
		HPText.GetComponent<Text>().text = ""+HPTarget;		
		int x = (HPTarget-ATKAttacker);
		if (x < 0){x=0;} 
		ResultText.GetComponent<Text>().text = ""+x;
	}
}
