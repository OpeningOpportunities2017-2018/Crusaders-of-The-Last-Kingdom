using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class HealthBar : MonoBehaviour
{
    public GameObject prefab;
    public GameObject root;
    public List<GameObject> parti;
    Vector3 poz;
    int fill=100;
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
    public IEnumerator Updatare(int cur,int init)
    {
        int dir;
        decimal umplere;
        umplere = ((cur / (decimal)init) * 100);
        //Debug.Log((int)umplere + " "+cur+" "+init);
        if(fill>umplere)
        {
            //Scade viata
            dir = 1;
        }
        else
        {
            //Creste viata
            dir = 0;
        }
        if (dir==1)
        {
            for (int i = parti.Count - 1; i >= 0; i--)
            {
                if (parti.IndexOf(parti[i]) < (int)umplere)
                    parti[i].GetComponent<Renderer>().material.color = Color.green;
                else
                    parti[i].GetComponent<Renderer>().material.color = Color.red;
                if(i%2==0)
                    yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            for (int i = 0; i <= parti.Count - 1; i++)
            {
                if (parti.IndexOf(parti[i]) < (int)umplere)
                    parti[i].GetComponent<Renderer>().material.color = Color.green;
                else
                    parti[i].GetComponent<Renderer>().material.color = Color.red;
                if(i%2==0)
                    yield return new WaitForEndOfFrame();
            }
        }
        fill = (int)umplere;
    }
	void Update ()
    {
		
	}
}
