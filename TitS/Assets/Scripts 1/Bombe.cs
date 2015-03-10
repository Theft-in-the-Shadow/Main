﻿using UnityEngine;
using System.Collections;

public class Bombe : MonoBehaviour {

    private BoxCollider cub;

    public bool bombePose = false;
    public bool inside = false;
    private Animator anim;                              // Reference to the animator component.
    private PlayerMovements playerMovement;            // Reference to the player movement script.
    private HashIDs hash;
    private GameObject player;
    public GameObject bomb;
    public GameObject bomb2;
    public GameObject bomb3;
    public GameObject wall;
    public GameObject bruit;
    public float time = 0f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        playerMovement = player.GetComponent<PlayerMovements>();
        hash = GameObject.Find("gameController").GetComponent<HashIDs>();
    }

    void Update()
    {
        if (inside)
        {
            if (Input.GetButton("Bombe"))
            {
                anim.SetBool(hash.bombeBool, true);
                bomb.SetActive(true);
                time = 0;

                
            }
            else
            {
                if (anim.GetCurrentAnimatorStateInfo(0).nameHash == hash.bombeState)
                {
                    anim.SetBool(hash.bombeBool, false);
                    // Disable the movement.                    
                    anim.SetFloat(hash.speedFloat, 0f);
                    playerMovement.enabled = false;
                }
                else
                {
                    playerMovement.enabled = true;
                    if(time>5f)
                    {
                        bomb.SetActive(false);
                        bomb2.SetActive(true);
                        if (time>7f)
                        {
                            Destroy(wall, 1);
                            bomb3.SetActive(true);
                        }
                    }
                        
                    
                }

                time = time + 1 * Time.deltaTime;
            }
            


        }        
    }

    void OnTriggerStay(Collider other)
    {
        inside = true;
    }
    void OnTriggerExit (Collider other)
    {
        inside = false;
    }

}