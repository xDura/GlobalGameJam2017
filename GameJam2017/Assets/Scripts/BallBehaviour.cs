using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {
    public int Speed = 3;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private GameObject ball;

	// Use this for initialization
	void Start () {
        ball = gameObject;
        InvokeRepeating("MoveBall", 0, Speed);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void MoveBall()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        ball.transform.position = new Vector3(x, y, 0);
    }
}
