using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        Debug.Log("Am incarcat optiunile");
        panouri[1].SetActive(true);
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
	}
	void Update ()
    {
		if(bindex==1)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Am apasat tasta Escape");
                panouri[0].SetActive(!panouri[0].activeSelf);
            }
        }
	}
}
