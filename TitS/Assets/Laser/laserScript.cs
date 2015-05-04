using UnityEngine;
using System.Collections;

public class laserScript : MonoBehaviour {
	public float On;
	public float Off;
    public Light Laser;

    private float timer;

    void Start ()
    {
        timer = Time.time;
        Laser.enabled = true;
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
        renderer.enabled = !renderer.enabled;;
       //light.enabled = !light.enabled;
	}
}
