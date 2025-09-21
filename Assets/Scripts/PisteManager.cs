using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PisteManager : MonoBehaviour
{
    public static PisteManager instanssi;

    // Piste muuttujat
    public int pisteet; 
    public int huippuPisteet; 

    private void Awake()
    {
        if (instanssi == null)
        {
            instanssi = this;
        }
    }

    void Start()
    {
        pisteet = 0; // Alustetaan pisteet 
        PlayerPrefs.SetInt("piste", pisteet); // Tallennetaan alustetut pisteet pysyv�ismuistiin
    }

    // Funktio pisteiden lis��miseen
    public void LisaaPisteita()
    {
        pisteet += 1; // Kasvatetaan pisteit� yhdell�
    }

    // Funktio, joka tallentaa pisteet ja p�ivitt�� huippupisteet, jos ne ovat suuremmat kuin aiemmat
    public void LopetaPisteet()
    {
        // Tallennetaan nykyiset pisteet 
        PlayerPrefs.SetInt("piste", pisteet);

        // Tarkistetaan, onko huippuPisteet jo tallennettu aiemmin
        if (PlayerPrefs.HasKey("huippuPisteet"))
        {
            // Jos nykyiset pisteet ovat suuremmat kuin aiemmat huippupisteet, p�ivitet��n huippupisteet
            if (pisteet > PlayerPrefs.GetInt("huippuPisteet"))
            {
                PlayerPrefs.SetInt("huippuPisteet", pisteet);
            }
        }
        else
        {
            // Jos huippupisteit� ei viel� ole tallennettu, asetetaan nykyiset pisteet huippupisteiksi
            PlayerPrefs.SetInt("huippuPisteet", huippuPisteet);
        }
    }
}
