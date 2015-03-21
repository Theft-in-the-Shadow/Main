using UnityEngine;
using System.Collections;

public class cameraDesactivationScript : MonoBehaviour {
    public GameObject camera;
    public TextMesh txt;

    private bool triggered;
    private bool IsActivated;
    private GameObject player;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        triggered = false;
        IsActivated = true;
	}
	

	void Update ()
    {
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
        if (other.gameObject==player)
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
        if (other.gameObject == player)
        {
            triggered = false;
        }
    }
}
