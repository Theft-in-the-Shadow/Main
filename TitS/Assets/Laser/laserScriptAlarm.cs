using UnityEngine;
using System.Collections;

public class laserScriptAlarm : MonoBehaviour
{
    private GameObject player;
    private DoneLastPlayerSighting lastPlayerSighting;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        lastPlayerSighting = GameObject.FindGameObjectWithTag(DoneTags.gameController).GetComponent<DoneLastPlayerSighting>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}
    void OnTriggerStay(Collider other)
    {
        if (renderer.enabled)
        {
            if (other.gameObject==player)
            {
                lastPlayerSighting.position = other.transform.position;
            }
        }
    }
}
