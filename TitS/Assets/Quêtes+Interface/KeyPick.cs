using UnityEngine;
using System.Collections;

public class KeyPick : MonoBehaviour
{
    public GameObject door;

    private GameObject player;
    private GameObject card;
    private bool triggered;
    private float timer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        card = GameObject.Find("prop_keycard_card_1");
        timer = 0f;
        triggered = false;

    }
    void Update()
    {
        if (triggered)
        {
            timer = timer + Time.deltaTime;
            if (timer > 3)
            {
                timer = 0f;
                triggered = false;
            }
        }

    }
    void OnGUI()
    {
        if (triggered)
        {
            GUI.Box(new Rect(Screen.width / 2 - 125, Screen.height - 75, 500, 25), "Vous venez de ramasser une carte magnétique, elle ouvre surement quelque chose.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            triggered = true;
            gameObject.collider.enabled = false;
            gameObject.light.enabled = false;
            card.renderer.enabled = false;
        }
    }
}
