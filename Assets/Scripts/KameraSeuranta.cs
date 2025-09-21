using UnityEngine;
using UnityEngine.UIElements;

public class KameraSeuranta : MonoBehaviour
{
    public GameObject pallo;
    // Kameran ja pallon v‰linen l‰htˆet‰isyys
    Vector3 offset;
    public float tarinaVaihe;
    public bool loppu;

    void Start()
    {
        // Lasketaan kameran ja pallon v‰linen et‰isyys ja tallennetaan se offset-muuttujaan
        offset = pallo.transform.position - transform.position;

        // Alustetaan loppu-muuttuja ep‰todeksi, jolloin seuranta on aktiivinen
        loppu = false;
    }

    // Update suoritetaan jokaisella ruudunp‰ivityksell‰
    void Update()
    {
        // Jos loppu on ep‰tosi, kutsutaan Seurata-funktiota
        if (!loppu)
        {
            Seurata();
        }
    }

    // Funktio, joka p‰ivitt‰‰ kameran paikan seuraamaan pallon liikett‰
    void Seurata()
    {
        // Nykyinen kameran paikka
        Vector3 paikka = transform.position;

        // Tavoiteltu kameran paikka, joka on aina offsetin p‰‰ss‰ pallosta
        Vector3 tavoitePaikka = pallo.transform.position - offset;

        // P‰ivitet‰‰n kameran paikka kohti tavoitePaikkaa tarinaVaihe-arvon nopeudella
        paikka = Vector3.Lerp(paikka, tavoitePaikka, tarinaVaihe * Time.deltaTime);

        // Asetetaan kameran uusi paikka
        transform.position = paikka;
    }
}
