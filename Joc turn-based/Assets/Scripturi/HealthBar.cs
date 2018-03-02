using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public int fill;
    public GameObject prefab;
    public GameObject root;
    Vector3 poz;
    private void Awake()
    {
        root = gameObject;
    }
    void Start ()
    {
        poz = root.transform.position;
		for(int i=1;i<=100;i++)
        {
            Instantiate(prefab,poz,root.transform.rotation,root.transform);
            poz.x += 0.125f;
        }
    }
    public void Updatare(int init)
    {
        if(fill>0)
        {
            for (int i = 0; i < fill; i++)
            {
                try
                {
                    root.transform.GetChild(i).GetComponent<Renderer>().material.color = Color.green;
                }
                catch
                {
                    Debug.Log(i + " " + fill);
                }
            }
            for (int i = fill; i < 100; i++)
            {
                try
                {
                    root.transform.GetChild(i).GetComponent<Renderer>().material.color = Color.red;
                }
                catch
                {
                    Debug.Log(i + " " + fill);
                }
            }
        }
    }
	void Update ()
    {
		
	}
}
