using UnityEngine;
using System.Collections;

public class camres : MonoBehaviour
{

    public float angle = 90f;
    public float fieldOfViewAngle = 20f;
    public bool playerInSight = false;
    public Vector3 direction;

    private bool IsActivated;
    private GameObject light;
    private GameObject rotat;
    private int i;
    private bool alle;
    private SphereCollider col;
    private GameObject player;
    private GameObject player2;
    private int nb_joueur;
    private bool init = false;
    private DoneLastPlayerSighting lastPlayerSighting;
    private Vector3 position_initiale;
    private Vector3 camera_initiale;
    private bool was_found = false;


    void Awake()
    {
        i = 0;
        alle = true;
        light = GameObject.FindGameObjectWithTag("light");
        rotat = GameObject.Find("CamArmature");
        camera_initiale = rotat.transform.position;
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        lastPlayerSighting = GameObject.FindGameObjectWithTag(DoneTags.gameController).GetComponent<DoneLastPlayerSighting>();
        light.SetActive(true);

    }
    void Update()
    {
        nb_joueur = Network.connections.Length + 1;
        if (!init && nb_joueur == 2)
        {
            player2 = GameObject.FindGameObjectWithTag("player2");
            init = true; 
        }

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
        if (other.gameObject == player||(init && other.gameObject == player2))
        {
            direction = other.transform.position - light.transform.position;
            float angle_f = Vector3.Angle(direction, light.transform.forward);

            if (angle_f < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(light.transform.position, direction.normalized, out hit, 5))
                {
                    if (hit.collider.gameObject == player || init && hit.collider.gameObject == player2)
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
        if (other.gameObject == player || (init && other.gameObject == player2))
        {
            playerInSight = false;
        }
    }
}

