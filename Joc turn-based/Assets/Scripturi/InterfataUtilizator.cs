using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InterfataUtilizator : MonoBehaviour
{
    /*Lista cu semnificatia fiecarui buton din scena, ordonati in functie de indice, de la 0. Alta ordine sau lipsa butoanelor va genera erori.
     * Scena Main Menu:
     * Start(se refera la main-menu)
     * Cum se joaca
     * Optiuni
     * Credite
     * Exit
     * Scena Main:
     * 
     */
    /*Lista cu semnificatia fiecarui panou din scena, ordonati in functie de indice, de la 0. Alta ordine sau lipsa panourilor va genera erori.
     * Scena Main Menu:
     * Cum se joaca
     * Optiuni
     * Credite
     * Scena Main:
     * Pause Menu
    */
    /*De asemenea, scenele trebuie puse in ordine la build
     * 0 - Main Menu
     * 1 - Main
     */
     /*Ordinea limbilor este
      * romana
      */
    public List<GameObject> butoane = new List<GameObject>();
    public List<GameObject> panouri = new List<GameObject>();
    public Dictionary<string, string> engleza = new Dictionary<string, string>();//Aceste dictionare pot fi folosite pentru a traduce usor un text din romana in alta limba. De ex. engleza["Salut"]="Hello"
    int limba = 0, bindex;
    int indice_buton = 0;
    GameObject buton_selectat;
    public void ButonExit()
    {
        if(bindex==0)
        {
            if (panouri[0].activeSelf)
            {
                Debug.Log("Nu se mai afiseaza sfaturile");
                panouri[0].SetActive(false);
            }
            else if (panouri[1].activeSelf)
            {
                Debug.Log("Nu se mai afiseaza optiunile");
                panouri[1].SetActive(false);
            }
            else if (panouri[2].activeSelf)
            {
                Debug.Log("Nu se mai afiseaza creditele");
                panouri[2].SetActive(false);
            }
            else
            {
                Debug.Log("Am iesit din joc.");
                Application.Quit();
            }
        }
        else if(bindex==1)
        {
            if(panouri[0].activeSelf)
            {
                Debug.Log("M-am intors in main menu.");
                SceneManager.LoadScene(0);
            }
            else if(panouri[1].activeSelf)
            {
                Debug.Log("Nu se mai afiseaza optiunile");
                panouri[1].SetActive(false);
                panouri[0].SetActive(true);
            }
        }
    }
    public void ButonStart()
    {
        if(SceneManager.GetActiveScene().buildIndex==0)
        {
            Debug.Log("Am incarcat scena 1");
            SceneManager.LoadScene(1);
        }
    }
    public void ButonCumSeJoacă()
    {
        Debug.Log("Am incarcat sfaturile");
        panouri[0].SetActive(true);
    }
    public void ButonOptiuni()
    {
        if(SceneManager.GetActiveScene().buildIndex==1)
        {
            panouri[1].SetActive(true);
            panouri[0].SetActive(false);
        }
        Debug.Log("Am incarcat optiunile");
    }
    public void ButonCredite()
    {
        Debug.Log("Am incarcat creditele");
        panouri[2].SetActive(true);
    }
    void Awake()
    {
        bindex = SceneManager.GetActiveScene().buildIndex;
    }
    void Start ()
    {
        foreach(GameObject g in panouri)
        {
            g.SetActive(false);
        }
        buton_selectat = butoane[0];
	}
	void Update ()
    {
		if(bindex==1)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Am apasat tasta Escape");
                indice_buton = -1;
                panouri[0].SetActive(!panouri[0].activeSelf);
            }
            if(panouri[0].activeSelf)
            {
                /*if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    indice_buton++;
                    if (indice_buton >= 4)
                        indice_buton = 0;
                    buton_selectat = butoane[indice_buton];
                    buton_selectat.GetComponent<Image>().sprite = buton_selectat.GetComponent<Button>().spriteState.pressedSprite;
                    Manipulatori.Swap(ref buton_selectat.GetComponent<Image>().sprite, ref buton_selectat.GetComponent<Button>().spriteState.pressedSprite);
                }
                */
            }
        }
	}
}
