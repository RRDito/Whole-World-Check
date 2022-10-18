using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowColorManager : MonoBehaviour
{
    private GameObject[] Pieces;
	private List<int> ExistingColorIDs = new List<int>();
    
	public int ColorID;
    public Color AC = new Color(0f, 0f, 0f);
	public Color xC = new Color(0f, 0f, 0f);
    
		
	
	void Start()
    {
		ColorID=0;
	}
    
	void Update()
	{
		ClearUnusedTargetColors();
	}
	
	public void FindLowerColorIDAvailable() 
	{
		ListExistingColorIDs();
		
        //this checks what is the lowest number to exist in ExistingColorIDs		
		for (int b=32; b>0; b--)
		{			
			if(!ExistingColorIDs.Contains(b))
			{
				ColorID=b;
			}
		}		
	}
	
	public void ClearUnusedTargetColors()//this will find if there is a piece using a TargetColor but no corresponding SwordColor
	{
		ListExistingColorIDs();
		        		
		for (int c=32; c>0; c--)// for every possible ColorID
		{			
			if(!ExistingColorIDs.Contains(c)) //if it doesnt exist
			{				
		        Pieces = GameObject.FindGameObjectsWithTag("Chessman"); //find all the pieces
				        
		        for (int i=0; i< Pieces.Length; i++)
		        {
			        if(Pieces[i].GetComponent<Chessman>().TargetColorID==c) //and if there is one with that Target Color
			        {
				       Pieces[i].GetComponent<Chessman>().TargetColorID=0; //reset target Color
			        }				
		        }
			}
		}
	}
	
	public void ListExistingColorIDs()
	{
		ExistingColorIDs.Clear();
	    ExistingColorIDs.TrimExcess();
		
		//this adds all of the pieces to Pieces
		Pieces = GameObject.FindGameObjectsWithTag("Chessman");
		
		//this adds all of the Pieces with a GlowColorID to ExistingColorIDs
		for (int i=0; i< Pieces.Length; i++)
		{
			if(Pieces[i].GetComponent<Chessman>().SwordColorID!=0)
			{
				ExistingColorIDs.Add(Pieces[i].GetComponent<Chessman>().SwordColorID);
			}				
		}
	}
		
	public int AssignColorID()
	{
		FindLowerColorIDAvailable();
		return ColorID;
	}
	
	public Color AssignColor()
	{
		AC = GetSpecificColor(ColorID);         
        return AC;		
	}
	
	public Color GetSpecificColor(int SC)
	{
		switch(SC)
		{
			case 0: xC = new Color(0f, 0f, 0f); break;			
			case 1: xC = new Color(1f, 0f, 0f); break; //red
			case 2: xC = new Color(0f, 1f, 0f); break; //green
			case 3: xC = new Color(0f, 0f, 1f); break; //blue
			case 4: xC = new Color(1f, 1f, 0f); break; //yellow
			case 5: xC = new Color(0f, 1f, 1f); break; //cyan
			case 6: xC = new Color(1f, 0.4f, 0f); break; //orange			
			case 7: xC = new Color(1f, 0f, 1f); break; //pink						
            case 8: xC = new Color(0.5f, 0f, 0.7f); break; //purple		
			case 9: xC = new Color(0.5f, 0.5f, 0.5f); break; //gray
			case 10: xC = new Color(0.5f, 0f, 0f); break; //dark red
			case 11: xC = new Color(0f, 0.5f, 0f); break; //dark green
			case 12: xC = new Color(0f, 0f, 0.5f); break; //dark blue					
			default: xC = new Color(1f, 1f, 1f); break; //white
		}        
        return xC;
	}
}
