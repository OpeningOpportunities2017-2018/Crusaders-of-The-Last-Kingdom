using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DragonBones;
public class Combat : MonoBehaviour
{
    public List<GameObject> ture;
    public GameObject nul;
    public GameObject initiator, tinta;//Cel care initiaza atacul si tinta atacului
    public int tip_tinta = -1,indiceabilitate=-1;//Astept playerul sa isi selecteze tinta? Nu? -1 Daca da, aliatul e 0, inamicul 1
    public bool pot_ataca = true;
    //Astea is pentru a arata cine urmeaza si detalii despre jucatorul cu mouse-ul peste el
    string text_upcoming = "Următorul combatant:{0}";
    public string text_detalii = "Nume:{0}\nClasa:{1}\nTip:{2}\nViata:{3}\nSpeed:{4}";
    public GameObject obiect_upcoming;
    public GameObject obiect_detalii;
    //
    public List<GameObject> slotabil;//Sloturile de abilitati
    void Awake()
    {
        ture = new List<GameObject>();//Ordinea combatantilor la atac
    }
    void Start()
    {
        InitializareTure();
        StartCoroutine(PrimaData(nul.GetComponent<Manipulatori>().timp_intre_atacuri));
    }
    void AdaugaTura(GameObject c)
    {
        ture.Add(c);
    }
    GameObject PrimulLaRand()
    {
        return ture.First(g=>g!=null);
    }
    GameObject UltimulLaRand()
    {
        return ture[ture.Count - 1];
    }
    public int IndiceCombatant(GameObject g)
    {
        return ture.IndexOf(g);
    }
    public IEnumerator PrimaData(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(UrmatorulCombatant());
    }
    public IEnumerator UrmatorulCombatant()
    {
        if (SuntTotiMorti(0) == true)
        {
            Debug.Log("Ai pierdut ma");
        }
        else if (SuntTotiMorti(1) == true)
        {
            Debug.Log("Ai castigat!!!");
        }
        else
        {
            //Se mai lupta lumea dom'le
            initiator = PrimulLaRand();
            ture[IndiceCombatant(initiator)] = null;
            DescarcaAbilitati();
            if (tinta != null)
                if(tinta.activeSelf)
                    tinta.GetComponent<ParticleSystem>().Stop();
            //Am scos jucatorul din coada. Ma ocup de el
            if (UltimulLaRand()!=null)
                UltimulLaRand().GetComponent<ParticleSystem>().Stop();
            initiator.GetComponent<ParticleSystem>().Play();
            obiect_upcoming.GetComponent<Text>().text = string.Format(text_upcoming, PrimulLaRand().GetComponent<Combatant>().GetNume());
            //if (initiator.GetComponent<Combatant>().GetViata() > 0)
            //{
                if (initiator.GetComponent<Combatant>().GetTip() == 0)
                {
                    pot_ataca = true;
                    IncarcaAbilitati(initiator);
                    //M-am ocupat de el. Il adaug inapoi in coada
                    AdaugaTura(initiator);
                }
                else
                {
                    //Realizez aici atacul inamicului
                    int indiceabilitate = Manipulatori.rand.Next(0, initiator.GetComponent<Combatant>().GetClasa().ObtineAbilitati().Count);
                    pot_ataca = false;
                    //Debug.Log(indiceabilitate);
                    //Am generat o abilitate aleatorie. Acum vad care e tinta
                    if (initiator.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].GetTinta() == 1)
                    {
                        do
                        {
                            //Cat timp gasesc combatanti morti sau care nu sunt aliati
                            tinta = nul.GetComponent<Jucator>().combatanti[Manipulatori.rand.Next(0, nul.GetComponent<Jucator>().combatanti.Count)];
                        }
                        while (tinta.GetComponent<Combatant>().GetTip() == 1);
                        //Debug.Log(temp.name + " " + tinta.name + " " + tinta.GetComponent<Combatant>().GetViata() + " " + temp.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].GetNume() + " " + temp.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].GetDamage());
                    }
                    else if (initiator.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].GetTinta() == 0)
                    {
                        do
                        {
                            //Cat timp gasesc combatanti morti sau care nu sunt inamici
                            tinta = nul.GetComponent<Jucator>().combatanti[Manipulatori.rand.Next(0, nul.GetComponent<Jucator>().combatanti.Count)];
                        }
                        while (tinta.GetComponent<Combatant>().GetTip() == 0);
                        //Debug.Log(temp.name + " " + tinta.name + " " + tinta.GetComponent<Combatant>().GetViata() + " " + temp.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].GetNume() + " " + temp.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].GetDamage());
                    }
                    initiator.GetComponent<UnityArmatureComponent>().animation.Play("Attack", 1);
                    tinta.GetComponent<UnityArmatureComponent>().animation.Play("Damaged", 1);
                    tinta.GetComponent<ParticleSystem>().Play();
                    initiator.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].FacCeEDeFacut(1, tinta);
                    yield return new WaitForSeconds(nul.GetComponent<Manipulatori>().timp_intre_atacuri);
                    initiator.GetComponent<UnityArmatureComponent>().animation.Play("Idle");
                    if (tinta.GetComponent<Combatant>().GetViata()<=0&&IndiceCombatant(tinta)!=-1)
                    {
                        //Victima atacului a murit
                        ture.RemoveAt(IndiceCombatant(tinta));
                    }
                    else
                    {
                        tinta.GetComponent<UnityArmatureComponent>().animation.Play("Idle");
                        AdaugaTura(initiator);
                    }
                    StartCoroutine(UrmatorulCombatant());
                //}
            }
            /*else
            {
                //Cel care a initiat atacul a murit
                Debug.Log(initiator.name+" a fost sters");
                yield return new WaitForSeconds(nul.GetComponent<Manipulatori>().timp_intre_atacuri);
                //initiator.GetComponent<UnityArmatureComponent>().animation.Play("Idle");
                //tinta.GetComponent<UnityArmatureComponent>().animation.Play("Idle");
                ture.RemoveAt(IndiceCombatant(tinta));
                StartCoroutine(UrmatorulCombatant());
            }*/
        }
    }
    public void MousePesteAbilitate(int indice)
    {
        nul.GetComponent<InterfataUtilizator>().stat_abil.SetActive(true);
        Abilitate a = initiator.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indice];
        Vector3 pozitie = nul.GetComponent<Combat>().slotabil[indice].GetComponent<RectTransform>().position;
        pozitie.y = 220;
        nul.GetComponent<InterfataUtilizator>().stat_abil.transform.GetChild(0).GetComponent<Text>().text = string.Format(nul.GetComponent<InterfataUtilizator>().stat_abil_text,a.GetNume(),a.GetTinta(),a.GetDamage());
        nul.GetComponent<InterfataUtilizator>().stat_abil.GetComponent<RectTransform>().position = pozitie;
    }
    public bool SuntTotiMorti(int tip)
    {
        foreach(GameObject g in ture)
        {
            if(g!=null)
                if (g.GetComponent<Combatant>().GetTip() == tip && g.GetComponent<Combatant>().EsteMort() == false)
                    return false;
        }
        return true;
    }
    public void AfiseazaCoada()
    {
        List<GameObject> temp = new List<GameObject>(ture);
        foreach(GameObject g in temp)
        {
            Debug.Log(g.name + " " + g.activeSelf);
        }
    }
    void InitializareTure()
    {
        //Copie intr-o lista toti combatantii, ii sorteaza dupa speed si apoi ii baga in coada
        List<GameObject> temp = new List<GameObject>();
        GameObject aux;
        foreach (GameObject g in nul.GetComponent<Jucator>().combatanti)
        {
            temp.Add(g);
        }
        for (int i = 0; i < temp.Count - 1; i++)
        {
            for (int j = i + 1; j < temp.Count; j++)
            {
                if (temp[i].GetComponent<Combatant>().GetSpeed() < temp[j].GetComponent<Combatant>().GetSpeed())
                {
                    aux = temp[i];
                    temp[i] = temp[j];
                    temp[j] = aux;
                }
            }
        }
        foreach (GameObject g in temp)
        {
            AdaugaTura(g);
        }
    }
    public void ClickPeAbilitate(int indiceabil)
    {
        //Debug.Log(initiator.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabil].GetNume());
        if(pot_ataca)
        {
            indiceabilitate = indiceabil;
            tip_tinta = initiator.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabil].GetTinta();
        }
    }
    void IncarcaAbilitati(GameObject comb)
    {
        //Voi actualiza aici sloturile cu abilitati
        //Pentru inceput, ma asigur ca sunt toate oprite
        DescarcaAbilitati();
        //Acum copii abilitatile temporar aici, pentru optimizare
        List<Abilitate> abilitati = new List<Abilitate>(comb.GetComponent<Combatant>().GetClasa().ObtineAbilitati());
        //Le pornesc in functie de cate sunt si le incarc si pictograma
        for (int i=0;i<abilitati.Count;i++)
        {
            slotabil[i].SetActive(true);
            slotabil[i].GetComponent<Image>().sprite = abilitati[i].GetPictograma();
        }
    }
    void DescarcaAbilitati()
    {
        foreach (GameObject g in slotabil)
            g.SetActive(false);
    }
    void Update ()
    {
		
	}
}
//Debug.Log("Inamicul " + initiator.GetComponent<Combatant>().GetNume() + " a atacat aliatul " + tinta.GetComponent<Combatant>().GetNume() + " cu abilitatea " + temp.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].GetNume() + " si damage " + temp.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].GetDamage());
//Debug.Log("Inamicul " + initiator.GetComponent<Combatant>().GetNume() + " a dat viata inamicului " + tinta.GetComponent<Combatant>().GetNume() + " cu abilitatea " + temp.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].GetNume() + " si damage " + temp.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].GetDamage());