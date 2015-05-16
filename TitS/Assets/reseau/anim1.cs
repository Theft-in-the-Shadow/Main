using UnityEngine;
using System.Collections;

public class anim1 : MonoBehaviour {

    private Animator anim;
    private NetworkView nt;
    private bool mort;
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
            mort = anim.GetBool("Dead");
            nt.RPC("maj", RPCMode.Others, vitesse, mort);
        }

	
	}

    [RPC]
    public void maj(float speed, bool dd)
    {
        if (dd)
            anim.SetFloat(Animator.StringToHash("Speed"), 0, 0.1f, Time.deltaTime);
        else
            anim.SetFloat(Animator.StringToHash("Speed"), speed, 0.1f,Time.deltaTime);
        anim.SetBool(Animator.StringToHash("Dead"), dd);

    }
}
