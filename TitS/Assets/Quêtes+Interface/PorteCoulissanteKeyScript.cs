using UnityEngine;
using System.Collections;

public class PorteCoulissanteKeyScript : MonoBehaviour
{
    public bool opening = false;
    public bool closing = false;
    public Transform door;
    public GameObject doorCollider;
    public GameObject key;

    public float speed;
    public float maxOpenValue;
    private float currentValue;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(DoneTags.player);
        currentValue = 0f;
    }


    void Update()
    {
        if (opening)
        {
            OpenDoor();
        }
        if (closing)
        {
            CloseDoor();
        }
    }
    void OnGUI()
    {
        if (opening & key.collider.enabled)
        {
            GUI.Box(new Rect(Screen.width / 2 - 125, Screen.height - 75, 250, 25), "Vous n'avez le pass requis.");
        }
    }
    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject == player)
        {
            opening = true;
            closing = false;
            if (!key.collider.enabled)
            {
                doorCollider.collider.enabled = false;
            }
        }
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.gameObject == player)
        {
            opening = false;
            closing = true;
            doorCollider.collider.enabled = true;
        }
    }

    void OpenDoor()
    {
        if (!key.collider.enabled)
        {
            float movement = speed * Time.deltaTime;
            currentValue += movement;

            if (currentValue <= maxOpenValue)
            {
                door.position = new Vector3(door.position.x + movement, door.position.y, door.position.z);
            }
            else
            {
                opening = false;
            }
        }
    }

    void CloseDoor()
    {
        float movement = speed * Time.deltaTime;
        currentValue -= movement;

        if (currentValue >= 0)
        {
            door.position = new Vector3(door.position.x - movement, door.position.y, door.position.z);
        }
        else
        {
            closing = false;
        }
    }
}
