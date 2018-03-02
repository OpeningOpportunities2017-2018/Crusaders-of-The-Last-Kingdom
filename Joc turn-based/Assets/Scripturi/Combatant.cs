using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using DragonBones;
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
[System.Serializable]
public class Combatant:MonoBehaviour,ICombatant
{
    Aliat a;
    Inamic i;
    int viatainit=1;
    int tip=0;//Deocamdata 0-Aliat,1-Inamic
    public GameObject nul;//Obiectul cu scripturi
    void Start()
    {
        nul = Jucator.obnul;
    }
    public Combatant(Clasa c,int t=0,int viata=100,int speed=1,string nume="",bool mort=false)
    {
        viatainit = viata;
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
    public int GetViataInit()
    {
        if (viatainit == 0)
            return 1;
        return viatainit;
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
    public void GiveViata(int v)
    {
        if (tip == 0)
        {
            if (!a.EsteMort())
            {
                if (a.GetViata() <= 0)
                    a.SeteazaMort(true);
                else
                    a.SetViata(a.GetViata() + v);
            }
        }
        else if (tip == 1)
        {
            if (!i.EsteMort())
            {
                if (i.GetViata() <= 0)
                    i.SeteazaMort(true);
                else
                    i.SetViata(i.GetViata() + v);
            }
        }
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
    void OnMouseDown()
    {
        if(nul.GetComponent<Combat>().initiator.GetComponent<Combatant>().GetTip()==0)
        {
            //if (nul.GetComponent<Combat>().pot_ataca == true && !nul.GetComponent<InterfataUtilizator>().EsteVreunPanouActiv() && nul.GetComponent<Combat>().tip_tinta != -1 && nul.GetComponent<Combat>().initiator.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[nul.GetComponent<Combat>().indiceabilitate].GetTinta() == nul.GetComponent<Combat>().tip_tinta && nul.GetComponent<Combat>().initiator != gameObject)
            if (nul.GetComponent<Combat>().pot_ataca == true && !nul.GetComponent<InterfataUtilizator>().EsteVreunPanouActiv() && nul.GetComponent<Combat>().tip_tinta != -1 && nul.GetComponent<Combat>().initiator != gameObject)
            {
                Debug.Log("Cineva a dat click pe " + name);
                StartCoroutine(UltimulPas());
            }
        }
    }
    IEnumerator UltimulPas()
    {
        if(GetViata()>0&&nul.GetComponent<Combat>().initiator.GetComponent<Combatant>().GetViata()>0)
        {
            nul.GetComponent<Combat>().pot_ataca = false;
            gameObject.GetComponent<UnityArmatureComponent>().animation.Play("Damaged", 1);
            nul.GetComponent<Combat>().initiator.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[nul.GetComponent<Combat>().indiceabilitate].FacCeEDeFacut(0, gameObject);
            //transform.GetChild(transform.childCount - 1).GetComponent<HealthBar>().fill = GetViata();
            //transform.GetChild(transform.childCount - 1).GetComponent<HealthBar>().Updatare(GetViataInit());
            gameObject.GetComponent<ParticleSystem>().Play();
            nul.GetComponent<Combat>().tinta = gameObject;
            yield return new WaitForSeconds(nul.GetComponent<Manipulatori>().timp_intre_atacuri);
            gameObject.GetComponent<UnityArmatureComponent>().animation.Play("Idle");
            nul.GetComponent<Combat>().initiator.GetComponent<UnityArmatureComponent>().animation.Play("Idle");
        }
        StartCoroutine(nul.GetComponent<Combat>().UrmatorulCombatant());
        nul.GetComponent<Combat>().tip_tinta = -1;
    }
    void OnMouseOver()
    {
        nul.GetComponent<Combat>().obiect_detalii.GetComponent<Text>().text = string.Format(nul.GetComponent<Combat>().text_detalii,GetNume(),GetClasa().GetNume(),GetTip(),GetViata(),GetSpeed());
    }
}