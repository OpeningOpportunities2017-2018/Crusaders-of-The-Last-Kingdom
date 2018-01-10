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
    public static List<Abilitate> abilitati;
    public static Dictionary<string, Clasa> clase;
    public List<GameObject> combatanti;
    public Queue<GameObject> ture;
    public List<GameObject> aliati, inamici;
    public Vector3[,] pozaliati, pozinamici;
    public string stats="Nume - {0}\nTip - {1}\nViata - {2}\nMort - {3}";//De test
    GameObject comb,tmp;//De test
    Combatant c;//De test
    void Awake()
    {
        abilitati = new List<Abilitate>();//Toate abilitatile din joc.
        clase = new Dictionary<string, Clasa>();//Clasele de jucatori.
        combatanti = new List<GameObject>();//Toti combatantii din terenul de joc
        ture = new Queue<GameObject>();//Ordinea combatantilor la atac
        pozaliati = new Vector3[3, 3]; pozinamici = new Vector3[3, 3];//Matrice de pozitii personaje. Nu sterge. Ms
    }
    void Start()
    {
        InitializareAbilitati();
        InitializareClase();
        InitializarePozitii();
        //Creez personajele si le pregatesc de lupta
        CreareCombatant(0,0,"Numele tau frate",100,1,clase["Priest"],pozaliati[1,1]);
        InitializareTure();
    }
    void Update()
    {
        //Daca apas tasta W, personajele vor lua damage 5 in ordinea lor din coada.
        if (Input.GetKeyDown(KeyCode.W))
        {
            comb = UrmatorulCombatant();
            c = comb.GetComponent<Combatant>();
            c.GiveViata(-5);
            foreach (GameObject g in combatanti)
            {
                if (g == comb)
                {
                    tmp = g;
                    break;
                }
            }
            Debug.Log("Combatantul cu numele "+c.GetNume()+" si speed "+c.GetSpeed()+" a luat 5 damage.");
            tmp.transform.GetChild(0).GetComponent<TextMesh>().text = String.Format(stats, c.GetNume(), c.GetTip(), c.GetViata(), c.EsteMort());
            AdaugaTura(comb);
        }
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
    void AdaugaTura(GameObject c)
    {
        ture.Enqueue(c);
    }
    GameObject UrmatorulCombatant()
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
        //Copie intr-o lista toti combatantii, ii sorteaza dupa speed si apoi ii baga in coada
        List<GameObject> temp = new List<GameObject>();
        GameObject aux;
        foreach (GameObject g in combatanti)
        {
            temp.Add(g);
        }
        for (int i = 0; i < temp.Count-1; i++)
        {
            for (int j = i + 1; j < temp.Count; j++)
            {
                if (temp[i].GetComponent<Combatant>().GetSpeed() < temp[j].GetComponent<Combatant>().GetSpeed())
                {
                    aux = temp[i];
                    temp[i] = temp[j];
                    temp[j] = aux;
                }
            }
        }
        foreach (GameObject g in temp)
        {
            AdaugaTura(g);
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
    public Clasa(string num="Clasa",int incabil=0,int sfabil=0)
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
    void FacCeEDeFacut(GameObject target)//Aici mai e de munca, dar se vor face cazuri particulare
    {
        target.GetComponent<Combatant>().SetViata(target.GetComponent<Combatant>().GetViata() - damage);
    }
    public Abilitate(string num="Abilitate",int dmg=10,int trg=1)
    {
        nume = num;
        damage = dmg;
        target = trg;
    }
}