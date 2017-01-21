using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller {

    bool hasActed = false;
    float distance = 100.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public override void Wave()
    {
        if (HasWaved()) return;

        base.Wave();
        
    }
}
