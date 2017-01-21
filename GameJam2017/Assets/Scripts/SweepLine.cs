using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepLine : MonoBehaviour {

    public Vector2 dir;
	// Use this for initialization
	void Start () {
        Debug.Log(GetComponent<Collider2D>().isTrigger);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(dir);
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Entering");
        if (!collider.GetComponent<Controller>()) return;

        Controller controller = collider.GetComponent<Controller>();

        if(controller is NPCController)
        {
            Debug.Log("NPCController");
        }
        else if(controller is PlayerController)
        {
            Debug.Log("PlayerController");
        }
    }
}
