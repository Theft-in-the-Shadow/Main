using UnityEngine;
using System.Collections;

public class desres : MonoBehaviour
{
    public GameObject camera;
    public TextMesh txt;

    private bool triggered;
    private bool IsActivated;
    private GameObject player;
    private GameObject player2;
    private int nb_joueur;
    private bool init = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        triggered = false;
        IsActivated = true;
    }


    void Update()
    {
        nb_joueur = Network.connections.Length + 1;
        if (!init && nb_joueur == 2)
        {
            player2 = GameObject.FindGameObjectWithTag("player2");
            init = true;
        }
        if (IsActivated & triggered)
        {
            txt.renderer.enabled = true;
        }
        else
        {
            txt.renderer.enabled = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            triggered = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player || (init && other.gameObject == player2))
        {
            if (Input.GetButton("Fire2"))
            {
                camera.SetActive(false);
                IsActivated = false;
            }
        }
    }
    void TriggerExit(Collider other)
    {
        if (other.gameObject == player || (init && other.gameObject == player2))
        {
            triggered = false;
        }
    }
}

