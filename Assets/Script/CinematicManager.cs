using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
	public GameObject audioManager;	
	public GameObject Gossip, PawnGossip, KnightGossip;
	private bool CinematicCampaignEndDone=false;
	private int Number=0;
	//private bool GossipAnimDone=true;
	
    // Start is called before the first frame update
    void Start()
    {
		audioManager.GetComponent<AudioManager>().PlaySound("Music"); 
        StartCoroutine(WaitAnimtoEnd());		
    }
	
	IEnumerator WaitAnimtoEnd()
	{
		yield return new WaitForSeconds(31);
		CinematicCampaignEndDone=true;
	}

    // Update is called once per frame
    void Update()
    {
        if (CinematicCampaignEndDone){RandomGossip();}
    }
	
	public void RandomGossip()
	{
		Number++;
		//int Number = UnityEngine.Random.Range(0, 1000);
		//Debug.Log(Number+" "+Time.time); 
        if (Number==200) {SetGossipText(); Gossip.GetComponent<Animator>().SetTrigger("Gossip");}
        if (Number==400) {Number=0; Gossip.GetComponent<Animator>().ResetTrigger("Gossip");} 		
				
	}
	
	public void SetGossipText()
	{
		int GossipNumber = UnityEngine.Random.Range(0, 9);
		switch (GossipNumber) //leave 0 as default
		{		
			case 1:
			PawnGossip.GetComponent<TMPro.TextMeshProUGUI>().text="Have you been in a GIF?";
			KnightGossip.GetComponent<TMPro.TextMeshProUGUI>().text="A couple of times, on Saturdays...";
			break;
			
			case 2:
			PawnGossip.GetComponent<TMPro.TextMeshProUGUI>().text="Are we gonna be here long?";
			KnightGossip.GetComponent<TMPro.TextMeshProUGUI>().text="Mmm, I don't know...";
			break;
			
			case 3:
			PawnGossip.GetComponent<TMPro.TextMeshProUGUI>().text="Who is Sky Guy?";
			KnightGossip.GetComponent<TMPro.TextMeshProUGUI>().text="I think he won a contest or something...";
			break;
			
			case 4:
			PawnGossip.GetComponent<TMPro.TextMeshProUGUI>().text="The AI is so dumb!";
			KnightGossip.GetComponent<TMPro.TextMeshProUGUI>().text="I heard they are working on fixing it...";
			break;
			
			case 5:
			PawnGossip.GetComponent<TMPro.TextMeshProUGUI>().text="Do you speak Italian?";
			KnightGossip.GetComponent<TMPro.TextMeshProUGUI>().text="You know I don't.";
			break;
			
			case 6:
			PawnGossip.GetComponent<TMPro.TextMeshProUGUI>().text="What about Chinese? Can you speak Chinese?";
			KnightGossip.GetComponent<TMPro.TextMeshProUGUI>().text="Now you're just showing off...";
			break;
			
			case 7:
			PawnGossip.GetComponent<TMPro.TextMeshProUGUI>().text="That last battle was hard...";
			KnightGossip.GetComponent<TMPro.TextMeshProUGUI>().text="Makes sense, it was the final scenario after all.";
			break;
			
			case 8:
			PawnGossip.GetComponent<TMPro.TextMeshProUGUI>().text="Up there! What does that button do?";
			KnightGossip.GetComponent<TMPro.TextMeshProUGUI>().text="I don't know, someone should press it!";
			break;

            default:
			PawnGossip.GetComponent<TMPro.TextMeshProUGUI>().text="That song sounds familiar.";
			KnightGossip.GetComponent<TMPro.TextMeshProUGUI>().text="Yeah, they used it in a trailer.";
			break;			
		}
	}
	
		
}
