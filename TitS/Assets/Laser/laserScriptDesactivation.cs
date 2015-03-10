using UnityEngine;
using System.Collections;

public class laserScriptDesactivation : MonoBehaviour {
    public GameObject laser;
    public TextMesh txt;

    private GameObject player;
    private bool triggered;
    private bool IsActivated;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        triggered = false;
        IsActivated = true;
	}
	
	// Update is called once per frame
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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject==player)
        {
            if (Input.GetButton("Fire2"))
            {
                laser.SetActive(false);
                IsActivated = false;
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
