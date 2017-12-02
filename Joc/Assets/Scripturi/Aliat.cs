using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Aliat : MonoBehaviour
{
    GameObject nul;
    int viata, mana;
    string nume;
    public void Constructor()
    {
        viata = mana = 100;
        nume = "Aliat";
        try
        {
            nul = GameObject.Find("Obiect nul");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    void SetViata(int v)
    {
        viata = v;
    }
    int GetViata()
    {
        return viata;
    }
    void SetMana(int m)
    {
        mana = m;
    }
    int GetMana()
    {
        return mana;
    }
    void SetNume(string s)
    {
        nume = s;
    }
    string GetNume()
    {
        return nume;
    }
}
