using UnityEngine;
using System.Collections;

public class TEMPS : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Horizontal") != 0) {
						Time.timeScale = 0;	
				}

		}

}
