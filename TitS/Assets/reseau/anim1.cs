using UnityEngine;
using System.Collections;

public class anim1 : MonoBehaviour {

    private Animator anim;
    private NetworkView nt;
    private float vitesse;
	// Use this for initialization
	void Awake () 
    {
        anim = GetComponent<Animator>();
        nt = GetComponent<NetworkView>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Network.isServer)
        {
            vitesse = anim.GetFloat("Speed");
            nt.RPC("maj", RPCMode.Others, vitesse);
        }

	
	}

    [RPC]
    public void maj(float speed)
    {
        anim.SetFloat(Animator.StringToHash("Speed"), speed, 0.1f,Time.deltaTime);
    }
}
