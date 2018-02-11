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
        obnul = transform.gameObject;
    }
    void Start()
    {
        VerificareEroriDeAmplasare();
        InitializareAbilitati();
        InitializareClase();
        InitializarePozitii();
        //Creez personajele si le pregatesc de lupta
        //De mentionat e faptul ca lipsa unui personaj in scena va provoca o eroare
        CreareCombatant(0, 1, null, 100, 2, clase["Arcas"], pozaliati[0, 0]);
        CreareCombatant(0, 3, null, 100, 4, clase["Cavaler(a)"], pozaliati[1, 1]);
        CreareCombatant(0, 4, null, 200, 6, clase["Preoteasa"], pozaliati[2, 2]);
        CreareCombatant(0, 1, "Arcas aliat 2", 150, 8, clase["Arcas"], pozaliati[2, 0]);
        CreareCombatant(1, 0, null, 300, 9, clase["Broscoi"], pozinamici[0, 0]);
        CreareCombatant(1, 4, null, 255, 7, clase["Preoteasa"], pozinamici[2, 0]);
        CreareCombatant(1, 2, null, 150, 5, clase["Cavaler"], pozinamici[1, 1]);
        CreareCombatant(1, 3, null, 400, 3, clase["Cavaler(a)"], pozinamici[1, 2]);
        CreareCombatant(1, 4, "Preoteasa inamic 2", 500, 1, clase["Preoteasa"], pozinamici[2, 2]);
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
                if(num==null)
                {
                    num = c.GetNume() + " - Aliat";
                }
                else
                    temp.name=num+" - Aliat";
            }
            else
            {
                temp = Instantiate(inamici[prefab], poz, Quaternion.identity);
                temp.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            if (num == null)
            {
                num = c.GetNume() + " - Inamic";
            }
            else
                temp.name = num + " - Inamic";
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
        pozaliati[0,0] = new Vector3(-31.7f,17f,-60.6f);
        pozaliati[0, 1] = new Vector3(-50.6f,17f,-42f);
        pozaliati[0, 2] = new Vector3(-67.4f,17f,-57.6f);
        pozaliati[1,0] = new Vector3(-35.4f, 17f, -19.6f);
        pozaliati[1, 1] = new Vector3(-52.5f, 17f, -28.6f);
        pozaliati[1, 2] = new Vector3(-71.16f, 17f, -27.4f);
        pozaliati[2,0] = new Vector3(-25.4f, 17f, -7.3f);
        pozaliati[2, 1] = new Vector3(-44.1f, 18.13f, -12.1f);
        pozaliati[2, 2] = new Vector3(-62.92f, 17f, -3f);
        //Initializare pozitii inamici
        pozinamici[0, 0] = new Vector3(66.87f, 68.11f, -9.3f);
        pozinamici[0, 1] = new Vector3(56.51f, 68.11f, -32.9f);
        pozinamici[0, 2] = new Vector3(38.8f, 68.11f, -7.9f);
        pozinamici[1, 0] = new Vector3(70.3f, 68.11f, -71.1f);
        pozinamici[1, 1] = new Vector3(48f, 68.11f, -58.3f);
        pozinamici[1, 2] = new Vector3(33.15f, 68.11f, -88.5f);
        pozinamici[2, 0] = new Vector3(59.4f, 68.11f, -103.1f);
        pozinamici[2, 1] = new Vector3(45f, 68.11f, -76.9f);
        pozinamici[2, 2] = new Vector3(40.7f, 68.11f, -128.6f);
    }
    void InitializareAbilitati()
    {
        //Aici se vor introduce manuale toate abilitatile posibile din joc.
        //Abilitatile de aici is momentan de test, dar cine stie
        abilitati.Add(new Abilitate(picabilitati[3],"Drojdeala de jale", 50,0));
        abilitati.Add(new Abilitate(picabilitati[2], "Dau heal cu borcanul", 40,0));
        abilitati.Add(new Abilitate(picabilitati[4], "Conditia taranului", 45,1));
        abilitati.Add(new Abilitate(picabilitati[0], "Divine revelation", 67,1));
        abilitati.Add(new Abilitate(picabilitati[1], "Distrugator de Project Manager",100,1));
    }
    void InitializareClase()
    {
        //Aici se vor introduce manual toate clasele posibile din joc, folosind functiile de setare atribute.
        //Clasele de mai jos is momentan de test, dar din nou, cine stie
        Clasa temp;
        temp = new Clasa("Arcas");
        temp.AdaugaAbilitati(2, 4);
        clase.Add("Arcas", temp);
        temp = new Clasa("Broscoi");
        temp.AdaugaAbilitati(0, 4);
        clase.Add("Broscoi", temp);
        temp = new Clasa("Cavaler");
        temp.AdaugaAbilitati(0, 0);
        temp.AdaugaAbilitati(3, 3);
        temp.AdaugaAbilitati(2, 2);
        clase.Add("Cavaler",temp);
        temp = new Clasa("Cavaler(a)");
        temp.AdaugaAbilitati(0, 3);
        clase.Add("Cavaler(a)", temp);
        temp = new Clasa("Preoteasa");
        temp.AdaugaAbilitati(0, 2);
        temp.AdaugaAbilitati(4, 4);
        clase.Add("Preoteasa", temp);

    }
    void VerificareEroriDeAmplasare()
    {
        foreach(GameObject g in aliati)
        {
            try
            {
                g.GetComponent<Combatant>();
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
            }
        }
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
    int tip_tinta = 1;//Se ia in functie de aliat. 0-Aliat 1-Inamic
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
    public int GetTinta()
    {
        return tip_tinta;
    }
    public void SetTinta(int t)
    {
        tip_tinta = t;
    }
    public void FacCeEDeFacut(int tip,GameObject target)//Aici mai e de munca, dar se vor face cazuri particulare.(Cea mai mare minciuna ever)
    {
        //tip este tipul combatantului care initiaza atacul.
        if(tip_tinta==target.GetComponent<Combatant>().GetTip())
        {
            if (tip_tinta == 0)
            {
                if (tip != target.GetComponent<Combatant>().GetTip())
                {
                    target.GetComponent<Combatant>().GiveViata(-damage);
                }
                else
                    target.GetComponent<Combatant>().GiveViata(damage);
            }
            else if (tip_tinta == 1)
            {
                if (tip != target.GetComponent<Combatant>().GetTip())
                {
                    target.GetComponent<Combatant>().GiveViata(damage);
                }
                else
                    target.GetComponent<Combatant>().GiveViata(-damage);
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
    public Abilitate(Sprite pict,string num="Abilitate",int dmg=10,int tinta=1)
    {
        nume = num;
        damage = dmg;
        pictograma = pict;
        tip_tinta = tinta;
    }
}
/*
        CreareCombatant(0, 0, "Priest",500,1,clase["Priest"],pozaliati[0,0]);
        CreareCombatant(0, 0, "Tank", 500, 3, clase["Tank"], pozaliati[2, 2]);
        CreareCombatant(0, 0, "Rogue", 500, 5, clase["Rogue"], pozaliati[1,1]);
        CreareCombatant(1, 0, "Rogue inamic", 500, 7, clase["Rogue"], pozinamici[0,2]);
        CreareCombatant(1, 0, "Tank inamic", 500, 4, clase["Tank"], pozinamici[2,0]);
        CreareCombatant(1, 0, "Un alt inamic ca oricare altul", 500, 6, clase["Tank"], pozinamici[1, 0]);
        CreareCombatant(1, 0, "E o zi ca oricare alta", 500, 2, clase["Tank"], pozinamici[1, 2]);
*/