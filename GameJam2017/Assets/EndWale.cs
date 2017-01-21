using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWale : MonoBehaviour {

    public URSSManager urssManager;

	// Use this for initialization
	void Start () { 
		
	}


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (urssManager == null) return;
        urssManager.EndWave();
    }

}
