using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("DestroyPerkele", 5);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void DestroyPerkele () {
        Destroy(this.gameObject);
    }
}
