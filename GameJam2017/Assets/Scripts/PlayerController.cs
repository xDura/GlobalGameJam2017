using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : Controller {

    bool hasActed = false;
    public float distanceScore =float.MaxValue;
    bool failed = false;
    public bool debugInfo = false;

    public int id = -1;

    Vector2 debugPos;
    Vector2 debugSweepLinePos;
    KeyCode keyCode = KeyCode.Space;

    public Light _light;

    public void SetKeyCode(KeyCode _keyCode)
    {
        keyCode = _keyCode;
    }

	// Update is called once per frame
	void Update () {
        if (debugInfo)
            Debug.DrawLine(debugPos, debugSweepLinePos, Color.red);
    }
    
	public void UpdateManually () {
        if (Input.GetKeyDown(keyCode))
            Wave();
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
        distanceScore = Mathf.Abs(URSSManager.sweepLine.transform.position.x - transform.position.x);
        //Debug.LogError("Player: " + gameObject.name + "Distance: " + distanceScore);
    }

    public void OpenLight()
    {
        _light.DOIntensity(1.0f, 1f);
    }

    public void CloseLight()
    {
        _light.DOIntensity(4.0f, 1f);
    }

}
