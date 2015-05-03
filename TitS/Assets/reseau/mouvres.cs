﻿using UnityEngine;
using System.Collections;

public class mouvres : MonoBehaviour
{

    public float vitesse = 1f;
    public AudioClip vroom;
    private float vit = 0.01f;
    private bool inter = false;
    private SphereCollider col;
    private GameObject player;
    private GameObject camera;
    private GameObject Maincam;
    private Camera cam;
    public GameObject roue1;
    public GameObject roue2;
    public GameObject roue3;
    public GameObject roue4;
    private int play = 0;
    public bool tableau = false;
    private GameObject player2;
    private int nb_joueur;
    private fine fine;
    public bool init = false;
    public bool j1 = false;
    public bool j2 = false;
    private NetworkView nt;


    // Use this for initialization
    void Awake()
    {
        nt = GetComponent<NetworkView>();
        fine = GameObject.FindGameObjectWithTag("can").GetComponent<fine>();
        Maincam = GameObject.FindGameObjectWithTag("MainCamera");
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("Camera");
        cam = camera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        nb_joueur = Network.connections.Length + 1;
        if (!init && nb_joueur == 2)
        {
            player2 = GameObject.FindGameObjectWithTag("player2");
            init = true;
        }
        if (Network.isServer)
            nt.RPC("update1", RPCMode.Others, j1);
        else
            nt.RPC("update2", RPCMode.Others, j2);
        if (tableau && j1 && j2)
        {
            cam.enabled = true;
            Maincam.SetActive(false);
            player.SetActive(false);
            player2.SetActive(false);
            camera.GetComponent<AudioListener>().enabled = true;
            inter = true;            
        }
        if (inter)
        {
            if (play == 0)
            {
                AudioSource.PlayClipAtPoint(vroom, transform.position);
                play++;
            }
            transform.Translate(0.0f, 0.0f, vit * 0.1f);
            roue1.transform.Rotate(vit * 1f, 0f, 0f);
            roue2.transform.Rotate(vit * 1f, 0f, 0f);
            roue3.transform.Rotate(vit * 1f, 0f, 0f);
            roue4.transform.Rotate(vit * 1f, 0f, 0f);
            fine.fini = true;
            if (vit < vitesse)
                vit = vit + 0.01f;
            if (vit + 0.01f >= vitesse)
            {
                Application.Quit();
            }

        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (Network.isServer && other.gameObject == player)
            j1 = true;
        if (Network.isClient && other.gameObject == player2)
            j2 = true;

    }
    void OnTriggerExit(Collider other)
    {
        if (Network.isServer && other.gameObject == player)
            j1 = false;
        if (Network.isClient && other.gameObject == player2)
            j2 = false;
    }

    [RPC]
    public void update1(bool j)
    {
        j1 = j;
    }
    [RPC]
    public void update2(bool j)
    {
        j2 = j;
    }



}