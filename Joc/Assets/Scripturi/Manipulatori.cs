using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Threading;
using UnityEditor.Advertisements;
public class Manipulatori : MonoBehaviour 
{
    //Trebuie sa atasezi scriptul de un obiect, de preferat Obiect nul
    public GameObject aliat,inamic;//Prefaburile pentru aliati si inamici
    public List<GameObject> aliati = new List<GameObject>(),inamici=new List<GameObject>();//Liste ce contin toti aliatii si toti inamicii
	System.Random rand = new System.Random();//Nu sterge. Asta e obiectul care va fi folosit pentru fiecare generare de numar aleator
    GameObject g;//Folosit pentru adaugarea aliatilor si inamicilor in listele lor.
	void Start () 
	{
		
	}
	void Update () 
	{
		
	}
	void SchimbaCuloare(GameObject obiect,Color culoare)//Merge daca imaginea nu face parte din UI (nu e randata cu Image, ci cu Sprite Renderer)
	{
		SpriteRenderer randare;
		try
		{
			randare = obiect.GetComponent<SpriteRenderer>();
			randare.color=culoare;
		}
		catch(Exception e) 
		{
			Debug.Log (e.Message);
		}
	}
}
/*
 * Scripturi vechi
 * public void ClickOnDummy()
    {
        Color culoare = new Color (slidere [0].value, slidere [1].value, slidere [2].value);
        SchimbaCuloare (imagine, culoare);
        try
        {
            text1.GetComponent<Text>().text=String.Format(textrand,rand.Next(7,12));
        }
        catch 
        {
            
        }
    }
*/