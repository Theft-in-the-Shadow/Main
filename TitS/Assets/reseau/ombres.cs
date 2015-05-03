using UnityEngine;
using System.Collections;

public class ombres : MonoBehaviour
{

    private NetworkView nt;
    private playerhealthres health;
    void Awake()
    {
        nt = GetComponent<NetworkView>();
        health = GetComponent<playerhealthres>();
    }

    // Update is called once per frame
    void Update() 
    {
	    if(Network.isServer)
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
