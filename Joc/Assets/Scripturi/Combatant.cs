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