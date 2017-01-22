using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepLine : MonoBehaviour {
    public Vector2 initPos;
    public Vector2 dir;
    public bool ready = false;

    private void Awake()
    {
        initPos = transform.position;
    }

    // Use this for initialization
    void Start () {
        initPos = transform.position;
        Debug.Log(GetComponent<Collider2D>().isTrigger);
	}
	
	// Update is called once per frame
	void Update () {
        if(ready)
            transform.Translate(dir*Time.deltaTime);
	}

    public void Init()
    {
        transform.position = initPos;
        ready = false;
    }

    public void StartWave()
    {
        ready = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("sweepLineDestroyer"))
            Destroy(this.gameObject);

        if (!collider.GetComponent<Controller>()) return;

        Controller controller = collider.GetComponent<Controller>();

        if(controller is NPCController)
        {
            //Debug.Log("NPCController");
            controller.Wave();
        }
    }
}
