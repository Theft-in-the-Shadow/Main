using UnityEngine;
using System.Collections;

public class playerhealthres: MonoBehaviour
{
    public float health = 100f;							// How much health the player has left.
    public float resetAfterDeathTime = 5f;				// How much time from the player dying to the level reseting.
    public AudioClip deathClip;							// The sound effect of the player dying.


    private Animator anim;								// Reference to the animator component.
    private playermouvres playerMovement;			// Reference to the player movement script.
    private DoneHashIDs hash;							// Reference to the HashIDs.
    private DoneSceneFadeInOut sceneFadeInOut;			// Reference to the SceneFadeInOut script.
    private DoneLastPlayerSighting lastPlayerSighting;	// Reference to the LastPlayerSighting script.
    private float timer;								// A timer for counting to the reset of the level once the player is dead.
    private bool playerDead;							// A bool to show if the player is dead or not.
    public SpawnPoint[] spawnPoints;
    private LevelManager level;
    private GameObject map;
    private playersightres playersight;
    public bool ombre = false;
    private int index;
    private NetworkView _ntView;

    void Awake()
    {
        // Setting up the references.

        map = GameObject.FindGameObjectWithTag("Map");
        level = map.GetComponent<LevelManager>();
        spawnPoints = level.spawnPoints;
        index = level.index;
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<playermouvres>();
        hash = GameObject.FindGameObjectWithTag(DoneTags.gameController).GetComponent<DoneHashIDs>();
        sceneFadeInOut = GameObject.FindGameObjectWithTag(DoneTags.fader).GetComponent<DoneSceneFadeInOut>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag(DoneTags.gameController).GetComponent<DoneLastPlayerSighting>();
        resetAfterDeathTime = 1f;
        _ntView = GetComponent<NetworkView>();
    }


    void Update()
    {
        // If health is less than or equal to 0...
        if (health <= 0f)
        {
            // ... and if the player is not yet dead...
            if (!playerDead)
                // ... call the PlayerDying function.
                PlayerDying();
            else
            {
                // Otherwise, if the player is dead, call the PlayerDead and LevelReset functions.
                PlayerDead();
                // detruire le perso et le reinicier
                LevelReset();
            }
        }
    }


    void PlayerDying()
    {
        // The player is now dead.
        playerDead = true;

        // Set the animator's dead parameter to true also.
        anim.SetBool(hash.deadBool, playerDead);

        // Play the dying sound effect at the player's location.
        AudioSource.PlayClipAtPoint(deathClip, transform.position);
    }


    void PlayerDead()
    {
        // If the player is in the dying state then reset the dead parameter.
        if (anim.GetCurrentAnimatorStateInfo(0).nameHash == hash.dyingState)
            anim.SetBool(hash.deadBool, false);

        // Disable the movement.
        anim.SetFloat(hash.speedFloat, 0f);
        playerMovement.enabled = false;

        // Reset the player sighting to turn off the alarms.
        lastPlayerSighting.position = lastPlayerSighting.resetPosition;

        // Stop the footsteps playing.
        audio.Stop();
    }


    void LevelReset()
    {
        // Increment the timer.
        if (_ntView.isMine)
        {
            timer += Time.deltaTime;
            if (timer >= resetAfterDeathTime)
            {

                sceneFadeInOut.mort();

                //If the timer is greater than or equal to the time before the level resets...
                if (sceneFadeInOut.morte)
                {
                    // ... reset the level.            
                    sceneFadeInOut.reStartScene();
                    transform.position = spawnPoints[index].transform.position;
                    playerMovement.enabled = true;
                    playerDead = false;
                    health = 100f;
                    GameObject.FindGameObjectWithTag("Enemy").GetComponent<playersightres>().playerInSight = false;
                    timer = 0;
                    anim.SetBool(hash.deadBool, false);
                    sceneFadeInOut.morte = false;


                }
            }
        }
    }


    public void TakeDamage(float amount)
    {
        // Decrement the player's health by amount.
        health -= amount;
    }
}
