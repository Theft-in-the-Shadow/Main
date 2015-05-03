using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class coulres : MonoBehaviour {

    private Image cercle;
    private GameObject player;
    private GameObject player2;
    private playerhealthres playerhealth;
    private playerhealthres playerhealth2;
    private int nb_joueur;
    private bool init = false;
    private bool init2 = false;

	void Awake () 
    {
        cercle = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        nb_joueur = Network.connections.Length + 1;
        if (!init2 && nb_joueur == 1)
        {
            player = GameObject.FindGameObjectWithTag(DoneTags.player);
            playerhealth = player.GetComponent<playerhealthres>();
            init2 = true;
        }
        
        if(!init && nb_joueur == 2)
        {
            player2 = GameObject.FindGameObjectWithTag("player2");
            playerhealth2 = player2.GetComponent<playerhealthres>();
            init = true;
        }
        if (Network.isServer)
            if (playerhealth.ombre == true)
                cercle.color = Color.green;
            else
                cercle.color = Color.red;
        if(init && Network.isClient)
            if(playerhealth2.ombre == true)
                cercle.color = Color.green;
            else
                cercle.color = Color.red;
	}
}
