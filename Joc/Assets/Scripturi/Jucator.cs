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
	public List<Abilitate> abilitati = new List<Abilitate> ();
	bool PoateFolosiAbilitatea(int pozitie,Abilitate abil)
	{
		return pozitie == abil.GetPozitie ();
	}
}
public class Clasa
{
	string nume;
	List<Abilitate> abilspec = new List<Abilitate>();
	public Clasa()
	{
		
	}
}
public class Abilitate
{
	string nume;
	int pozitie;
	public void SetNume(string s)
	{
		nume = s;
	}
	public string GetNume()
	{
		return nume;
	}
	public void SetPozitie(int p)
	{
		pozitie = p;
	}
	public int GetPozitie()
	{
		return pozitie;
	}
}