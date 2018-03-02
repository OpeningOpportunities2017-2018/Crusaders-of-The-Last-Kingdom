using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[System.Serializable]
public class Aliat:ICombatant
{
    Clasa clasa;
    List<Abilitate> abil;
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
            speed=1;
            mort=false;
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
    public void GiveViata(int v)
    {
        viata += v;
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
}