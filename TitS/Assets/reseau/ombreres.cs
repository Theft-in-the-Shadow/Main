using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ombreres : MonoBehaviour
{
    private NetworkView NetworkView;
    private GameObject player;
    private GameObject player2;
    private int nb_joueur;
    private bool init = false;
    private bool init2 = false;
    public bool ent = false;

    void Awake()
    {
        NetworkView = new NetworkView();
    }


    void Update()
    {
        nb_joueur = Network.connections.Length + 1;
        if(!init2 && nb_joueur == 1)
        {
            player = GameObject.FindGameObjectWithTag(DoneTags.player);
            init2 = true;
        }
        
        if(!init && nb_joueur == 2)
        {
            player2 = GameObject.FindGameObjectWithTag("player2");
            init = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            player.GetComponent<playerhealthres>().ombre = false; 
        else if (init && other.gameObject == player2)
            player2.GetComponent<playerhealthres>().ombre = false; 
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            player.GetComponent<playerhealthres>().ombre = true; 
        else if (init && other.gameObject == player2)
            player2.GetComponent<playerhealthres>().ombre = true;      
    }
}
