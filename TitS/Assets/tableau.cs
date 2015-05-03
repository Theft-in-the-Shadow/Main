using UnityEngine;
using System.Collections;

public class tableau : MonoBehaviour
{

    // Use this for initialization
    public GameObject tableau1;
    public GameObject tableau2;
    private GameObject player;
    private mouv mouve;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mouve = GameObject.Find("VEHICLE_SUV").GetComponent<mouv>();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if (Input.GetButton("Bombe"))
            {
                tableau2.SetActive(true);
                mouve.tableau = true;
                tableau1.SetActive(false);
            }
        }
    }
}
