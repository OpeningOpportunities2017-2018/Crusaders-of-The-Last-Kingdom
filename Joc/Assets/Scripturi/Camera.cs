﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
public class Camera : MonoBehaviour 
{
    public GameObject obiect_urmarit;//Obiect urmarit de camera asta
    public Vector3 dif;
    public Vector3 rotatie;
    public GameObject cub;
    void Start()
    {
        UrmaresteObiect(cub);
    }
    public void UrmaresteObiect(GameObject g)
    {
        try
        {
            obiect_urmarit = g;
            dif = this.transform.position-g.transform.position;
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    void LateUpdate()
    {
        if(obiect_urmarit!=null)
            this.transform.position = obiect_urmarit.transform.position + dif;
    }
}
