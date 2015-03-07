using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Mouse X")!=0) 
		{
			transform.RotateAround (GameObject.Find("GameObject").transform.position,Vector3.up,Input.GetAxis ("Mouse X")*1000*Time.deltaTime);
		}

	}
}
