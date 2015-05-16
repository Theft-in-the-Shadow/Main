using UnityEngine;
using System.Collections;

public class laseres : MonoBehaviour
{
    public GameObject laser;

    private PlayerMovements playerMovement; 
    private NetworkView NetworkView;
    private GameObject player;
    private GameObject player2;
    private int nb_joueur;
    private bool init = false;

    private bool triggered;
    private bool LaserIsActivated;
    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        playerMovement = player.GetComponent<PlayerMovements>();
        NetworkView = GetComponent<NetworkView>();
        triggered = false;
        LaserIsActivated = true;
        timer = 5;
    }

    void Update()
    {
        nb_joueur = Network.connections.Length + 1;
        if (!init && nb_joueur == 2)
        {
            player2 = GameObject.FindGameObjectWithTag("player2");
            init = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player || other.gameObject == player2)
        {
            if (Input.GetButton("Fire2"))
            {
                networkView.RPC("update", RPCMode.Others);
                LaserIsActivated = false;
                laser.renderer.enabled = false;
                laser.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            triggered = true;
        }
    }
    void TriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            triggered = false;
        }
    }
    [RPC]
    public void update()
    {
        LaserIsActivated = false;
        laser.renderer.enabled = false;
        laser.SetActive(false);
    }

}

