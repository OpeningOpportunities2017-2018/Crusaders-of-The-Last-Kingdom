using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Aliat:MonoBehaviour,ICombatant
{
    GameObject nul;
    Manipulatori manip;
    Clasa clasa;
    int viata;
    int speed;
    string nume;
    bool mort;
    void Start()
    {
        try
        {
            nume="Aliat";
            viata=100;
            speed=clasa.GetSpeed();
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
    public void SetSpeed(int m)
    {
        this.speed = m;
    }
    public int GetSpeed()
    {
        return this.speed;
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
    public void SetClasa(Clasa c)
    {
        clasa = c;
    }
    public Clasa GetClasa()
    {
        return clasa;
    }
    void OnMouseOver()
    {
        if(!manip.statistici.activeInHierarchy)
            manip.statistici.SetActive(true);
        try   
        {
            manip.statistici.transform.GetChild(0).GetComponent<Text>().text = String.Format("Viata: {0}", this.viata);
            manip.statistici.transform.GetChild(1).GetComponent<Text>().text = String.Format("Nume: {0}", this.nume);
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
