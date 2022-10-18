using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
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
	
	public PlayerData (ProgressData Progress)
	{
		Sc1Won = Progress.Sc1Won;
		Sc2Won = Progress.Sc2Won;
		Sc3Won = Progress.Sc3Won;
		Sc4Won = Progress.Sc4Won;
		Sc5Won = Progress.Sc5Won;
		Sc6Won = Progress.Sc6Won;
		Sc7Won = Progress.Sc7Won;
		Sc8Won = Progress.Sc8Won;
		Sc9Won = Progress.Sc9Won;
		
		ScWinners[0]= Progress.ScWinners[0];
		ScWinners[1]= Progress.ScWinners[1];
		ScWinners[2]= Progress.ScWinners[2];
		ScWinners[3]= Progress.ScWinners[3];
		ScWinners[4]= Progress.ScWinners[4];
		ScWinners[5]= Progress.ScWinners[5];
		ScWinners[6]= Progress.ScWinners[6];
		ScWinners[7]= Progress.ScWinners[7];
		ScWinners[8]= Progress.ScWinners[8];
	}
}
