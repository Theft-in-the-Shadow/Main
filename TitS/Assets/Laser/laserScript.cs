using UnityEngine;
using System.Collections;

public class laserScript : MonoBehaviour {
	public float On;
	public float Off;

    private float timer;
	// Use this for initialization
    void Start ()
    {
        timer = Time.time;
    }
    void Update()
    {

        timer += Time.deltaTime;
        if (renderer.enabled & On <= timer)
        {
            Switch();
        }
        if (!renderer.enabled & Off <= timer)
        {
            Switch();
        }
    }
	
	// Update is called once per frame
	void Switch()
    {
        timer = 0f;
        renderer.enabled = !renderer.enabled;
        light.enabled = !light.enabled;
	}
}
