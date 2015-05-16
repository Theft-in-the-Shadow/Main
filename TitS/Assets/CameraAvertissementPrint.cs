using UnityEngine;
using System.Collections;

public class CameraAvertissementPrint : MonoBehaviour
{
    public GameObject light;


    private bool triggered;
    private bool IsActivated;
    private GameObject player;
    private float timer;

    void Start()
    {
        triggered = false;
        IsActivated = true;
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        timer = 0f;
    }

    void Update()
    {
      // IsActivated = light.renderer.enabled;

        if (triggered)
        {
            timer = timer + Time.deltaTime;
            if (timer > 4)
            {
                timer = 0f;
                triggered = false;
            }
        }
    }

    void OnGUI()
    {
        if (triggered & IsActivated)
        {
            GUI.Box(new Rect(Screen.width-250, 110 , 250, 100), "ATTENTION: \n Une caméra balaie cette zone, \n il faut trouver un moyen de \n couper l'alimentation de cette caméra.");
        }
    }

    void OnTriggerStay(Collider other)
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
}
