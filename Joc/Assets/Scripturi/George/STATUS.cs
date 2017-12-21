using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class STATUS : MonoBehaviour{
	
	public int HP;
	public int DEF;
	public int ATK;
	public int SPD;
	public GameObject a;

	void Start () { 
		HP = 100;
		DEF = 10;
		ATK = 10;
		SPD = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void dez()
	{	a.SetActive (false);
		
	}

}
