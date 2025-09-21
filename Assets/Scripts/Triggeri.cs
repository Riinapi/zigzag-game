using UnityEngine;

public class Triggeri : MonoBehaviour
{

    // Kutsutaan, kun pallo poistuu triggerin alueelta
    private void OnTriggerExit(Collider kol)
    {
        // Tarkistetaan onko poistuvan objektin tagi Pallo
        if (kol.gameObject.tag == "Pallo")
        {
            // Viiveen jälkeen kutsutaan `Putoaminen`-funktiota
            Invoke("Putoaminen", 0.5f);
        }
    }

    // Alustan putoaminen
    void Putoaminen()
    {
        // Otetaan käyttöön painovoima ja poistetaan kinematiikka objektilta
        GetComponentInParent<Rigidbody>().useGravity = true;
        GetComponentInParent<Rigidbody>().isKinematic = false;

        // Tuhoaa alustan objekti kahden sekunnin kuluttua putoamisen alkamisesta
        Destroy(transform.parent.gameObject, 2f);
    }
}
