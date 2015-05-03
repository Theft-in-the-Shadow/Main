using UnityEngine;
using System.Collections;

public class tableaures : MonoBehaviour
{

    // Use this for initialization
    public GameObject tableau1;
    private GameObject player;
    private mouvres mouve;
    private tab tab;
    private NetworkView nt;

    void Awake()
    {
        nt = GetComponent<NetworkView>();
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
                nt.RPC("tau", RPCMode.All);
            }
        }
    }

    [RPC]
    public void tau()
    {
        mouve.tableau = true;
        tab.recu = true;
        tableau1.SetActive(false);
    }
}
