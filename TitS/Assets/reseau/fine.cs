using UnityEngine;
using System.Collections;

public class fine : MonoBehaviour {

    public GameObject fin;
    public bool fini = false;
	
	// Update is called once per frame
	void Update () {
        if (fini)
            fin.SetActive(true);
	}
}
