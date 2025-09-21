using UnityEngine;

public class PallonLiike : MonoBehaviour
{
    [SerializeField]
    private float nopeus;

    // Rigidbody-komponentti, joka hallitsee pallon fysiikkaa
    Rigidbody rb;
    bool alku;
    bool pelinLoppu;
    // Partikkeli-objekti, joka n‰kyy timantin tuhoutuessa
    public GameObject partikkeli;
    // Ensimm‰inen alusta jossa pallo on
    public GameObject lahtoAlusta;
    public AudioClip poistoAani;  // ƒ‰nitiedosto
    private AudioSource audioLahde; // ƒ‰nen toistaja
     // ƒ‰nen toistaja
    private void Awake()
    {
        // Haetaan Rigidbody-komponentti
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Alustetaan peli alkutilaan
        alku = false;
        pelinLoppu = false;

        // Haetaan tai lis‰t‰‰n AudioSource
        audioLahde = gameObject.GetComponent<AudioSource>();
        if (audioLahde == null)
        {
            audioLahde = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (!alku)
        {
            // Jos hiirt‰ painetaan, pallo l‰htee liikkeelle
            if (Input.GetMouseButtonDown(0))
            {
                // Asetetaan pallolle alkuvauhti x-akselin suuntaan
                rb.linearVelocity = new Vector3(nopeus, 0, 0);
                alku = true;

                // Aloitetaan peli kutsumalla PeliManager-luokan AloitaPeli-funktio
                PeliManager.instanssi.AloitaPeli();
                // Tuhotaan iso alusta 5s kuluttua
                Destroy(lahtoAlusta, 5f);
            }
        }

        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        // Tarkistetaan, onko pallo viel‰ alustalla
        if (!Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            // Jos alustaa ei ole alla, peli loppuu ja pallo putoaa alasp‰in
            pelinLoppu = true;
            rb.linearVelocity = new Vector3(0, -25f, 0);

            // Kamera pys‰htyy seuraamasta
            Camera.main.GetComponent<KameraSeuranta>().loppu = true;

            // Lopetetaan peli PeliManager-luokan avulla
            PeliManager.instanssi.LopetaPeli();
        }

        // Jos peli ei ole loppunut ja hiirt‰ painetaan, kutsutaan SuunnanVaihto-funktiota
        if (Input.GetMouseButtonDown(0) && !pelinLoppu)
        {
            SuunnanVaihto();
        }
    }

    // Funktio, joka vaihtaa pallon liikesuuntaa
    void SuunnanVaihto()
    {
        // Jos pallo liikkuu z-akselin suuntaan, muutetaan se liikkumaan x-akselilla
        if (rb.linearVelocity.z > 0)
        {
            rb.linearVelocity = new Vector3(nopeus, 0, 0);
        }
        // Jos pallo liikkuu x-akselilla, muutetaan se liikkumaan z-akselilla
        else if (rb.linearVelocity.x > 0)
        {
            rb.linearVelocity = new Vector3(0, 0, nopeus);
        }
    }

    // Funktio, joka aktivoituu, kun pallo osuu timanttiin
    void OnTriggerEnter(Collider kohde)
    {
        // Tarkistetaan, onko kohde-objektilla "Timantti"-tag
        if (kohde.gameObject.CompareTag("Timantti"))
        {
            // Suoritetaan partikkeli
            GameObject osa = Instantiate(partikkeli, kohde.gameObject.transform.position, Quaternion.identity);

            if (kohde.gameObject != null)
            {
                // Pienennet‰‰n timantin kokoa
                kohde.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

                // Soitetaan ‰‰ni
                if (poistoAani != null && audioLahde != null)
                {
                    audioLahde.PlayOneShot(poistoAani);
                }

                // Tuhotaan timantti hetken kuluttua
                Destroy(kohde.gameObject);
                Destroy(osa.gameObject, 1f);

                // Lis‰t‰‰n pisteit‰
                PisteManager.instanssi.LisaaPisteita();
            }
        }
    }
}
