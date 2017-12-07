using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jucator : MonoBehaviour 
{
		
    /*
	 * Abilitati - Fiecare abilitate posibila in joc
	 * Momentan am considerat ca fiecare abilitate are o singura "pozitie" posibila in joc. Daca trebuie facute modificari, ele trebuie facute in clasa Abilitate si in functia PoateFolosiAbilitate
     * Clasele din fisierul asta vor fi globale, iar intr-un alt fisier voi face clasele orientate pentru instantiere
	 */
    //Trebuie sa atasezi scriptul de un obiect, de preferat Obiect nul
	public List<Abilitate> abilitati = new List<Abilitate> ();//Toate abilitatile din joc.
    public Dictionary<string,Clasa> clase = new Dictionary<string, Clasa>();//Clasele de jucatori.
    void Awake()
    {
        InitializareAbilitati();
        InitializareClase();
    }
    void InitializareAbilitati()
    {
        //Aici se vor introduce manuale toate abilitatile posibile din joc, folosind functiile de setare atribute.
    }
    void InitializareClase()
    {
        //Aici se vor introduce manual toate clasele posibile din joc, folosind functiile de setare atribute.
    }
}
public class Clasa
{
	string nume;
    int speed;
	List<Abilitate> abilspec = new List<Abilitate>();
    public int GetSpeed()
    {
        return speed;
    }
    public void SetSpeed(int s)
    {
        speed = s;
    }
    public void SetNume(string s)
    {
        nume = s;
    }
    public string GetNume()
    {
        return nume;
    }
    public void AdaugaAbilitate(Abilitate a)
    {
        abilspec.Add(a);
    }
    public List<Abilitate> ObtineAbilitati()
    {
        return abilspec;
    }
	Clasa()
	{
        nume = "Clasa";
        speed = 1;
	}
}
public class Abilitate
{
	string nume;
    void SetNume(string s)
    {
        nume = s;
    }
    string GetNume()
    {
        return nume;
    }
}