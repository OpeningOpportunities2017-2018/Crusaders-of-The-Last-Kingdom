using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPTY : MonoBehaviour {
	// fiecare x ii viata aliatului
	// fiecare y ii viata inamicului
	public int x1 = 0;
	public int x2 = 0;
	public int x3 = 0;
	public int y1 = 0;
	public int y2 = 0;
	public int y3 = 0;
	public int z = 0;
	public STATUS ally1;
	public STATUS ally2;
	public STATUS ally3;
	public STATUS enemy1;
	public STATUS enemy2;
	public STATUS enemy3;
	public GameObject empty;

	void Start () {
		

	 
	}
	
	void Update () {
		x1 = ally1.HP;
		x2 = ally2.HP;
		x3 = ally3.HP;
		y1 = enemy1.HP;
		y2 = enemy2.HP;
		y3 = enemy3.HP;
		if (x1 <= 0)
			ally1.dez ();
		if (x2 <= 0)
			ally2.dez ();
		if (x3 <= 0)
			ally3.dez ();
		if (y1 <= 0)
			enemy1.dez ();
		if (y2 <= 0)
			enemy2.dez ();
		if (y3 <= 0)
			enemy3.dez ();
		
	}
	public void inamic1()
	{ enemy1.HP-=ally1.ATK-(ally1.ATK*enemy1.DEF)/100;
		z = enemy1.ATK;
		empty.SetActive(true);
	}
	public void inamic2()
	{	enemy2.HP-=ally2.ATK-(ally2.ATK*enemy2.DEF)/100;
		z = enemy2.ATK;
		empty.SetActive(true);
	}
	public void inamic3()
	{	z = enemy3.ATK;
		enemy3.HP-=ally3.ATK-(ally3.ATK*enemy3.DEF)/100;
		empty.SetActive(true);
	}

}
