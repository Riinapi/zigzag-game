using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject timantti; 
    public GameObject alusta;   
    private Vector3 viimeinenSijainti; 
    private float koko; 
    bool loppu;

    void Start()
    {
        // Asetetaan aloitussijainti ensimm‰iselle alustalle
        viimeinenSijainti = alusta.transform.position;

        // Haetaan alustan koko X-akselin mittana
        koko = alusta.transform.localScale.x;
    }

    void Update()
    {
        // Tarkistetaan, onko peli viel‰ k‰ynniss‰
        if (PeliManager.instanssi.peliLoppu == true)
        {
            CancelInvoke("AlustaSiirtymat"); // Kutsutaan alustan siirtym‰toiminto
        }
    }

    // Siirt‰‰ seuraavan alustan X-akselin suuntaisesti
    void SiirtymaX()
    {
        // M‰‰ritt‰‰ uuden sijainnin viimeisen alustan viereen x-akselilla
        Vector3 sij = viimeinenSijainti;
        sij.x += koko;

        // P‰ivitet‰‰n uusi sijainti seuraavaa alustaa varten
        viimeinenSijainti = sij;

        // Luodaan uusi alusta p‰ivitettyyn sijaintiin
        Instantiate(alusta, sij, Quaternion.identity);

        // Satunnaisesti asetetaan timantti alustalle
        int sat = Random.Range(0, 4);
        if (sat < 1)
        {
            Instantiate(timantti, new Vector3(sij.x, sij.y + 1, sij.z), timantti.transform.rotation);
        }
    }

    // Siirt‰‰ seuraavan alustan Z-akselin suuntaisesti
    void SiirtymaZ()
    {
        // M‰‰ritt‰‰ uuden sijainnin viimeisen alustan viereen z-akselilla
        Vector3 sij = viimeinenSijainti;
        sij.z += koko;

        // P‰ivitet‰‰n uusi sijainti seuraavaa alustaa varten
        viimeinenSijainti = sij;

        // Luodaan uusi alusta p‰ivitettyyn sijaintiin
        Instantiate(alusta, sij, Quaternion.identity);

        // Satunnaisesti asetetaan timantti alustalle
        int sat = Random.Range(0, 4);
        if (sat < 1)
        {
            Instantiate(timantti, new Vector3(sij.x, sij.y + 1, sij.z), timantti.transform.rotation);
        }
    }

    // Valitsee satunnaisesti, kumpaa siirtym‰‰ (X- tai Z-akselin suuntainen) kutsutaan
    void AlustaSiirtymat()
    {
        // Jos peli on loppu, lopetetaan uusien alustojen luonti
        if (loppu)
        {
            
            return;
        }

        // Satunnaisluku valitsee, kumpaa suuntaa siirryt‰‰n
        int satu = Random.Range(0, 6);
        if (satu < 3)
        {
            SiirtymaX(); // Siirryt‰‰n x-akselin suuntaisesti
        }
        else
        {
            SiirtymaZ(); // Siirryt‰‰n z-akselin suuntaisesti
        }
    }

    // Aloittaa alustojen kopioinnin s‰‰nnˆllisin v‰liajoin
    public void AloitaAlustojenKopiointi()
    {
        // K‰ynnist‰‰ `AlustaSiirtymat`-funktion kutsumisen toistuvasti 0.2 sekunnin v‰lein, kahden sekunnin viiveell‰
        InvokeRepeating("AlustaSiirtymat", 2f, 0.2f);
    }
}
