using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
	//piece is already linked to the piece in which this HPBar is
	public GameObject piece;
	public float BarLength;
	public float TempLength;
	public float LastLength;
	public float MaxHP;
	private float LastHP;
	
	
	
    // Start is called before the first frame update
    public void Start()
    {
		//at the start we can now what is the Max HP for the piece
		MaxHP = piece.GetComponent<Chessman>().HP;
        Transform bar = transform.Find("HPBar");
	    Transform border = transform.Find("HPBarBorder");
		Transform back = transform.Find("HPBarBackground");
		Transform temp = transform.Find("HPBarTemp");
		bar.localScale = new Vector3(0f, 2f);
		temp.localScale = new Vector3(0f, 2f);
		border.localScale = new Vector3(0f, 2f);
		back.localScale = new Vector3(0f, 2f);
		LastHP = MaxHP;
    }	

    
    void Update()
    {
		//if the piece is not at maximum HP the HP Bar appears for the first time
		if(piece.GetComponent<Chessman>().HP != LastHP) //this makes a change only if the HP has changed
		{			
            Damage();
            LastHP = piece.GetComponent<Chessman>().HP; //this makes sure that it only updates the bar once			
		}
		
		if(piece.GetComponent<Chessman>().controller.GetComponent<Game>().PieceTargeted!=piece)
        {
			UnPreview();
		} 			
		
    }
	
	public void Preview(int ATK)
	{	
        if ((piece.GetComponent<Chessman>().HP - ATK)>0)
		    {TempLength = 2*((piece.GetComponent<Chessman>().HP - ATK) / MaxHP);}	
		else TempLength = 0;
		LastLength = 2*(LastHP / MaxHP);
		Transform bar = transform.Find("HPBar");
	    Transform border = transform.Find("HPBarBorder");
	    Transform back = transform.Find("HPBarBackground");
		Transform temp = transform.Find("HPBarTemp");
		bar.localScale = new Vector3(LastLength, 2f);
		temp.localScale = new Vector3(TempLength, 2f);
		border.localScale = new Vector3(2f, 2f);
	    back.localScale = new Vector3(2f, 2f);
		this.GetComponent<Animator>().SetBool("isTargeted", true);		
	}
	
	public void UnPreview()
	{
		if (LastHP==MaxHP){Start();this.GetComponent<Animator>().SetBool("isTargeted", false);}
		else
		{
			Transform bar = transform.Find("HPBar");
	    	Transform temp = transform.Find("HPBarTemp");
		    temp.localScale = bar.localScale;
            this.GetComponent<Animator>().SetBool("isTargeted", false);			
		}
	}
	
    void Damage()
	{
		//BarLength will give us the percent of HP remaining
		BarLength = 2*(piece.GetComponent<Chessman>().HP / MaxHP);
		Transform bar = transform.Find("HPBar");
	    Transform border = transform.Find("HPBarBorder");
	    Transform back = transform.Find("HPBarBackground");
		Transform temp = transform.Find("HPBarTemp");
		bar.localScale = new Vector3(BarLength, 2f);
		temp.localScale = new Vector3(BarLength, 2f);
		border.localScale = new Vector3(2f, 2f);
	    back.localScale = new Vector3(2f, 2f);
		this.GetComponent<Animator>().SetBool("isTargeted", false);
		this.GetComponent<Animator>().SetTrigger("ReceiveDamage");
	}	
}
