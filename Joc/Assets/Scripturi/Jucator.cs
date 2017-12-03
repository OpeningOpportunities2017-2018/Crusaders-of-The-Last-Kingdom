using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jucator : MonoBehaviour 
{
<<<<<<< HEAD
	void Start()
	{
		
=======
    /*
	 * Abilitati - Fiecare abilitate posibila in joc
	 * Momentan am considerat ca fiecare abilitate are o singura "pozitie" posibila in joc. Daca trebuie facute modificari, ele trebuie facute in clasa Abilitate si in functia PoateFolosiAbilitate
     * Clasele din fisierul asta vor fi globale, iar intr-un alt fisier voi face clasele orientate pentru instantiere
	 */
	public List<Abilitate> abilitati = new List<Abilitate> ();
	bool PoateFolosiAbilitatea(int pozitie,Abilitate abil)
	{
		return pozitie == abil.GetPozitie ();
>>>>>>> 36717c6347087f7578163a45081f87d876a4fb1a
	}
}
public class Clasa
{
	string nume;
	public List<Abilitate> abilitati = new List<Abilitate> ();
	Clasa()
	{
		
	}
}
public class Abilitate
{
	string nume;
}