using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller {

    bool hasActed = false;
    float distanceScore = 100.0f;
    bool failed = false;
    public bool debugInfo = false;

    Vector2 debugPos;
    Vector2 debugSweepLinePos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (debugInfo)
            Debug.DrawLine(debugPos, debugSweepLinePos, Color.red);
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

        debugPos = transform.position;
        debugSweepLinePos = URSSManager.sweepLine.transform.position;
        debugSweepLinePos.y = debugPos.y;
        float distanceScore = Mathf.Abs(URSSManager.sweepLine.transform.position.y - transform.position.y);
        Debug.LogError("Player: " + gameObject.name + "Distance: " + distanceScore);
    }

    public void KillPlayer()
    {
        failed = true;
    }

}
