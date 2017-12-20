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
	System.Random rand = new System.Random();//Nu sterge. Asta e obiectul care va fi folosit pentru fiecare generare de numar aleator
    GameObject g;//Folosit pentru adaugarea aliatilor si inamicilor in listele lor.
    public GameObject cub;//Obiect pentru testare functii de rotire/manipulare treptata
    public Vector3 rotatie,directie;//Cum se roteste/misca obiectul de test
    //Schimbare. Propun sa facem functiile de aici de manipulare statice, ca sa nu trebuiasca sa tot cautam obiectul nul. E pentru optimizare
	void Start () 
	{
        
	}
	void Update () 
	{
        if (Input.GetKey(KeyCode.A))
        {
            MiscaDinTasta(cub, KeyCode.A,0.5f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MiscaDinTasta(cub, KeyCode.D,0.5f);
        }
	}
    void MiscaDinTasta(GameObject g,KeyCode cod,float multip=1f)//Misca obiectul g cu tasta cu codul "cod" cu multiplicatorul multip 
    {
        switch(cod)
        {
            case KeyCode.A:
                {
                    g.transform.Translate(Vector3.left*multip);
                    break;
                }
            case KeyCode.D:
                {
                    g.transform.Translate(Vector3.right*multip);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
	static void SchimbaCuloare(GameObject obiect,Color culoare)//Merge daca imaginea nu face parte din UI (nu e randata cu Image, ci cu Sprite Renderer)
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
    void RotireTreptata(GameObject g,Vector3 rot,uint etape=0)
    {
        StartCoroutine(RotesteTreptat(g, rot,etape));
    }
    static IEnumerator RotesteTreptat(GameObject g,Vector3 rot,uint etape)//Daca etape=0 se repeta la infinit.
    {
        if (etape > 0)
        {
            for (int i = 1; i <= etape; i++)
            {
                g.transform.Rotate(rot);
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (true)
            {
                g.transform.Rotate(rot);
                yield return new WaitForFixedUpdate();
            }
        }
    }
    void MiscareTreptata(GameObject g,Vector3 misc,uint etape=0)//Daca etape=0 se repeta la infinit
    {
        StartCoroutine(MiscaTreptat(g, misc,etape));
    }
    static IEnumerator MiscaTreptat(GameObject g,Vector3 misc,uint etape)
    {
        if (etape > 0)
        {
            for (int i = 1; i <= etape; i++)
            {
                g.transform.Translate(misc, Space.World);
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (true)
            {
                g.transform.Translate(misc, Space.World);
                yield return new WaitForFixedUpdate();
            }
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