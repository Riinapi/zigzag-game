using UnityEngine;

public class PeliManager : MonoBehaviour
{

    public static PeliManager instanssi;
    public bool peliLoppu;

    private void Awake()
    {
        if (instanssi == null)
        {
            instanssi = this;
        }
    }

    void Start()
    {
        peliLoppu = false; 
    }

    // Pelin aloitus
    public void AloitaPeli()
    {
        // Kutsutaan UIManagerin metodia 
        UIManager.instanssi.PelinAlku();

        // K‰ynnistet‰‰n alustojen kopiointi kutsumalla Laajentaja-objektin spawn-funktiota
        GameObject.Find("Laajentaja").GetComponent<Spawning>().AloitaAlustojenKopiointi();
    }

    // Pelin lopetus
    public void LopetaPeli()
    {
        peliLoppu = true;  // Pelin loppu tosi

        // Kutsutaan UIManagerin metodia 
        UIManager.instanssi.PelinLoppu();

        // Kutsutaan PisteManagerin metodia
        PisteManager.instanssi.LopetaPisteet();
    }

    // Suljetaan peli metodi
    public void SuljePeli()
    {
        Application.Quit();
    }
}
