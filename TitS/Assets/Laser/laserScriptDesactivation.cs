using UnityEngine;
using System.Collections;

public class laserScriptDesactivation : MonoBehaviour
{
    public GameObject laser;
    public Camera MainCamera;
    public Camera AnimationCam;

    private PlayerMovements playerMovement;
    private GameObject player;

    private bool triggered;
    private bool LaserIsActivated;
    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        playerMovement = player.GetComponent<PlayerMovements>();

        triggered = false;
        LaserIsActivated = true;
        timer = 5;
    }

    void Update()
    {
        if (!LaserIsActivated)
        {
            timer = timer + Time.deltaTime;
            OnGUI();
        }
    }
    void OnGUI()
    {
        if (LaserIsActivated & triggered)
        {
            GUI.Box(new Rect(Screen.width/2 -25, Screen.height - 75, 250, 25), "Appuyer sur 'A' pour désactiver le laser.");
        }
        if (!LaserIsActivated & timer < 4)
        {
            GUI.Box(new Rect(Screen.width - 250, 50, 250, 50), "MISSION SECONDAIRE: \n ACCOMPLIE !");
        }
        else
        {
            AnimationCam.depth = MainCamera.depth - 1;
            playerMovement.enabled = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if (Input.GetButton("Fire2"))
            {                
                LaserIsActivated = false;
                playerMovement.enabled = false;
                timer = 0;                
            }
            if (!LaserIsActivated & timer > 1)
            {
                AnimationCam.depth = MainCamera.depth + 1;
                AnimationCam.animation.Play();
            }
            if (!LaserIsActivated &  timer > 3)
            {
                laser.SetActive(false);
                laser.renderer.enabled = false;
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

}
