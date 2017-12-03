using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Aliat:MonoBehaviour
{
    GameObject nul;
    Manipulatori manip;
    int viata, mana;
    string nume;
    bool mort;
    void Awake()
    {
        try
        {
            nume="Aliat";
            viata=mana=100;
            mort=false;
            nul = GameObject.Find("Obiect nul");
            manip = nul.GetComponent<Manipulatori>();
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void SetViata(int v)
    {
        this.viata = v;
    }
    public int GetViata()
    {
        return this.viata;
    }
    public void SetMana(int m)
    {
        this.mana = m;
    }
    public int GetMana()
    {
        return this.mana;
    }
    public void SetNume(string s)
    {
        nume = s;
    }
    public string GetNume()
    {
        return this.nume;
    }
    public bool EsteMort()
    {
        return this.mort;
    }
    public void SeteazaMort(bool m)
    {
        this.mort = m;
    }
    void OnMouseOver()
    {
        if(!manip.statistici.activeInHierarchy)
            manip.statistici.SetActive(true);
            try   
            {
                manip.statistici.transform.GetChild(0).GetComponent<Text>().text = String.Format("Viata: {0}", this.viata);
                manip.statistici.transform.GetChild(1).GetComponent<Text>().text = String.Format("Mana: {0}", this.mana);
                manip.statistici.transform.GetChild(2).GetComponent<Text>().text = String.Format("Nume: {0}", this.nume);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
    }
    void OnMouseExit()
    {
        if (manip.statistici.activeInHierarchy)
            manip.statistici.SetActive(false);
    }
}
