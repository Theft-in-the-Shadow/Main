using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ombre : MonoBehaviour {

    private GameObject player;
    public GameObject cercle;
    private Image couleur;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        couleur = cercle.GetComponent<Image>();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            couleur.color = Color.green;
            player.GetComponent<DonePlayerHealth>().ombre = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        // If the player leaves the trigger zone...
        if (other.gameObject == player)
        {
            // ... the player is not in sight.
            couleur.color = Color.red;
            player.GetComponent<DonePlayerHealth>().ombre = false;
        }
            
    }
}
