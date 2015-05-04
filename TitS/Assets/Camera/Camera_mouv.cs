using UnityEngine;
using System.Collections;

public class Camera_mouv : MonoBehaviour {

	public float angle = 90f;
	public float fieldOfViewAngle = 20f;
    public float Speed = 1f;
    public bool playerInSight = false;
    public Vector3 direction;
    public GameObject light;
    public GameObject rotat;

    private bool IsActivated;
    private float i;
    private bool alle;
    private SphereCollider col;
    private GameObject player;
    private DoneLastPlayerSighting lastPlayerSighting;
    private Vector3 position_initiale;
    private Vector3 camera_initiale;
    private bool was_found = false;


	void Start () 
	{
        i = 0;
        alle = true;
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        lastPlayerSighting = GameObject.FindGameObjectWithTag(DoneTags.gameController).GetComponent<DoneLastPlayerSighting>();

       // rotat.transform.rotation = ....

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
                        rotat.transform.Rotate(0, 0, -Speed);
                        i=i+Speed;
                    }
                    else
                    {
                        alle = false;
                        rotat.transform.Rotate(0, 0, Speed);
                        i = i - Speed;
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
        if (other.gameObject == player & IsActivated)
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
