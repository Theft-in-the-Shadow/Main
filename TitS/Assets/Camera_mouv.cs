using UnityEngine;
using System.Collections;

public class Camera_mouv : MonoBehaviour {

	public float angle = 90f;
	public float fieldOfViewAngle = 20f;
    public bool playerInSight = false;
    public Vector3 direction;

	private GameObject light ;
	private GameObject rotat;
    private int i;
    private bool alle;
    private SphereCollider col;
    private GameObject player;
    private DoneLastPlayerSighting lastPlayerSighting;


	void Awake () 
	{
        i = 0;
        alle = true;
		light = GameObject.Find ("Directional light");
		rotat = GameObject.Find ("CamArmature");
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        lastPlayerSighting = GameObject.FindGameObjectWithTag(DoneTags.gameController).GetComponent<DoneLastPlayerSighting>();

	
	}
	
	// Update is called once per frame
	void Update () {
        if (i <= angle & alle == true)
        {
            rotat.transform.Rotate(0, 0, -1);
            light.transform.Rotate(1, 0, 0);
            i++;
        }
        else
        {
            alle = false;
            rotat.transform.Rotate(0, 0, 1);
            light.transform.Rotate(-1, 0, 0);
            i--;
            if(i <= 0)
            {
                alle = true;
            }
        }
        if(playerInSight == true)
        {
            light.GetComponent<Light>().color = Color.blue;

        }
        else
        {
            light.GetComponent<Light>().color = Color.red;
        }

            
		
	
	}

    void OnTriggerStay(Collider other)
    {
         //If the player has entered the trigger sphere...
        if (other.gameObject == player)
        {

            // Create a vector from the enemy to the player and store the angle between it and forward.
            direction = other.transform.position - light.transform.position;
            float angle = Vector3.Angle(direction, light.transform.forward);

            // If the angle between forward and where the player is, is less than half the angle of view...
            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;

                // ... and if a raycast towards the player hits something...
                if (Physics.Raycast(light.transform.position, direction.normalized, out hit, 5))
                {
                    // ... and if the raycast hits the player...
                    if (hit.collider.gameObject == player)
                    {
                        // ... the player is in sight.
                        playerInSight = true;
                        lastPlayerSighting.position = other.transform.position;
                    }
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        // If the player leaves the trigger zone...
        if (other.gameObject == player)
            // ... the player is not in sight.
            playerInSight = false;
    }
}
