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
    public GameObject cub;//Obiect pentru testare functii de rotire/manipulare treptata
    public Vector3 rotatie,directie;//Cum se roteste/misca obiectul de test
    //Schimbare. Propun sa facem functiile de aici de manipulare statice, ca sa nu trebuiasca sa tot cautam obiectul nul. E pentru optimizare
	void Start () 
	{
        RotireTreptata(cub, rotatie);
        MiscareTreptata(cub, directie);
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
    void RotireTreptata(GameObject g,Vector3 rot,int etape)
    {
        StartCoroutine(RotesteTreptat(g, rot,etape));
    }
    IEnumerator RotesteTreptat(GameObject g,Vector3 rot,int etape)
    {
        for(int i=1;i<=etape;i++)
        {
            g.transform.Rotate(rot);
            yield return new WaitForFixedUpdate();
        }
    }    
    void RotireTreptata(GameObject g,Vector3 rot)
    {
        StartCoroutine(RotesteTreptat(g, rot));
    }
    IEnumerator RotesteTreptat(GameObject g,Vector3 rot)
    {
        while(true)
        {
            g.transform.Rotate(rot);
            yield return new WaitForFixedUpdate();
        }
    }
    void MiscareTreptata(GameObject g,Vector3 misc,int etape)
    {
        StartCoroutine(MiscaTreptat(g, misc,etape));
    }
    IEnumerator MiscaTreptat(GameObject g,Vector3 misc,int etape)
    {
        for(int i=1;i<=etape;i++)
        {
            g.transform.Translate(misc, Space.World);
            yield return new WaitForFixedUpdate();
        }
    }
    void MiscareTreptata(GameObject g,Vector3 misc)
    {
        StartCoroutine(MiscaTreptat(g, misc));
    }
    IEnumerator MiscaTreptat(GameObject g,Vector3 misc)
    {
        while(true)
        {
            g.transform.Translate(misc, Space.World);
            yield return new WaitForFixedUpdate();
        }
    }
}
/* Scripturi vechi
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