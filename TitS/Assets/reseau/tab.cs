using UnityEngine;
using System.Collections;

public class tab : MonoBehaviour {

    public GameObject table;
    public bool recu = false;
	
	// Update is called once per frame
	void Update () {
        if(recu)
        {
            
            table.SetActive(true);
        }
	
	}
}
