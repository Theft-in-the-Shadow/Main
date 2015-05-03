using UnityEngine;
using System.Collections;

public class tableaures : MonoBehaviour
{

    // Use this for initialization
    public GameObject tableau1;
    private GameObject player;
    private mouvres mouve;
    private tab tab;

    void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        mouve = GameObject.Find("VEHICLE_SUV").GetComponent<mouvres>();
        tab = GameObject.FindGameObjectWithTag("main").GetComponent<tab>();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if (Input.GetButton("Bombe"))
            {
                mouve.tableau = true;
                tab.recu = true;
                tableau1.SetActive(false);
            }
        }
    }
}
