using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Jucator : MonoBehaviour 
{
		
    /*
	 * Abilitati - Fiecare abilitate posibila in joc
	 * Momentan am considerat ca fiecare abilitate are o singura "pozitie" posibila in joc. Daca trebuie facute modificari, ele trebuie facute in clasa Abilitate si in functia PoateFolosiAbilitate
     * Clasele din fisierul asta vor fi globale, iar intr-un alt fisier voi face clasele orientate pentru instantiere
     * Abilitate=>Clasa=>Aliat si Inamic=>Combatant
	 */
    //Trebuie sa atasezi scriptul de un obiect, de preferat Obiect nul
	public static List<Abilitate> abilitati = new List<Abilitate> ();//Toate abilitatile din joc.
    public static Dictionary<string,Clasa> clase = new Dictionary<string, Clasa>();//Clasele de jucatori.
    public List<GameObject> combatanti = new List<GameObject>();//Toti combatantii din terenul de joc
    public Queue<Combatant> ture = new Queue<Combatant>();//Ordinea combatantilor la atac
    public List<GameObject> aliati = new List<GameObject>(),inamici=new List<GameObject>();//Liste ce contin toate prefaburile de aliatii si toate prefaburile de inamici
    public string stats="Nume - {0}\nTip - {1}\nViata - {2}\nMort - {3}";
    public Vector3[,] pozaliati = new Vector3[3,3],pozinamici=new Vector3[3,3];
    void Awake()
    {
        InitializareAbilitati();
        InitializareClase();
        InitializarePozitii();
        InitializareTure();
    }
    void Start()
    {
        CreareCombatant(0,0,"Aliatul unic",100,1,clase["Tank"],pozaliati[0,0]);
    }
    void CreareCombatant(int tip,int prefab,string num,int viata,int speed,Clasa c,Vector3 poz)
    {
        GameObject temp;
        string statistici;
        try
        {
            if (tip == 0)
            {
                temp = Instantiate(aliati[prefab], poz, Quaternion.identity);
            }
            else
            {
                temp = Instantiate(inamici[prefab], poz, Quaternion.identity);
            }
            temp.AddComponent<Combatant>();
            temp.GetComponent<Combatant>().SetTip(tip);
            temp.GetComponent<Combatant>().SetClasa(c);
            temp.GetComponent<Combatant>().SetViata(100);
            temp.GetComponent<Combatant>().SetNume(num);
            temp.GetComponent<Combatant>().SetSpeed(speed);
            temp.GetComponent<Combatant>().SeteazaMort(false);
            statistici=String.Format(stats,temp.GetComponent<Combatant>().GetNume(),temp.GetComponent<Combatant>().GetTip(),temp.GetComponent<Combatant>().GetViata(),temp.GetComponent<Combatant>().EsteMort());
            Debug.Log(statistici);
            combatanti.Add(temp);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    void InitializarePozitii()
    {
        //Initializare pozitii aliati
        pozaliati[0,0] = new Vector3(-4.62f,-0.63f,35.44f);
        pozaliati[0, 1] = new Vector3(-4.62f,-0.63f,31.62f);
        pozaliati[0, 2] = new Vector3(-4.62f,-0.63f,26.8f);
        pozaliati[1,0] = new Vector3(-6.12f,-0.63f,35.44f);
        pozaliati[1, 1] = new Vector3(-6.12f,-0.63f,31.62f);
        pozaliati[1, 2] = new Vector3(-6.12f,-0.63f,26.8f);
        pozaliati[2,0] = new Vector3(-8.34f,-0.63f,35.44f);
        pozaliati[2, 1] = new Vector3(-8.34f,-0.63f,31.62f);
        pozaliati[2, 2] = new Vector3(-8.34f,-0.63f,26.8f);
        //Initializare pozitii inamici
        pozinamici[0,0] = new Vector3(3.54f,-0.63f,35.44f);
        pozinamici[0, 1] = new Vector3(3.54f,-0.63f,31.62f);
        pozinamici[0, 2] = new Vector3(3.54f,-0.63f,26.8f);
        pozinamici[1,0] = new Vector3(2.35f,-0.63f,35.44f);
        pozinamici[1, 1] = new Vector3(2.35f,-0.63f,31.62f);
        pozinamici[1, 2] = new Vector3(2.35f,-0.63f,26.8f);
        pozinamici[2,0] = new Vector3(1f,-0.63f,35.44f);
        pozinamici[2, 1] = new Vector3(1f,-0.63f,31.62f);
        pozinamici[2, 2] = new Vector3(1f,-0.63f,26.8f);
    }
    void AdaugaTura(Combatant c)
    {
        ture.Enqueue(c);
    }
    Combatant UrmatorulCombatant()
    {
        return ture.Dequeue();
    }
    void InitializareAbilitati()
    {
        //Aici se vor introduce manuale toate abilitatile posibile din joc.
        //Abilitatile de aici is momentan de test, dar cine stie
        abilitati.Add(new Abilitate("Drojdeala de jale", -50, 0));
        abilitati.Add(new Abilitate("Dau heal cu borcanul", -40, 0));
        abilitati.Add(new Abilitate("Conditia taranului", 45, 1));
        abilitati.Add(new Abilitate("Divine revelation", 0, 1));
        abilitati.Add(new Abilitate("Distrugator de Project Manager",100,1));
    }
    void InitializareClase()
    {
        //Aici se vor introduce manual toate clasele posibile din joc, folosind functiile de setare atribute.
        //Clasele de mai jos is momentan de test, dar din nou, cine stie
        clase.Add("Tank",new Clasa("Tank",2,3));
        clase.Add("Priest",new Clasa("Priest",1,1));
        clase.Add("Rogue",new Clasa("Rogue",0,3));
    }
    void InitializareTure()
    {
        
    }
}
public class Clasa
{
	string nume;
	List<Abilitate> abilspec = new List<Abilitate>();
    public void SetNume(string s)
    {
        nume = s;
    }
    public string GetNume()
    {
        return nume;
    }
    public void AdaugaAbilitati(int inceput,int sfarsit)
    {
        try
        {
            for(int i=inceput;i<=sfarsit;i++)
            {
                AdaugaAbilitate(Jucator.abilitati[i]);
            }
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void AdaugaAbilitate(Abilitate a)
    {
        abilspec.Add(a);
    }
    public List<Abilitate> ObtineAbilitati()
    {
        return abilspec;
    }
    public Clasa(string num="Clasa",int incabil=0,int sfabil=0)
	{
        try
        {
            nume = num;
            AdaugaAbilitati(incabil,sfabil);
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }
	}
}
public class Abilitate
{
    int damage;
    int target;//0-Aliat,1-Inamic
	string nume;
    public void SetNume(string s)
    {
        nume = s;
    }
    public string GetNume()
    {
        return nume;
    }
    public void SetDamage(int d)
    {
        damage = d;
    }
    public int GetDamage()
    {
        return damage;
    }
    public void SetTarget(int t)
    {
        target = t;
    }
    public int GetTarget()
    {
        return target;
    }
    public Abilitate(string num="Abilitate",int dmg=10,int trg=1)
    {
        nume = num;
        damage = dmg;
        target = trg;
    }
}