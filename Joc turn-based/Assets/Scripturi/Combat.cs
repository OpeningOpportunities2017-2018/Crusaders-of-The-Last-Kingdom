using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class Combat : MonoBehaviour
{
    public Queue<GameObject> ture;
    public GameObject nul;
    public GameObject initiator, tinta;//Cel care initiaza atacul si tinta atacului
    public int tip_tinta = -1,indiceabilitate=-1;//Astept playerul sa isi selecteze tinta? Nu? -1 Daca da, aliatul e 0, inamicul 1
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
        UrmatorulCombatant();
    }
    void AdaugaTura(GameObject c)
    {
        ture.Enqueue(c);
    }
    public GameObject UrmatorulCombatant()
    {
        GameObject temp = ture.Dequeue();
        initiator = temp;
        //Am scos jucatorul din coada. Ma ocup de el
        ture.LastOrDefault().GetComponent<ParticleSystem>().Stop();
        temp.GetComponent<ParticleSystem>().Play();
        obiect_upcoming.GetComponent<Text>().text=string.Format(text_upcoming, ture.Peek().GetComponent<Combatant>().GetNume());
        IncarcaAbilitati(temp);
        //M-am ocupat de el. Il adaug inapoi in coada
        AdaugaTura(temp);
        //L-am adaugat. In caz ca trebuie, returnez si ce jucator a fost acum.
        return temp;
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
        Debug.Log(initiator.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabil].GetNume());
        indiceabilitate = indiceabil;
        if (initiator.GetComponent<Combatant>().GetTip() == 1)
        {
            switch(initiator.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabil].GetTarget())
            {
                case 0:
                    {
                        tip_tinta = 1;
                        break;
                    }
                case 1:
                    {
                        tip_tinta = 0;
                        break;
                    }
                default:
                    {
                        Debug.Log("Ceva nu e bine");
                        break;
                    }
            }
        }
        else
            tip_tinta = initiator.GetComponent<Combatant>().GetClasa().ObtineAbilitati()[indiceabil].GetTarget();
    }
    void IncarcaAbilitati(GameObject comb)
    {
        //Voi actualiza aici sloturile cu abilitati
        //Pentru inceput, ma asigur ca sunt toate oprite
        foreach(GameObject g in slotabil)
        {
            g.SetActive(false);
        }
        //Acum copii abilitatile temporar aici, pentru optimizare
        List<Abilitate> abilitati = new List<Abilitate>(comb.GetComponent<Combatant>().GetClasa().ObtineAbilitati());
        //Le pornesc in functie de cate sunt si le incarc si pictograma
        for (int i=0;i<abilitati.Count;i++)
        {
            slotabil[i].SetActive(true);
            slotabil[i].GetComponent<Image>().sprite = abilitati[i].GetPictograma();
        }
    }
    void Update ()
    {
		
	}
}
