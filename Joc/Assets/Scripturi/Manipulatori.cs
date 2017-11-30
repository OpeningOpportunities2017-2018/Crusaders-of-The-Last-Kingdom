using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Manipulatori : MonoBehaviour 
{
	public GameObject imagine,butondummy,text1;
	public List<Slider> slidere = new List<Slider>();//Culorile se vor pune in ordinea rosu, verde si albastru
	System.Random rand = new System.Random();
	public string textrand = "Numar aleator generat: {0}";
	void Start()
	{
		text1.GetComponent<Text> ().text = String.Format (textrand, 0);
	}
	public void ClickOnDummy()
	{
		Color culoare = new Color (slidere [0].value, slidere [1].value, slidere [2].value);
		SchimbaCuloare (imagine, culoare);
		try
		{
			text1.GetComponent<Text>().text=String.Format(textrand,GenerareNumarAleator(7,12));
		}
		catch(Exception e)
		{
			Debug.Log (e.Message);
		}
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
	int GenerareNumarAleator(int lim_inf,int lim_sup)
	{
		return rand.Next (lim_inf, lim_sup);
	}
}
