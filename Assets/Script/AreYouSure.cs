using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreYouSure : MonoBehaviour
{
	public GameObject confirm;
    public GameObject decline;
    string textC , textD;
	
    // Start is called before the first frame update
    public void Start()
    {
		GenerateText();
        confirm.GetComponent<Text>().text = textC;
		decline.GetComponent<Text>().text = textD;
    }

    void GenerateText()
	{
		int x = UnityEngine.Random.Range(0, 3);
		int y = UnityEngine.Random.Range(0, 3);
		
		switch (x)
		{
			case 0: textC="Yeah, I was totally winning... but... I gotta go... feed my cat...  Its not an excuse, I swear";                    			
			break;						
			
			case 1: textC="Get me out of here! Enemy pieces are scary!";                     		
			break;

            case 2: textC="I'm just gonna adjust the difficulty settings and I'll be back";                     		
			break; 			
		}
		
		switch (y)
		{
			case 0: textD="Exit? No! I thought this was an UNDO button, get me back to the game!!!";			
			break;						
			
			case 1: textD="I won't quit! I can win this! Right?";			
			break;

            case 2: textD="What? No! Ctrl+Z, Ctrl+Z!";                     		
			break;			
		}
	}
}
