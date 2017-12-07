using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
<<<<<<< HEAD
using System.Threading;
using UnityEditor.Advertisements;
public class Manipulatori : MonoBehaviour 
{
	public GameObject imagine,butondummy,text1;//Toate astea is pentru test
    public List<Slider> slidere = new List<Slider>();//Culorile se vor pune in ordinea rosu, verde si albastru. Este doar pentru teste
    public GameObject aliat,inamic;//Prefaburile pentru aliati si inamici
    public GameObject statistici;//Prefab pentru statistici
    public List<GameObject> aliati = new List<GameObject>(),inamici=new List<GameObject>();//Liste ce contin toti aliatii si toti inamicii
	System.Random rand = new System.Random();//Nu sterge. Asta e obiectul care va fi folosit pentru fiecare generare de numar aleator
    public string textrand = "Numar aleator generat: {0}";//Text prestabilit pentru testare generator de nr. aleatorii ({0} e masca)
    GameObject g;//Folosit pentru adaugarea aliatilor si inamicilor in listele lor.
	void Start()
=======
public class Manipulatori : MonoBehaviour 
{
	public GameObject imagine,butondummy,text1;
	public List<Slider> slidere = new List<Slider>();//Culorile se vor pune in ordinea rosu, verde si albastru
	System.Random rand = new System.Random();
	string textrand = "Numar aleator generat:{0}";
	void Start () 
>>>>>>> aaf59d0af7443a07f4d9879bf45925227820bf10
	{
		
	}
	void Update () 
	{
		
	}
	public void ClickOnDummy()
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
