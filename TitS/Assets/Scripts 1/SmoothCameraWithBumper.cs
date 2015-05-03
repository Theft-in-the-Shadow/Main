using UnityEngine;
using System.Collections;

public class SmoothCameraWithBumper : MonoBehaviour 
{
	public Transform target = null;
	public float distance = 1.40f;
	public float height = 1.78f;
	private float damping = 5.0f;
	private bool smoothRotation = true;
	private float rotationDamping = 10.0f;
	private bool touche = false;
	
	private Vector3 targetLookAtOffset = new Vector3(0f,1.5f,0f); // allows offsetting of camera lookAt, very useful for low bumper heights
	
	private float bumperDistanceCheck = 2.5f; // length of bumper ray
	private float bumperCameraHeight = 1.0f; // adjust camera height while bumping
	private Vector3 bumperRayOffset; // allows offset of the bumper ray from target origin
	SphereCollider camcollider = new SphereCollider();
	/// <Summary>
	/// If the target moves, the camera should child the target to allow for smoother movement. DR
	/// </Summary>
	///

	private void Awake()
	{
		camera.transform.parent = target;
	}
	
	private void FixedUpdate() 
	{
		if (distance != 1.40f && !touche) {
			distance += 0.02f*(1.40f-distance);
				}
		if (height != 1.78f&&!touche)
			height -= 0.08f*(height-1.78f);


		Vector3 wantedPosition = target.TransformPoint(0, height, -distance);
		
		// check to see if there is anything behind the target
		//RaycastHit hit;
		Vector3 back = target.transform.TransformDirection(-1 * Vector3.forward); 
		
		// cast the bumper ray out from rear and check to see if there is anything behind
		//if (Physics.Raycast(target.TransformPoint(bumperRayOffset), back, out hit, bumperDistanceCheck)
		 //   && hit.transform != target) // ignore ray-casts that hit the user. DR
		//{
			// clamp wanted position to hit position
		//	wantedPosition.x = hit.point.x;
		//	wantedPosition.z = hit.point.z;
		//	wantedPosition.y = Mathf.Lerp(hit.point.y + bumperCameraHeight, wantedPosition.y, Time.deltaTime * damping);
		//} 
		
		transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);
		
		Vector3 lookPosition = target.TransformPoint(targetLookAtOffset);
		
		if (smoothRotation & ((Input.GetAxis ("Horizontal")!=0) || (Input.GetAxis ("Vertical")!=0)))
		{
			Quaternion wantedRotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);
			transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
		} 
		else 
			transform.rotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);
	}
	void OnTriggerEnter()  {
		touche = true;
		if (distance > 0.5f)
				if (height < 2.19f)
						height += 0.4f;
		distance -= 0.4f*(distance-0.5f);
	}
	void OnTriggerExit() {
		touche = false;
	}

}
