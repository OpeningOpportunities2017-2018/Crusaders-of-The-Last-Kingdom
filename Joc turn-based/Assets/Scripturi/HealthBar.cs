using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class HealthBar : MonoBehaviour
{
    public GameObject prefab;
    public GameObject root;
    public List<GameObject> parti;
    Vector3 poz;
    private void Awake()
    {
        root = gameObject;
    }
    void Start ()
    {
        parti = new List<GameObject>();
        poz = root.transform.position;
		for(int i=1;i<=100;i++)
        {
            parti.Add(Instantiate(prefab,poz,root.transform.rotation,root.transform));
            poz.x += 0.125f;
        }
    }
    public void Updatare(int cur,int init)
    {
        decimal umplere;
        umplere = ((cur / (decimal)init) * 100);
        Debug.Log((int)umplere + " "+cur+" "+init);
        foreach(GameObject g in parti)
        {
            if (parti.IndexOf(g) < (int)umplere)
                g.GetComponent<Renderer>().material.color = Color.green;
            else
                g.GetComponent<Renderer>().material.color = Color.red;
        }
    }
	void Update ()
    {
		
	}
}
