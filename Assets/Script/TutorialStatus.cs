using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialStatus : MonoBehaviour
{
    public Text TStatus;
	public Text TStatusRead;
	public string ts;
	public string tsr;
	
    void Start()
	{
		WriteStatus();
	}
    	
	void WriteStatus()
	{
		ts = "";
		tsr = "";
		string strike = "\n ------------------------------";
		
		if (PlayerPrefs.GetString("Tut Intro") == "Read") {ts += " Intro"; tsr += " ------------------------------";}
            else {ts += " Intro"; tsr += "";} 
			
		if (PlayerPrefs.GetString("Tut ClickPiece") == "Read") {ts += "\n Click Piece"; tsr += strike;}
            else {ts += "\n Click Piece"; tsr += "\n ";}

        if (PlayerPrefs.GetString("Tut Move") == "Read") {ts += "\n Move"; tsr += strike;}
            else {ts += "\n Move"; tsr += "\n ";}
        
		if (PlayerPrefs.GetString("Tut MoveChart") == "Read") {ts += "\n Move Chart"; tsr += strike;}
            else {ts += "\n Move Chart"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut MoveChartDots") == "Read") {ts += "\n Move Chart: Dots"; tsr += strike;}
            else {ts += "\n Move Chart: Dots"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut MoveChartLine") == "Read") {ts += "\n Move Chart: Lines"; tsr += strike;}
            else {ts += "\n Move Chart: Lines"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut MoveChartSkip") == "Read") {ts += "\n Move Chart: Skip"; tsr += strike;}
            else {ts += "\n Move Chart: Skip"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut MoveChartSkipAllies") == "Read") {ts += "\n Move Chart: Skip Allies"; tsr += strike;}
            else {ts += "\n Move Chart: Skip Allies"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut Stats") == "Read") {ts += "\n Stats"; tsr += strike;}
            else {ts += "\n Stats"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut Attack") == "Read") {ts += "\n Attack"; tsr += strike;}
            else {ts += "\n Attack"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut BattleForecast") == "Read") {ts += "\n Battle Forecast"; tsr += strike;}
            else {ts += "\n Battle Forecast"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut AttackReady") == "Read") {ts += "\n Attack: Ready"; tsr += strike;}
            else {ts += "\n Attack: Ready"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut AttackColors") == "Read") {ts += "\n Attack: Colors"; tsr += strike;}
            else {ts += "\n Attack: Colors"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut AttackDodge") == "Read") {ts += "\n Attack: Dodge"; tsr += strike;}
            else {ts += "\n Attack: Dodge"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut CheckEnemy") == "Read") {ts += "\n Check Enemy Stats"; tsr += strike;}
            else {ts += "\n Check Enemy Stats"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut PerkDisplay") == "Read") {ts += "\n Perks"; tsr += strike;}
            else {ts += "\n Perks"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut OutnumberBonus") == "Read") {ts += "\n Outnumber Bonus"; tsr += strike;}
            else {ts += "\n Outnumber Bonus"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut MoraleBonus") == "Read") {ts += "\n Morale Bonus"; tsr += strike;}
            else {ts += "\n Morale Bonus"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut Promotion") == "Read") {ts += "\n Promotion"; tsr += strike;}
            else {ts += "\n Promotion"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut UniquePieces") == "Read") {ts += "\n Unique Pieces"; tsr += strike;}
            else {ts += "\n Unique Pieces"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut PieceTaken") == "Read") {ts += "\n Piece Taken"; tsr += strike;}
            else {ts += "\n Piece Taken"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut GameOverVersus") == "Read") {ts += "\n Game Over: Versus"; tsr += strike;}
            else {ts += "\n Game Over: Versus"; tsr += "\n ";}
			
		if (PlayerPrefs.GetString("Tut GameOverCampaign") == "Read") {ts += "\n Game Over: Campaign"; tsr += strike;}
            else {ts += "\n Game Over: Campaign"; tsr += "\n ";}
			
		TStatus.text = ts;
		TStatusRead.text = tsr;
	}
	
	public void Reset()
    {
        PlayerPrefs.SetString("Tut Intro", "");
        PlayerPrefs.SetString("Tut ClickPiece", "");
        PlayerPrefs.SetString("Tut Move", "");
        PlayerPrefs.SetString("Tut MoveChart", "");
        PlayerPrefs.SetString("Tut MoveChartDots", "");
        PlayerPrefs.SetString("Tut MoveChartLine", "");
        PlayerPrefs.SetString("Tut MoveChartSkip", "");
        PlayerPrefs.SetString("Tut MoveChartSkipAllies", "");
        PlayerPrefs.SetString("Tut Stats", "");
        PlayerPrefs.SetString("Tut Attack", "");
        PlayerPrefs.SetString("Tut BattleForecast", "");
        PlayerPrefs.SetString("Tut AttackReady", "");
        PlayerPrefs.SetString("Tut AttackColors", "");
        PlayerPrefs.SetString("Tut AttackDodge", "");
        PlayerPrefs.SetString("Tut CheckEnemy", "");
        PlayerPrefs.SetString("Tut PerkDisplay", "");
        PlayerPrefs.SetString("Tut OutnumberBonus", "");
        PlayerPrefs.SetString("Tut MoraleBonus", "");
        PlayerPrefs.SetString("Tut Promotion", "");
        PlayerPrefs.SetString("Tut UniquePieces", "");
        PlayerPrefs.SetString("Tut PieceTaken", "");
        PlayerPrefs.SetString("Tut GameOverVersus", "");
        PlayerPrefs.SetString("Tut GameOverCampaign", "");
		
		Start();        
    }
}
