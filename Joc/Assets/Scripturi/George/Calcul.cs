using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calcul : MonoBehaviour {
	private int x1 = 0;
	private int x2 = 0;
	private int x3 = 0;
	private int y =0;
	private int a1 = 0;
	private int a2 = 0;
	private int a3 = 0;
	private int c = 0;
	private int aux = 0;
	public STATUS ally1;
	public STATUS ally2;
	public STATUS ally3;
	public EMPTY empty;
	public GameObject a;
	void Start () {
		
	}
				
	void Update () {
		empty = FindObjectOfType<EMPTY> ();
		x1 = ally1.HP;
		x2 = ally2.HP;
		x3 = ally3.HP;
		y = empty.z;
		a1 = ally1.DEF;
		a2 = ally2.DEF;
		a3 = ally3.DEF;
		if (x1 <= 0)
			x1 = 1000000;
		if (x2 <= 0)
			x2 = 1000000;
		if (x3 <= 0)
			x3 = 1000000;
        Debug.Log(x1);
        Debug.Log(y);
        Debug.Log(a1);
        Debug.Log((y * a1) / 100);
        Debug.Log((y - ((y * a1) / 100)));
        c = (Mathf.Min (x1 /( y - ((y * a1) / 100)), x2 / ( y - ((y * a2) / 100)), x3 / ( y - ((y * a3) / 100))));
        if (c == x1 /( y - ((y * a1) / 100))) 
		{ aux =ally1.HP-( y - ((y * a1) / 100));
			Debug.Log (aux);
			ally1.HP = aux;
			a.SetActive (false);


		} 
		else if (c == x2 /( y - ((y * a2) / 100))) {
			aux =ally2.HP-( y - ((y * a2) / 100));
			Debug.Log (aux);
			ally2.HP = aux;
			a.SetActive (false);
		} 
		else 
		{	aux =ally3.HP-( y - ((y * a3) / 100));
			Debug.Log (aux);
			ally3.HP = aux;
			a.SetActive (false);
		}

	}

}