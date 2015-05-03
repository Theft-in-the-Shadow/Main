using UnityEngine;
using System.Collections;

public class mouv : MonoBehaviour
{

    public float vitesse = 1f;
    public AudioClip vroom;
    private float vit = 0.01f;
    private bool inter = false;
    private SphereCollider col;
    private GameObject player;
    private GameObject camera;
    private Camera cam;
    public GameObject roue1;
    public GameObject roue2;
    public GameObject roue3;
    public GameObject roue4;
    private int play = 0;
    public bool tableau = false;
    public GameObject fin;


    // Use this for initialization
    void Awake()
    {
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("Camera");
        cam = camera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
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
            fin.SetActive(true);
            if (vit < vitesse)
                vit = vit + 0.01f;
            if (vit + 0.01f >= vitesse)
            {
                Application.LoadLevel("menu");
            }

        }

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if (tableau)
            {
                cam.enabled = true;
                player.SetActive(false);
                camera.GetComponent<AudioListener>().enabled = true;
                inter = true;
            }
        }

    }

}
