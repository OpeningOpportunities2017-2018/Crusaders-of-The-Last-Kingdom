using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour 
{
    //Inca nu bagam sunete in joc, dar nu strica sa fie facut scriptul
    public AudioSource proba;
	void Start () 
    {
        proba.time = 20;
        proba.pitch = 0.7f;
	}
	void Update () 
    {
        
	}
}
