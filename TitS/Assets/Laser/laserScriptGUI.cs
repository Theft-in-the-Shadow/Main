using UnityEngine;
using System.Collections;

public class laserScriptGUI : MonoBehaviour
{
    public GameObject laser;

    private GameObject player;
    private bool triggered;
    private bool HasEntered;
    private float timer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        triggered = false;
        HasEntered = false;
        timer = 0;
    }

    void Update()
    {
        if (HasEntered)
        {
            timer = timer + Time.deltaTime;
        }
    }
    void OnGUI()
    {
        
        if (triggered)
        {
            if (timer > 3)
            {
                if (laser.renderer.enabled) GUI.Box(new Rect(1500, 50, 250, 50), "MISSION SECONDAIRE: \n Désactiver le laser.");
            }
            else if (laser.renderer.enabled)
            {
                GUI.Box(new Rect(1500, 50, 300, 100), "Il semblerait que ce laser ai besoin \n d'être désactiver pour pouvoir passer.. \n Il y a surement un moyen de le désactiver.");
            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            triggered = true;
            HasEntered = true;
            OnGUI();
        }
    }
    void TriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            triggered = false;
            OnGUI();
        }
    }
}
