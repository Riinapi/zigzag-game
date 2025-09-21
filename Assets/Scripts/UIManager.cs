using TMPro; 
using UnityEngine.SceneManagement; 
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Muuttujat UI Managerista
    public GameObject alkuPaneeli;
    public GameObject pelinLoppuPaneeli;
    public TMP_Text pisteet; 
    public TMP_Text huippuPisteet1; 
    public TMP_Text huippuPisteet2; 
    public GameObject aloitusTeksti;
    public AudioClip peliAani;  // ƒ‰nitiedosto
    private AudioSource aaniLahde;

    public static UIManager instanssi; 

    private void Awake()
    {
        if (instanssi == null)
        {
            instanssi = this;
        }
        if (aaniLahde == null)
        {
            aaniLahde = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
       
        // N‰ytet‰‰n korkeimmat pisteet aloituspaneelissa
        huippuPisteet1.text = "Huippu pisteet: " + PlayerPrefs.GetInt("huippuPisteet").ToString();

        // Asetetaan aloituspaneeli ja -teksti n‰kyv‰ksi, kun peli alkaa
        alkuPaneeli.SetActive(true);
        aloitusTeksti.SetActive(true);
        aaniLahde = gameObject.GetComponent<AudioSource>();

        // Soitetaan ‰‰ni
        if (peliAani != null && aaniLahde != null)
        {
            aaniLahde.PlayOneShot(peliAani);
        }

    }

    // Funktio pelin aloitustoimintojen suorittamiseen
    public void PelinAlku()
    {
        // K‰ynnistet‰‰n aloitus paneelin ja aloita tekstin animaatio 
        alkuPaneeli.GetComponent<Animator>().Play("PaneeliYlos");
        aloitusTeksti.GetComponent<Animator>().Play("AloitaTekstiAlas");

        // P‰ivitet‰‰n huippupisteet 
        huippuPisteet1.text = PlayerPrefs.GetInt("huippuPisteet").ToString();
    }

    // Funktio pelin lopetustoimintojen suorittamiseen
    public void PelinLoppu()
    {
        pelinLoppuPaneeli.SetActive(true); // N‰ytet‰‰n loppu paneeli
        pelinLoppuPaneeli.GetComponent<Animator>().Play("PaneeliAlas"); // K‰ynnistet‰‰n loppu paneelin animaatio

        // P‰ivitet‰‰n lopulliset pisteet ja huippupisteet 
        pisteet.text = PlayerPrefs.GetInt("piste").ToString();
        huippuPisteet2.text = PlayerPrefs.GetInt("huippuPisteet").ToString();
    }

    // Funktio pelin uudelleenaloitukseen
    public void AloitaAlusta()
    {
        SceneManager.LoadScene(0); // Ladataan pelin ensimm‰inen scene uudelleen
    }
}
