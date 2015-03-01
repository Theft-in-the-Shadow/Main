using UnityEngine;
using System.Collections;

public class Camera_mouv : MonoBehaviour {

	public float angle = 90f;
	public float fieldOfViewAngle = 30f;

	private GameObject light ;
	private GameObject rotat;

	void Awake () 
	{
		light = GameObject.Find ("Directional light");
		rotat = GameObject.Find ("CamArmature");

	
	}
	
	// Update is called once per frame
	void Update () {
		rotat.transform.Rotate (0, 0, -1);
		light.transform.Rotate (0, -1, 0);
	
	}
}
