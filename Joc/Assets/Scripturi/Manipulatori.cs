using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Manipulatori : MonoBehaviour 
{
	public GameObject imagine,butondummy,text1;//Toate astea is pentru test
    public GameObject aliat,inamic;//Prefaburi pentru aliati si inamici
    public GameObject txtviata,txtmana,txtnume;
	public List<Slider> slidere = new List<Slider>();//Culorile se vor pune in ordinea rosu, verde si albastru. Este doar pentru teste
	System.Random rand = new System.Random();//Nu sterge. Asta e obiectul care va fi folosit pentru fiecare generare de numar aleator
	public string textrand = "Numar aleator generat: {0}";
	void Start()
	{
		try
		{
			text1.GetComponent<Text> ().text = String.Format (textrand, 0);
		}
		catch(Exception e)
		{
			Debug.Log (e.Message);
		}
	}
	public void ClickOnDummy()
	{
		Color culoare = new Color (slidere [0].value, slidere [1].value, slidere [2].value);
		SchimbaCuloare<SpriteRenderer> (imagine, culoare);
		try
		{
			text1.GetComponent<Text>().text=String.Format(textrand,GenerareNumarAleator(7,12));
		}
		catch(Exception e)
		{
			Debug.Log (e.Message);
		}
	}
	void SchimbaCuloare<Tip>(GameObject obiect,Color culoare)//Functia va merge acum si pentru GameObject ce contin SpriteRenderer sau Image, dar trebuie precizat in ce situatie se foloseste.
	{
		SpriteRenderer randare;
		Image imagine;
		if (typeof(Tip) == typeof(SpriteRenderer))
		{
			try
			{
				randare = obiect.GetComponent<SpriteRenderer>();
				randare.color = culoare;
			}
			catch (Exception e)
			{
				Debug.Log(e.Message);
			}
		}
		else if (typeof(Tip) == typeof(Image))
		{
			try
			{
				imagine = obiect.GetComponent<Image>();
				imagine.color = culoare;
			}
			catch (Exception e)
			{
				Debug.Log(e.Message);
			}
		}
	}
	int GenerareNumarAleator(int lim_inf,int lim_sup)
	{
		return rand.Next (lim_inf, lim_sup);
	}
}
