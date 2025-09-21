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
        PlayerPrefs.SetInt("piste", pisteet); // Tallennetaan alustetut pisteet pysyväismuistiin
    }

    // Funktio pisteiden lisäämiseen
    public void LisaaPisteita()
    {
        pisteet += 1; // Kasvatetaan pisteitä yhdellä
    }

    // Funktio, joka tallentaa pisteet ja päivittää huippupisteet, jos ne ovat suuremmat kuin aiemmat
    public void LopetaPisteet()
    {
        // Tallennetaan nykyiset pisteet 
        PlayerPrefs.SetInt("piste", pisteet);

        // Tarkistetaan, onko huippuPisteet jo tallennettu aiemmin
        if (PlayerPrefs.HasKey("huippuPisteet"))
        {
            // Jos nykyiset pisteet ovat suuremmat kuin aiemmat huippupisteet, päivitetään huippupisteet
            if (pisteet > PlayerPrefs.GetInt("huippuPisteet"))
            {
                PlayerPrefs.SetInt("huippuPisteet", pisteet);
            }
        }
        else
        {
            // Jos huippupisteitä ei vielä ole tallennettu, asetetaan nykyiset pisteet huippupisteiksi
            PlayerPrefs.SetInt("huippuPisteet", huippuPisteet);
        }
    }
}
