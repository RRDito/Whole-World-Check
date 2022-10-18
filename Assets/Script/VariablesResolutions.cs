using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script is started with Canvas
public class VariablesResolutions : MonoBehaviour
{
    public GameObject Board;//so far, not used    0.96  -3.36
       
    public void VariablesForCoords (out float a, out float b)
    {	   
        a = 0.963f*(Camera.main.aspect/1.778292f);          //ax+b   ay+b
	    b = -3.370f*(Camera.main.aspect/1.778292f);  	   
    }
   
    void Start()
    {
	   
    }
   
}
