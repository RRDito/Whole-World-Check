using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressData : MonoBehaviour
{
    public bool Sc1Won;
	public bool Sc2Won;
	public bool Sc3Won;
	public bool Sc4Won;
	public bool Sc5Won;
	public bool Sc6Won;
	public bool Sc7Won;
	public bool Sc8Won;
	public bool Sc9Won;
	public string [] ScWinners = new string[9];	
    
   void Start()//this loads the data into ProgressData from the file, whenever a scene containing ProgressData starts
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
}
