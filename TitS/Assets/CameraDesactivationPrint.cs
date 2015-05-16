using UnityEngine;
using System.Collections;

public class CameraDesactivationPrint : MonoBehaviour
{
    public GameObject Camera;
    public Camera AnimationCamera;
    public Camera MainCamera;

    private GameObject player;
    private bool triggered;
    private bool CameraIsActivated;
    private float timer;
    private float timerDesactivation;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(DoneTags.player);

        triggered = false;
        CameraIsActivated = true;
        timer = 0f;
        timerDesactivation = -1f;
    }


    void Update()
    {
        if (!CameraIsActivated || timer > 5)
        {
            timer = timer + Time.deltaTime;
            if (timer > 5)
            {
                timer = 4.5f;
                //Pour arrêter de compter + empêcher le GUI.
            }
        }
        if (timerDesactivation >= 0)
        {
            timerDesactivation = timerDesactivation + Time.deltaTime;
            AnimationCamera.depth = MainCamera.depth + 1;
            AnimationCamera.animation.Play();

            if (timerDesactivation > 1)
            {
                CameraIsActivated = false;
                Camera.SetActive(false);
                Camera.renderer.enabled = false;                
            }
        }
    }

    void OnGUI()
    {
        if (CameraIsActivated & triggered & timerDesactivation == -1f)
        {
            GUI.Box(new Rect(Screen.width / 2 - 25, Screen.height - 75, 250, 25), "Appuyer sur 'A' pour désactiver la caméra.");
        }
        if (!CameraIsActivated & timerDesactivation > 1.5)
        {
            GUI.Box(new Rect(Screen.width / 2 - 25, Screen.height - 75, 250, 50), "Caméra hors service, le champ est libre!");
            AnimationCamera.depth = MainCamera.depth - 1;
            timerDesactivation = -1f;
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
        if (other.gameObject == player)
        {
            if (Input.GetButton("Fire2") & CameraIsActivated)
            {
                timerDesactivation = 0f;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            triggered = false;
        }
    }
}
