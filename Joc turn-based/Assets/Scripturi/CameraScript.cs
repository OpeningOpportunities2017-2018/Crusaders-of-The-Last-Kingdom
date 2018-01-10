using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CameraScript : MonoBehaviour 
{
    public GameObject obiect_urmarit;//Obiect urmarit de camera asta
    public Vector3 dif;//Diferenta dintre obiect si camera, folosita pentru tracking
    void Start()
    {
        
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
