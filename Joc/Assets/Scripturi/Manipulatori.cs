using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
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
        //Butonul asta e de proba, e ala din coltul stanga jos, si va colora cainele in functie de slidere, va genera un numar aleator si va crea 5 aliati si 5 inamici, iar apoi da damage random la toti aliatii din scena.
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
        for (int i = 1; i <= 5; i++)
        {
            g = Instantiate(aliat);
            g.GetComponent<Aliat>().SetNume("Aliat " + i);
            g.transform.position = new Vector3((float)(rand.Next(0,5)+rand.NextDouble()),(float)(rand.Next(0,5)+rand.NextDouble()),(float)(rand.Next(0,5)+rand.NextDouble()));
            aliati.Add(g);
            g = Instantiate(inamic);
            g.GetComponent<Inamic>().SetNume("Inamic " + i);
            g.transform.position = new Vector3((float)(rand.Next(-2,3)+rand.NextDouble()),(float)(rand.Next(-2,3)+rand.NextDouble()),(float)(rand.Next(-2,3)+rand.NextDouble()));
            inamici.Add(g);
        }
        foreach (GameObject g in aliati)
        {
            if(g.GetComponent<Aliat>().GetViata() > 0)
                g.GetComponent<Aliat>().SetViata(g.GetComponent<Aliat>().GetViata() - GenerareNumarAleator(5, 25));
            else if (g.GetComponent<Aliat>().GetViata() <= 0)
            {
                g.GetComponent<Aliat>().SeteazaMort(true);
                g.GetComponent<Aliat>().SetViata(1);
                Debug.Log("Aliatul cu numele " + g.GetComponent<Aliat>().GetNume() + " a murit.");
            }
            else if (g.GetComponent<Aliat>().EsteMort())
            {
                Debug.Log("Aliatul cu numele " + g.GetComponent<Aliat>().GetNume() + " a murit deja.");
            }
            Debug.Log(g.GetComponent<Aliat>().GetViata());
        }
	}
	void SchimbaCuloare<Tip>(GameObject obiect,Color culoare)
	{
        //Functia va merge acum si pentru GameObject ce contin SpriteRenderer sau Image, dar trebuie precizat in ce situatie se foloseste.
        //Daca nu precizezi, nu-i nimic. Exceptia este tratata.
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
	public int GenerareNumarAleator(int lim_inf,int lim_sup)
	{
		return rand.Next (lim_inf, lim_sup);
	}
}
