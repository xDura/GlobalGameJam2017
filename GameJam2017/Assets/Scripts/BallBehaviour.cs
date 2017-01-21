using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Timers;

public class BallBehaviour : MonoBehaviour {
    public int Speed = 3;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private GameObject ball;
    private bool hopping;

	// Use this for initialization
	void Start () {
        ball = gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(MoveBallTest());
    }

    IEnumerator MoveBallTest()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        if (hopping) yield break;

        hopping = true;
        var startPos = ball.transform.position;
        var dest = new Vector2(x, y);
        var timer = 0.0f;

        while (timer <= 1.0)
        {
            var height = Mathf.Sin(Mathf.PI * timer) * (y + 0.5f);
            ball.transform.position = Vector3.Lerp(startPos, dest, timer) + Vector3.up * height;

            timer += Time.deltaTime / 2f;
            yield return null;
        }

        hopping = false;
        
    }
}
