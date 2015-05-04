using UnityEngine;
using System.Collections;

public class CameraDesactivationPrint : MonoBehaviour {
    public GameObject Camera;

    private GameObject player;
    private bool triggered;
    private bool CameraIsActivated;
    private float timer;
    
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        triggered = false;
        CameraIsActivated = true;
        timer = 0f;
	}
	

	void Update ()
    {
        if (!CameraIsActivated || timer>5)
        {
            timer = timer + Time.deltaTime;
            if (timer>5)
            {
                timer = 4.5f;
                //Pour arrêter de compter + empêcher le GUI.
            }
        }
	}

    void OnGUI()
    {
        if (CameraIsActivated & triggered)
        {
            GUI.Box(new Rect(Screen.width / 2 - 125, Screen.height - 75, 250, 25), "Appuyer sur 'A' pour désactiver la caméra.");
        }
        if (!CameraIsActivated & timer < 4f)
        {
            GUI.Box(new Rect(Screen.width / 2 - 125, Screen.height - 75, 250, 50), "Caméra hors service, le champ est libre!");
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
            if (Input.GetButton("Fire2"))
            {
                CameraIsActivated = false;
                Camera.SetActive(false);
                Camera.renderer.enabled = false;
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
