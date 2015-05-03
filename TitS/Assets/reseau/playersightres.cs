using UnityEngine;
using System.Collections;

public class playersightres : MonoBehaviour {

	public float fieldOfViewAngle = 110f;				// Number of degrees, centred on forward, for the enemy see.
    public bool playerInSight;							// Whether or not the player is currently sighted.
	public Vector3 personalLastSighting;				// Last place this enemy spotted the player.
	
	
	private NavMeshAgent nav;							// Reference to the NavMeshAgent component.
	private SphereCollider col;							// Reference to the sphere collider trigger component.
	private Animator anim;								// Reference to the Animator.
	private DoneLastPlayerSighting lastPlayerSighting;	// Reference to last global sighting of the player.
    private GameObject player;							// Reference to the player.
    private GameObject player2;                         // reference du joueur 2
    private bool player2init = false;                   // reference de l'int du joueur 2
    private GameObject[] map;
    private LevelManager level;      
    public int nb_joueur = 0;
    private playerhealthres player2Health;
    private Animator playerAnim;						// Reference to the player's animator component.
	private playerhealthres playerHealth;				// Reference to the player's health script.
	private DoneHashIDs hash;							// Reference to the HashIDs.
	private Vector3 previousSighting;					// Where the player was sighted last frame.
    public bool player1 = true;
	
	void Awake ()
	{
		// Setting up the references.
		nav = GetComponent<NavMeshAgent>();
		col = GetComponent<SphereCollider>();
		anim = GetComponent<Animator>();
		lastPlayerSighting = GameObject.FindGameObjectWithTag(DoneTags.gameController).GetComponent<DoneLastPlayerSighting>();
		player = GameObject.FindGameObjectWithTag(DoneTags.player);
		playerAnim = player.GetComponent<Animator>();
		playerHealth = player.GetComponent<playerhealthres>();
		hash = GameObject.FindGameObjectWithTag(DoneTags.gameController).GetComponent<DoneHashIDs>();
		
		// Set the personal sighting and the previous sighting to the reset position.
		personalLastSighting = lastPlayerSighting.resetPosition;
		previousSighting = lastPlayerSighting.resetPosition;
        map = GameObject.FindGameObjectsWithTag("Map");
        level = map[0].GetComponent<LevelManager>();
	}
	
	
	void Update ()
	{
        nb_joueur = Network.connections.Length + 1;
        if(!player2init & nb_joueur == 2)
        {
            player2 = GameObject.FindGameObjectWithTag("player2");
            player2Health = player2.GetComponent<playerhealthres>();
            player2init = true;
        }
		// If the last global sighting of the player has changed...
		if(lastPlayerSighting.position != previousSighting)
			// ... then update the personal sighting to be the same as the global sighting.
			personalLastSighting = lastPlayerSighting.position;
		
		// Set the previous sighting to the be the sighting from this frame.
		previousSighting = lastPlayerSighting.position;
		
		// If the player is alive...
		if(playerHealth.health > 0f && player1)
			// ... set the animator parameter to whether the player is in sight or not.
			anim.SetBool(hash.playerInSightBool, playerInSight);
        else if (nb_joueur == 2 && player2Health.health > 0f && !player1)
            // ... set the animator parameter to whether the player is in sight or not.
            anim.SetBool(hash.playerInSightBool, playerInSight);

        else
        {
            playerInSight = false;
            // ... set the animator parameter to false.
            anim.SetBool(hash.playerInSightBool, false);
        }
	}
	

	void OnTriggerStay (Collider other)
    {
		// If the player has entered the trigger sphere...
        if(other.gameObject == player || other.gameObject == player2)
        {
			// By default the player is not in sight.
			playerInSight = false;
			
			// Create a vector from the enemy to the player and store the angle between it and forward.
            Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			
			// If the angle between forward and where the player is, is less than half the angle of view...
            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;

                // ... and if a raycast towards the player hits something...
                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                {
                    // ... and if the raycast hits the player...
                    if (hit.collider.gameObject == player)
                    {
                        if (!playerHealth.ombre || lastPlayerSighting.position != lastPlayerSighting.resetPosition)
                        {
                            // ... the player is in sight.
                            playerInSight = true;
                            player1 = true;

                            // Set the last global sighting is the players current position.
                            lastPlayerSighting.position = player.transform.position;
                        }

                    }
                    else if (hit.collider.gameObject == player2)
                    {
                        if (!player2Health.ombre || lastPlayerSighting.position != lastPlayerSighting.resetPosition)
                        {
                            // ... the player is in sight.
                            playerInSight = true;
                            player1 = false;
                            // Set the last global sighting is the players current position.
                            lastPlayerSighting.position = player2.transform.position;
                        }
                    }
                }
            }
			
			// Store the name hashes of the current states.
			int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo(0).nameHash;
			// If the player is running or is attracting attention...
			if(Input.GetButton("courir"))
			{
				// ... and if the player is within hearing range...
				if(CalculatePathLength(player.transform.position) <= col.radius)
					// ... set the last personal sighting of the player to the player's current position.
					personalLastSighting = player.transform.position;
			}
        }
    }
	
	
	void OnTriggerExit (Collider other)
	{
		// If the player leaves the trigger zone...
		if(other.gameObject == player)
			// ... the player is not in sight.
			playerInSight = false;
        if(other.gameObject == player2)
            playerInSight = false;
	}
	
	
	float CalculatePathLength (Vector3 targetPosition)
	{
		// Create a path and set it based on a target position.
		NavMeshPath path = new NavMeshPath();
		if(nav.enabled)
			nav.CalculatePath(targetPosition, path);
		
		// Create an array of points which is the length of the number of corners in the path + 2.
		Vector3 [] allWayPoints = new Vector3[path.corners.Length + 2];
		
		// The first point is the enemy's position.
		allWayPoints[0] = transform.position;
		
		// The last point is the target position.
		allWayPoints[allWayPoints.Length - 1] = targetPosition;
		
		// The points inbetween are the corners of the path.
		for(int i = 0; i < path.corners.Length; i++)
		{
			allWayPoints[i + 1] = path.corners[i];
		}
		
		// Create a float to store the path length that is by default 0.
		float pathLength = 0;
		
		// Increment the path length by an amount equal to the distance between each waypoint and the next.
		for(int i = 0; i < allWayPoints.Length - 1; i++)
		{
			pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
		}
		
		return pathLength;
	}
}
