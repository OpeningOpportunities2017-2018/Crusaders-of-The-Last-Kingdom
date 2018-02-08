using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class CombatSimulator : MonoBehaviour {
    public GameObject g1, g2;
    public GameObject poz1, poz2;
    public Vector3 retine1, retine2;
    bool ok;
    [Range(0.0f,2f)]
    public float step;
	// Use this for initialization
	void Start () {
        ok = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(g1!=null && g2!=null && ok==true)
        {
            g1.transform.position = Vector3.MoveTowards(g1.transform.position, poz1.transform.position, step);
           // g1.transform.position = poz1.transform.position;
          //  g2.transform.position = poz2.transform.position;
            g2.transform.position = Vector3.MoveTowards(g2.transform.position, poz2.transform.position, step);
            if (g1.transform.position == poz1.transform.position && g2.transform.position == poz2.transform.position)
            { ok = false; StartCoroutine(anim()); }
       }
	}
    public void resetare()
    {
        Debug.Log("resetare");
        g1.transform.position = retine1;
        g2.transform.position = retine2;
        StopAllCoroutines();
        g1.GetComponent<UnityArmatureComponent>().animation.Play("Idle");
        g2.GetComponent<UnityArmatureComponent>().animation.Play("Idle");
        g1 = null;
        g2 = null;
        ok = true;
    }
    IEnumerator anim()
    {
        g1.GetComponent<UnityArmatureComponent>().animation.Play("Attack");
        yield return new WaitForSeconds(0.5f);
        g2.GetComponent<UnityArmatureComponent>().animation.Play("Damaged");
    }
}
