using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour 
{
    public bool opening = false;
    public bool closing = false;
    public Transform door;

    public float speed;
    public float maxOpenValue;
    private float currentValue = 0f;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
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
    void OnTriggerEnter(Collider obj)
    {
        if (obj.transform.name == "DefaultAvatar")
        {
            opening = true;
            closing = false;
        }
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.transform.name == "DefaultAvatar")
        {
            opening = false;
            closing = true;
        }
    }

    void OpenDoor()
    {
        float movement = speed*Time.deltaTime;
        currentValue += movement;

        if(currentValue <= maxOpenValue)
        {
            door.position = new Vector3(door.position.x + movement, door.position.y, door.position.z);
        }
        else
        {
            opening = false;
        }
    }

    void CloseDoor()
    {
        float movement = speed*Time.deltaTime;
        currentValue -= movement;

        if(currentValue >=0)
        {
            door.position = new Vector3(door.position.x - movement, door.position.y, door.position.z);
        }
        else
        {
            closing = false;
        }
    }
}
