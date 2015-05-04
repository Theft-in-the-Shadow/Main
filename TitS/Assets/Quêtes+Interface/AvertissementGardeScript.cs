using UnityEngine;
using System.Collections;

public class AvertissementGardeScript : MonoBehaviour
{
    public Camera Main;
    public Camera Animation;
    private Camera Switch;

    private PlayerMovements playerMovement;
    private bool triggered;
    private bool trig2;
    private float TimerDisparition;
    private GameObject player;
    private float timer;
    void Start()
    {

        TimerDisparition = 0;
        trig2 = false;
        triggered = false;
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        playerMovement = player.GetComponent<PlayerMovements>();

    }

    void Update()
    {
        if (trig2)
        {
            TimerDisparition = TimerDisparition + Time.deltaTime;
            if (TimerDisparition > 5)
            {
                triggered = false;
            }
        }
    }
    void OnGUI()
    {
        if (triggered)
        {
            GUI.Box(new Rect(0, 450, 300, 100), "ATTENTION: \n On dirait des bruits de pas.. \n Un garde doit surement ce trouver\n non loin d'ici. Rester dans l'ombre et \n limiter le bruit");
        }
        if (TimerDisparition > 12)
        {
            GUI.Box(new Rect(Screen.width - 250 , 50, 250, 60), "MISSION SECONDAIRE: \n Eviter le garde et trouver un moyen \n de sortir de cette pièce");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            trig2 = true;
            triggered = true;

            Animation.depth = Main.depth + 1;
            playerMovement.enabled = false;
            Animation.animation.Play();

        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if (TimerDisparition > 12)
            {
                Animation.depth = Main.depth - 1;
                playerMovement.enabled = true;
            }
        }
    }

    void TriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            triggered = false;
        }
    }
}
