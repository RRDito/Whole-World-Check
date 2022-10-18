using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveChart : MonoBehaviour
{
	public Sprite Dot, OnlyA, OnlyM;
	public GameObject South, North, West, East, SEast, SWest, NEast, NWest;
	public GameObject c1r7,c2r7,c3r7,c4r7,c5r7,c6r7,c7r7;
	public GameObject c1r6,c2r6,c3r6,c4r6,c5r6,c6r6,c7r6;
	public GameObject c1r5,c2r5,c3r5,c4r5,c5r5,c6r5,c7r5;
	public GameObject c1r4,c2r4,c3r4,     c5r4,c6r4,c7r4;
	public GameObject c1r3,c2r3,c3r3,c4r3,c5r3,c6r3,c7r3;
	public GameObject c1r2,c2r2,c3r2,c4r2,c5r2,c6r2,c7r2;
	public GameObject c1r1,c2r1,c3r1,c4r1,c5r1,c6r1,c7r1;	
	
	private GameObject[,] Dots = new GameObject[8,8];
	public GameObject controller;
	
	Color DotC = new Color(0.8f, 0.8f, 0.8f, 0.8f); 
	
	void Start()
	{
		//AssignDots();
		CleanMoveChart();        		
	}
	
	public void AssignDots()
	{
		Dots[1,7]=c1r7 ; Dots[2,7]=c2r7 ; Dots[3,7]=c3r7; Dots[4,7]=c4r7; Dots[5,7]=c5r7; Dots[6,7]=c6r7; Dots[7,7]=c7r7;
		Dots[1,6]=c1r6 ; Dots[2,6]=c2r6 ; Dots[3,6]=c3r6; Dots[4,6]=c4r6; Dots[5,6]=c5r6; Dots[6,6]=c6r6; Dots[7,6]=c7r6;
		Dots[1,5]=c1r5 ; Dots[2,5]=c2r5 ; Dots[3,5]=c3r5; Dots[4,5]=c4r5; Dots[5,5]=c5r5; Dots[6,5]=c6r5; Dots[7,5]=c7r5;
		Dots[1,4]=c1r4 ; Dots[2,4]=c2r4 ; Dots[3,4]=c3r4; Dots[4,4]=null; Dots[5,4]=c5r4; Dots[6,4]=c6r4; Dots[7,4]=c7r4;
		Dots[1,3]=c1r3 ; Dots[2,3]=c2r3 ; Dots[3,3]=c3r3; Dots[4,3]=c4r3; Dots[5,3]=c5r3; Dots[6,3]=c6r3; Dots[7,3]=c7r3;
		Dots[1,2]=c1r2 ; Dots[2,2]=c2r2 ; Dots[3,2]=c3r2; Dots[4,2]=c4r2; Dots[5,2]=c5r2; Dots[6,2]=c6r2; Dots[7,2]=c7r2;
		Dots[1,1]=c1r1 ; Dots[2,1]=c2r1 ; Dots[3,1]=c3r1; Dots[4,1]=c4r1; Dots[5,1]=c5r1; Dots[6,1]=c6r1; Dots[7,1]=c7r1;
	}
	
	public GameObject GetDot (int column, int row)
	{   		
		//Method 1 with an array
		//return Dots[column,row];
		
		//Method 2 Finding the names
		string name = column+"x"+row;
		return GameObject.Find(name);
	}
	
	public void CleanMoveChart()
	{
		for (int i=1; i< 8; i++)
		{
			for (int j=1; j< 8; j++)
			{
				if (i==4 && j==4){j++;}
				GameObject X = GetDot(i,j);
				X.GetComponent<Image>().sprite = Dot;
				X.GetComponent<Image>().color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
				DotC = new Color(0.8f, 0.8f, 0.8f, 0.8f);
			}
		}
		South.GetComponent<Image>().color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
		North.GetComponent<Image>().color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
		West.GetComponent<Image>().color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
 		East.GetComponent<Image>().color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
		SEast.GetComponent<Image>().color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
		SWest.GetComponent<Image>().color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
		NEast.GetComponent<Image>().color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
		NWest.GetComponent<Image>().color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
	}
	
	public void CleanChartDuringAttackPhase()
	{
		if((controller.GetComponent<Game>().Phase == 2)||(controller.GetComponent<Game>().Phase == 4)||(controller.GetComponent<Game>().IsAIPlaying()))
		{
			CleanMoveChart(); //this will help for Line
			DotC = new Color (0.0f, 0.0f, 0.0f, 0.0f); //this is for the rest
		}
	}
	
	public void PaintDot(int c, int r, int DotSprite)
	{
		GameObject X = GetDot(c,r);

		CleanChartDuringAttackPhase();
		
		X.GetComponent<Image>().color = DotC;
		
		switch (DotSprite)
		{
			case 2: X.GetComponent<Image>().sprite = OnlyA; break;
			case 1: X.GetComponent<Image>().sprite = OnlyM; break;
			default: X.GetComponent<Image>().sprite = Dot; break;
		}
	}	
	
	public void DotsColor(string C)
	{
		switch(C)
			{
				case "Golden": DotC = new Color(0.5f, 0.4f, 0.1f, 1f);	break;  //for skipping allies and enemies
				case "Blue": DotC = new Color(0.3f, 0.4f, 0.7f, 1f);	break;  //for skipping only allies
				case "White": DotC = new Color(0.8f, 0.8f, 0.8f, 0.8f);	break;			
			}		
	}		
	
	public void Horse()
	{
		DotsColor("Golden");
		PaintDot(3,2,0); PaintDot(5,2,0); PaintDot(3,6,0); PaintDot(5,6,0);
        PaintDot(2,3,0); PaintDot(2,5,0); PaintDot(6,3,0); PaintDot(6,5,0);
	}
	
	public void Line(string Dir)
	{
		//Directions Nx Sx Ex Wx NE NW SE SW
		if (Dir.Contains("Nx")) {PaintDot(4,5,0);PaintDot(4,6,0);PaintDot(4,7,0);North.GetComponent<Image>().color = new Color (0.8f, 0.8f, 0.8f, 0.8f);}
		if (Dir.Contains("Sx")) {PaintDot(4,3,0);PaintDot(4,2,0);PaintDot(4,1,0);South.GetComponent<Image>().color = new Color (0.8f, 0.8f, 0.8f, 0.8f);}
		if (Dir.Contains("Ex")) {PaintDot(5,4,0);PaintDot(6,4,0);PaintDot(7,4,0);East.GetComponent<Image>().color = new Color (0.8f, 0.8f, 0.8f, 0.8f);}
		if (Dir.Contains("Wx")) {PaintDot(3,4,0);PaintDot(2,4,0);PaintDot(1,4,0);West.GetComponent<Image>().color = new Color (0.8f, 0.8f, 0.8f, 0.8f);}
		if (Dir.Contains("NE")) {PaintDot(5,5,0);PaintDot(6,6,0);PaintDot(7,7,0);NEast.GetComponent<Image>().color = new Color (0.8f, 0.8f, 0.8f, 0.8f);}
		if (Dir.Contains("NW")) {PaintDot(3,5,0);PaintDot(2,6,0);PaintDot(1,7,0);NWest.GetComponent<Image>().color = new Color (0.8f, 0.8f, 0.8f, 0.8f);}
		if (Dir.Contains("SE")) {PaintDot(5,3,0);PaintDot(6,2,0);PaintDot(7,1,0);SEast.GetComponent<Image>().color = new Color (0.8f, 0.8f, 0.8f, 0.8f);}
		if (Dir.Contains("SW")) {PaintDot(3,3,0);PaintDot(2,2,0);PaintDot(1,1,0);SWest.GetComponent<Image>().color = new Color (0.8f, 0.8f, 0.8f, 0.8f);}
		
		CleanChartDuringAttackPhase();
	}
	
	public void Surround()
	{
		PaintDot(4,3,0);PaintDot(4,5,0);
		PaintDot(3,4,0);PaintDot(5,4,0);
		PaintDot(3,3,0);PaintDot(5,5,0);
		PaintDot(5,3,0);PaintDot(3,5,0);
	}
	
	public void Surround2()
	{
		Surround();
		PaintDot(4,2,0);PaintDot(4,6,0);
		PaintDot(2,4,0);PaintDot(6,4,0);
		PaintDot(2,2,0);PaintDot(6,6,0);
		PaintDot(6,2,0);PaintDot(2,6,0);		
	}
}
