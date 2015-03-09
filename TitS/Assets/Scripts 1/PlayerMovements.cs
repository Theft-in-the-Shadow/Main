using UnityEngine;
using System.Collections;

public class PlayerMovements : MonoBehaviour
{
	public float turnSmoothing = 200f;   // A smoothing value for turning the player.
	public float speedDampTime = 0.1f;  // The damping for the speed parameter
	
	float rot = 0f;
	private Animator anim;              // Reference to the animator component.
	private HashIDs hash;  
	public float vitesse;
//	private Vector3 izi = new Vector3();// Reference to the HashIDs.
	
	
	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent<Animator>();
		hash = GameObject.Find("gameController").GetComponent<HashIDs>();
		
		// Set the weight of the shouting layer to 1.
		//anim.SetLayerWeight(1, 1f);
	}
	
	
	void FixedUpdate ()
	{
		// Cache the inputs.
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		bool sneak = Input.GetButton("Sneak");
		if (Input.GetAxis ("courir") != 0) 
		{
			vitesse = 5.5f;
		} 
		else 
		{
			vitesse = 1f;
		}
		
		MovementManagement(h, v, sneak);
	}
	
	

	
	
	void MovementManagement (float horizontal, float vertical, bool sneaking)
	{
		// Set the sneaking parameter to the sneak input.
		anim.SetBool(hash.sneakingBool, sneaking);
		
		// If there is some axis input...
		if(horizontal != 0f|| vertical != 0)
		{
			// ... set the players rotation and set the speed parameter to 5.5f.
			//Rotating(horizontal, vertical);
			if (vertical > 0f & horizontal ==0)
			{
				rot = rot + (3f*horizontal);
				rigidbody.MoveRotation(Quaternion.Euler(0,rot,0));
				anim.SetFloat(hash.speedFloat, vitesse, speedDampTime, Time.deltaTime);
			}
			if (vertical > 0f & horizontal !=0)
			{
			rot = rot + (3f*horizontal);
			rigidbody.MoveRotation(Quaternion.Euler(0,rot,0));
				anim.SetFloat(hash.speedFloat, vitesse, speedDampTime, Time.deltaTime);
			}
			if (vertical == 0f)
			{
			rot = rot + (7f * horizontal);
			rigidbody.MoveRotation(Quaternion.Euler(0,rot,0));
				anim.SetFloat(hash.speedFloat, vitesse, speedDampTime, Time.deltaTime);
			}
			if (vertical < 0f & horizontal ==0f)
			{
				Rotating(0,vertical);
				//rot = rot + (5f*horizontal);
				//rigidbody.MoveRotation(Quaternion.Euler(0,rot,0));
				anim.SetFloat(hash.speedFloat, vitesse, speedDampTime, Time.deltaTime);
			}
			if (vertical < 0f & horizontal !=0f)
			{

				rot =rot + (3f*horizontal);
				rigidbody.MoveRotation(Quaternion.Euler(0,180+rot,0));
				anim.SetFloat(hash.speedFloat, vitesse, speedDampTime, Time.deltaTime);
			}



		}

		else
			// Otherwise set the speed parameter to 0.
			anim.SetFloat(hash.speedFloat, 0);
	}
	
	
	void Rotating (float horizontal, float vertical)
	{
		// Create a new vector of the horizontal and vertical inputs.
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);

		
		// Create a rotation based on this new vector assuming that up is the global y axis.
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		
		// Create a rotation that is an increment closer to the target rotation from the player's rotation.
		Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
		
		// Change the players rotation to this new rotation.
		rigidbody.MoveRotation(newRotation);
	}
	

}