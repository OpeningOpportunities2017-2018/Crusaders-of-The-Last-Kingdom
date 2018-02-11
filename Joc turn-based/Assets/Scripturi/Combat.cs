using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DragonBones;
public class Combat : MonoBehaviour
{
    public Queue<GameObject> ture;
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
        ture = new Queue<GameObject>();//Ordinea combatantilor la atac
    }
    void Start()
    {
        InitializareTure();
        StartCoroutine(UrmatorulCombatant());
    }
    void AdaugaTura(GameObject c)
    {
        ture.Enqueue(c);
    }
    public IEnumerator UrmatorulCombatant()
    {
        GameObject temp = ture.Dequeue();
        pot_ataca = true;
        if(tinta!=null)
            tinta.GetComponent<ParticleSystem>().Stop();
        initiator = temp;
        //Am scos jucatorul din coada. Ma ocup de el
        ture.LastOrDefault().GetComponent<ParticleSystem>().Stop();
        temp.GetComponent<ParticleSystem>().Play();
        obiect_upcoming.GetComponent<Text>().text=string.Format(text_upcoming, ture.Peek().GetComponent<Combatant>().GetNume());
        //IncarcaAbilitati(temp);
        if(initiator.GetComponent<Combatant>().GetViata()>0)
        {
            if (temp.GetComponent<Combatant>().GetTip() == 0)
            {
                IncarcaAbilitati(temp);
                //M-am ocupat de el. Il adaug inapoi in coada
                AdaugaTura(temp);
            }
            else
            {
                //Realizez aici atacul inamicului
                int indiceabilitate = Manipulatori.rand.Next(0, temp.GetComponent<Combatant>().GetClasa().ObtineAbilitati().Count);
                pot_ataca = false;
                //Debug.Log(indiceabilitate);
                //Am generat o abilitate aleatorie. Acum vad care e tinta
                DescarcaAbilitati();
                if (temp.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].GetTinta() == 0)
                {
                    //Tinta este un aliat
                    do
                    {
                        tinta = nul.GetComponent<Jucator>().combatanti[Manipulatori.rand.Next(0, nul.GetComponent<Jucator>().combatanti.Count)];
                    }
                    while (tinta.GetComponent<Combatant>().GetTip() != 0);
                    Debug.Log("Inamicul " + initiator.GetComponent<Combatant>().GetNume() + " a atacat aliatul " + tinta.GetComponent<Combatant>().GetNume() + " cu abilitatea " + temp.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].GetNume() + " si damage " + temp.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].GetDamage());
                    //Debug.Log(tinta.GetComponent<Combatant>().GetNume());
                }
                else
                {
                    //Tinta este un inamic
                    do
                    {
                        tinta = nul.GetComponent<Jucator>().combatanti[Manipulatori.rand.Next(0, nul.GetComponent<Jucator>().combatanti.Count)];
                    }
                    while (tinta.GetComponent<Combatant>().GetTip() != 1);
                    Debug.Log("Inamicul " + initiator.GetComponent<Combatant>().GetNume() + " a dat viata inamicului " + tinta.GetComponent<Combatant>().GetNume() + " cu abilitatea " + temp.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].GetNume() + " si damage " + temp.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].GetDamage());
                    //Debug.Log(tinta.GetComponent<Combatant>().GetNume());
                }
                initiator.GetComponent<UnityArmatureComponent>().animation.Play("Attack", 1);
                tinta.GetComponent<UnityArmatureComponent>().animation.Play("Damaged", 1);
                tinta.GetComponent<ParticleSystem>().Play();
                initiator.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabilitate].FacCeEDeFacut(1, tinta);
                AdaugaTura(temp);
                yield return new WaitForSeconds(nul.GetComponent<Manipulatori>().timp_intre_atacuri);
                initiator.GetComponent<UnityArmatureComponent>().animation.Play("Idle");
                tinta.GetComponent<UnityArmatureComponent>().animation.Play("Idle");
                StartCoroutine(UrmatorulCombatant());
            }
        }
        else
        {
            Destroy(temp);
            yield return new WaitForSeconds(nul.GetComponent<Manipulatori>().timp_intre_atacuri);
            //initiator.GetComponent<UnityArmatureComponent>().animation.Play("Idle");
            tinta.GetComponent<UnityArmatureComponent>().animation.Play("Idle");
            StartCoroutine(UrmatorulCombatant());
        }
    }
    bool SuntTotiMorti(int tip)
    {
        foreach(GameObject g in nul.GetComponent<Jucator>().combatanti)
        {
            if (g.GetComponent<Combatant>().GetTip() == tip && g.GetComponent<Combatant>().EsteMort() == false)
                return false;
        }
        return true;
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
