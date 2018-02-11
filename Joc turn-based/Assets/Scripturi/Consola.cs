using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Consola : MonoBehaviour
{
    public string continut="";
    public GameObject ob_continut,consola;
    public DateTime data;
	// Use this for initialization
	void Start ()
    {

    }
    public void ScrieInConsola(string text)
    {
        data = DateTime.Now;
        string de_adaugat = "{0}/{1}:{2}:{3}- {4}\n";
        continut += string.Format(de_adaugat,data.Date,data.Hour,data.Minute,data.Second,text);
        ob_continut.GetComponent<Text>().text = continut;
    }
	void Update ()
    {
		
	}
}
