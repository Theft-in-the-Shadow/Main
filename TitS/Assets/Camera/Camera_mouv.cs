using UnityEngine;
using System.Collections;

public class Camera_mouv : MonoBehaviour {

	public float angle = 90f;
	public float fieldOfViewAngle = 20f;
    public bool playerInSight = false;
    public Vector3 direction;

    private bool IsActivated;
	private GameObject light ;
	private GameObject rotat;
    private int i;
    private bool alle;
    private SphereCollider col;
    private GameObject player;
    private DoneLastPlayerSighting lastPlayerSighting;
    private Vector3 position_initiale;
    private Vector3 camera_initiale;
    private bool was_found = false;


	void Awake () 
	{
        i = 0;
        alle = true;
		light = GameObject.Find ("Directional light");
		rotat = GameObject.Find ("CamArmature");
        camera_initiale = rotat.transform.position;
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        lastPlayerSighting = GameObject.FindGameObjectWithTag(DoneTags.gameController).GetComponent<DoneLastPlayerSighting>();
        light.SetActive(true);
	
	}
	void Update ()
    {
        IsActivated = light.active;

        if (IsActivated)
        {
            if (playerInSight)
            {
                light.GetComponent<Light>().color = Color.blue;
            }
            else
            {
                light.GetComponent<Light>().color = Color.red;
                {
                    if (i <= angle & alle)
                    {
                        rotat.transform.Rotate(0, 0, -1);
                        i++;
                    }
                    else
                    {
                        alle = false;
                        rotat.transform.Rotate(0, 0, 1);
                        i--;
                        if (i <= 0)
                        {
                            alle = true;
                        }
                    }
                }
            } 
        }
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            direction = other.transform.position - light.transform.position;
            float angle_f = Vector3.Angle(direction, light.transform.forward);

            if (angle_f < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(light.transform.position, direction.normalized, out hit, 5))
                {
                    if (hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                        lastPlayerSighting.position = other.transform.position;
                    }
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;
        }
    }
}
