using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller {

    bool hasActed = false;
    float distanceScore = 100.0f;
    bool failed = false;

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

        if (URSSManager.sweepLine == null)
        {
            Debug.LogError("SeepLine es nula LOLOLO");
            return;
        }

        float distanceScore = Mathf.Abs(URSSManager.sweepLine.transform.position.y - transform.position.y);
        Debug.Log("Player: " + gameObject.name + "Distance: " + distanceScore);
    }

    public void KillPlayer()
    {
        failed = true;
    }

}
