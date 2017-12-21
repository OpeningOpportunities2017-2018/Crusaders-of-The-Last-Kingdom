﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ICombatant te obliga si te face sa nu uiti ce functii ai de bagat, deci mai intai bagi aici antetul unei functii comune, si dup-aia o definesti
public interface ICombatant 
{
    void SetViata(int v);
    int GetViata();
    void SetSpeed(int m);
    int GetSpeed();
    void SetNume(string s);
    string GetNume();
    bool EsteMort();
    void SeteazaMort(bool m);
    void SetClasa(Clasa c);
    Clasa GetClasa();
}
public class Combatant:MonoBehaviour,ICombatant
{
    Aliat a;
    Inamic i;
    int tip=0;//Deocamdata 0-Aliat,1-Inamic
    public Combatant(Clasa c,int t=0,int viata=100,int speed=1,string nume="",bool mort=false)
    {
        if (t == 0)
        {
            a = new Aliat();
            tip = t;
            a.SetViata(viata);
            a.SetSpeed(speed);
            a.SetNume(nume);
            a.SeteazaMort(mort);
            if (c == null)
                a.SetClasa(Jucator.clase["Tank"]);
            else
                a.SetClasa(c);
        }
        else
        {
            i = new Inamic();
            tip = t;
            i.SetViata(viata);
            i.SetSpeed(speed);
            i.SetNume(nume);
            i.SeteazaMort(mort);
            if (c == null)
                i.SetClasa(Jucator.clase["Tank"]);
            else
                i.SetClasa(c);
        }
    }
    public void SetTip(int t)
    {
        tip = t;
        if (t == 0)
            a = new Aliat();
        else
            i = new Inamic();
    }
    public int GetTip()
    {
        return tip;
    }
    public void SetViata(int v)
    {
        if (tip == 0)
            a.SetViata(v);
        else if (tip == 1)
            i.SetViata(v);
    }
    public int GetViata()
    {
        if (tip == 0)
            return a.GetViata();
        else if (tip == 1)
            return i.GetViata();
        return -1;
    }
    public void SetSpeed(int s)
    {
        if (tip == 0)
            a.SetSpeed(s);
        else if (tip == 1)
            i.SetSpeed(s);
    }
    public int GetSpeed()
    {
        if (tip == 0)
            return a.GetSpeed();
        else if (tip == 1)
            return i.GetSpeed();
        return -1;
    }
    public void SetNume(string s)
    {
        if (tip == 0)
            a.SetNume(s);
        else if (tip == 1)
            i.SetNume(s);
    }
    public string GetNume()
    {
        if (tip == 0)
            return a.GetNume();
        else if (tip == 1)
            return i.GetNume();
        return "";
    }
    public bool EsteMort()
    {
        if (tip == 0)
            return a.EsteMort();
        else if (tip == 1)
            return i.EsteMort();
        return false;
    }
    public void SeteazaMort(bool s)
    {
        if (tip == 0)
            a.SeteazaMort(s);
        else if (tip == 1)
            i.SeteazaMort(s);
    }
    public Clasa GetClasa()
    {
        Clasa c=new Clasa();
        if (tip == 0)
            return a.GetClasa();
        else if (tip == 1)
            return i.GetClasa();
        return c;
    }
    public void SetClasa(Clasa c)
    {
        if (tip == 0)
            a.SetClasa(c);
        else if (tip == 1)
            i.SetClasa(c);
    }
}