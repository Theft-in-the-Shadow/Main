using UnityEngine;
using System.Collections;

public class gcres : MonoBehaviour {

    private NetworkView nt;
    private DoneLastPlayerSighting lastsighting;
    public Vector3 position;
    public Vector3 resetPositio = new Vector3(1000f, 1000f, 1000f);
	void Awake () 
    {
        nt = GetComponent<NetworkView>();
        lastsighting = GetComponent<DoneLastPlayerSighting>();
	}
	
	// Update is called once per frame
	void Update () 
    {

        position = lastsighting.position;
        if(Network.isClient)
        nt.RPC("update", RPCMode.Others, position);

	}

    [RPC]
    void update(Vector3 l)
    {
        lastsighting.position = l;
    }
}
