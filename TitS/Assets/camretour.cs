using UnityEngine;
using System.Collections;

public class camretour : MonoBehaviour {
	//private Transform target = null;
	//private Vector3 targetLookAtOffset;
	private float rotationDamping = 10.0f;
	// Use this for initialization
	void Start () {
		//camera.transform.parent = target;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//if ((Input.GetAxis ("Horizontal")!=0) || (Input.GetAxis ("Vertical")!=0)) {
		if (Input.GetAxis ("turn")==0){
			//Vector3 lookPosition = target.TransformPoint(targetLookAtOffset);
			//Quaternion wantedRotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);
			//transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
			if (transform.rotation.y != GameObject.Find("DefaultAvatar").transform.rotation.y)
			{
				if (transform.rotation.y > GameObject.Find("DefaultAvatar").transform.rotation.y)
				{
					transform.RotateAround(GameObject.Find("DefaultAvatar").transform.position,Vector3.up,Time.deltaTime*-500f*(transform.rotation.y-GameObject.Find("DefaultAvatar").transform.rotation.y));
				}
				else
				{
					transform.RotateAround(GameObject.Find("DefaultAvatar").transform.position,Vector3.up,Time.deltaTime*500f*(GameObject.Find("DefaultAvatar").transform.rotation.y-transform.rotation.y));
				}
			}


		}

	}
}
