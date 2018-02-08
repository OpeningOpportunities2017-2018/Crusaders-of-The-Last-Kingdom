using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaraSim : MonoBehaviour {
    CombatSimulator cs;
	// Use this for initialization
	void Start () {
        cs = FindObjectOfType<CombatSimulator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown()
    {
        Debug.Log("test");
        if (cs.g1 == null)
        { cs.g1 = this.gameObject; cs.retine1 = this.transform.position; }
        else if (cs.g1 != null && cs.g2 == null)
        { cs.g2 = this.gameObject; cs.retine2 = this.transform.position; }
    }
}
