using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Jucator : MonoBehaviour 
{

    /*
	 * Abilitati - Fiecare abilitate posibila in joc
	 * Momentan am considerat ca fiecare abilitate are o singura "pozitie" posibila in joc. Daca trebuie facute modificari, ele trebuie facute in clasa Abilitate si in functia PoateFolosiAbilitate
     * Clasele din fisierul asta vor fi globale, iar intr-un alt fisier voi face clasele orientate pentru instantiere
     * Abilitate=>Clasa=>Aliat si Inamic=>Combatant
	 */
    //Trebuie sa atasezi scriptul de un obiect, de preferat Obiect nul
    public static List<Abilitate> abilitati;//Toate abilitatile posibile
    public List<Sprite> picabilitati;//Pictogramele abilitatilor
    public static Dictionary<string, Clasa> clase;//Clasele din scena
    public List<GameObject> combatanti;//Toti combatantii din scena
    public List<GameObject> aliati, inamici;//Lista cu prefaburile de aliati si inamici
    public Vector3[,] pozaliati, pozinamici;//Pozitiile de spawnare ale aliatilor si inamicilor
    public static GameObject obnul;//Obiectul cu scripturi
    void Awake()
    {
        abilitati = new List<Abilitate>();//Toate abilitatile din joc.
        clase = new Dictionary<string, Clasa>();//Clasele de jucatori.
        combatanti = new List<GameObject>();//Toti combatantii din terenul de joc
        pozaliati = new Vector3[3, 3]; pozinamici = new Vector3[3, 3];//Matrice de pozitii personaje. Nu sterge. Ms
    }
    void Start()
    {
        InitializareAbilitati();
        InitializareClase();
        InitializarePozitii();
        obnul = transform.gameObject;
        //Creez personajele si le pregatesc de lupta
        CreareCombatant(0, 0, "Priest",100,1,clase["Priest"],pozaliati[0,0]);
        CreareCombatant(0, 0, "Tank", 100, 3, clase["Tank"], pozaliati[2, 2]);
        CreareCombatant(0, 0, "Rogue", 100, 5, clase["Rogue"], pozaliati[1,1]);
        CreareCombatant(1, 0, "Rogue inamic", 100, 2, clase["Rogue"], pozinamici[0,2]);
        CreareCombatant(1, 0, "Tank inamic", 100, 4, clase["Tank"], pozinamici[2,0]);
    }
    void Update()
    {
        
    }
    void CreareCombatant(int tip,int prefab,string num,int viata,int speed,Clasa c,Vector3 poz)
    {
        GameObject temp;
            if (tip == 0)
            {
                temp = Instantiate(aliati[prefab], poz, Quaternion.identity);
                temp.name=num+" - Aliat";
            }
            else
            {
                temp = Instantiate(inamici[prefab], poz, Quaternion.identity);
                temp.name=num+" - Inamic";
            }
            temp.GetComponent<Combatant>().SetTip(tip);
            temp.GetComponent<Combatant>().SetClasa(c);
            temp.GetComponent<Combatant>().SetViata(viata);
            temp.GetComponent<Combatant>().SetNume(num);
            temp.GetComponent<Combatant>().SetSpeed(speed);
            temp.GetComponent<Combatant>().SeteazaMort(false);
            combatanti.Add(temp);
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
    void InitializareAbilitati()
    {
        //Aici se vor introduce manuale toate abilitatile posibile din joc.
        //Abilitatile de aici is momentan de test, dar cine stie
        abilitati.Add(new Abilitate(picabilitati[3],"Drojdeala de jale", 50, 0));
        abilitati.Add(new Abilitate(picabilitati[2], "Dau heal cu borcanul", 40, 0));
        abilitati.Add(new Abilitate(picabilitati[4], "Conditia taranului", 45, 1));
        abilitati.Add(new Abilitate(picabilitati[0], "Divine revelation", 0, 1));
        abilitati.Add(new Abilitate(picabilitati[1], "Distrugator de Project Manager",100,1));
    }
    void InitializareClase()
    {
        //Aici se vor introduce manual toate clasele posibile din joc, folosind functiile de setare atribute.
        //Clasele de mai jos is momentan de test, dar din nou, cine stie
        Clasa temp;
        temp = new Clasa("Tank");
        temp.AdaugaAbilitate(abilitati[0]);
        temp.AdaugaAbilitate(abilitati[1]);
        clase.Add("Tank",temp);
        temp = new Clasa("Priest");
        temp.AdaugaAbilitati(0, 3);
        clase.Add("Priest",temp);
        temp = new Clasa("Rogue");
        temp.AdaugaAbilitate(abilitati[4]);
        clase.Add("Rogue",temp);
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
    public Clasa(string num,int incabil,int sfabil)
	{
        try
        {
            nume = num;
            AdaugaAbilitati(incabil,sfabil);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
	}
    public Clasa()
    {
        nume = "Default";
    }
    public Clasa(string num)
    {
        nume = num;
    }
}
public class Abilitate
{
    Sprite pictograma;
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
    public void FacCeEDeFacut(GameObject target)//Aici mai e de munca, dar se vor face cazuri particulare
    {
        switch(target.GetComponent<Combatant>().GetTip())
        {
            default:
                {
                    target.GetComponent<Combatant>().SetViata(target.GetComponent<Combatant>().GetViata() - damage);
                    break;
                }
        }
    }
    public void SetPictograma(Sprite pict)
    {
        pictograma = pict;
    }
    public Sprite GetPictograma()
    {
        return pictograma;
    }
    public Abilitate(Sprite pict,string num="Abilitate",int dmg=10,int trg=1)
    {
        nume = num;
        damage = dmg;
        target = trg;
        pictograma = pict;
    }
}