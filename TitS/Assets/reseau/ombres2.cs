using UnityEngine;
using System.Collections;

public class ombres2 : MonoBehaviour {

    private NetworkView nt;
    private playerhealthres health;
	void Awake () {
        nt = GetComponent<NetworkView>();
        health = GetComponent<playerhealthres>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Network.isClient)
        {
            nt.RPC("ch", RPCMode.Others, health.ombre);
        }
	}

    [RPC]
    public void ch(bool om)
    {
        health.ombre = om;
    }
}
