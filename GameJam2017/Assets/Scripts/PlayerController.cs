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

    public AudioController audioController;
    public GameObject bloodPivot;

    public GameObject currentBlood;

    public void SetKeyCode(KeyCode _keyCode)
    {
        keyCode = _keyCode;
    }

    public void Awake()
    {
        audioController = FindObjectOfType<AudioController>();
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

    public void BleedOut(GameObject bloodPrefab)
    {
        currentBlood = GameObject.Instantiate(bloodPrefab, bloodPivot.transform.position, Quaternion.identity);
        if (Random.Range(0, 1) == 1)
            currentBlood.GetComponent<Animator>().SetTrigger("blood2");
        else
            currentBlood.GetComponent<Animator>().SetTrigger("blood");
    }

    public override void Wave()
    {
        if (HasWaved()) return;

        base.Wave();
        audioController.PlayJump();

        SweepLine sweepLine;
        if (URSSManager.sweepLine != null)
            sweepLine = URSSManager.sweepLine;

        else if (Mainmenu.currentSweepLine != null)
            sweepLine = Mainmenu.currentSweepLine;
        else
        {
            Debug.LogError("SeepLine es nula LOLOLO");
            return;
        }

        debugPos = transform.position;
        debugSweepLinePos = sweepLine.transform.position;
        debugSweepLinePos.y = debugPos.y;
        distanceScore = Mathf.Abs(sweepLine.transform.position.x - transform.position.x);
        //Debug.LogError("Player: " + gameObject.name + "Distance: " + distanceScore);
    }

    public void OpenLight()
    {
        _light.DOIntensity(2.5f, 1f);
    }

    public void CloseLight()
    {
        _light.DOIntensity(0.0f, 1f);
    }

}
