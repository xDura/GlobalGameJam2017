using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour {

    public float sweepLineSpawnTime;
    float timer = 0.0f;

    public GameObject sweepLinePrefab;

    public GameObject spawnTransform;
    public Vector2 sweepLineDir;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= sweepLineSpawnTime)
        {
            SpawnSeepLine();
            timer = 0.0f;
        }
	}

    public void SpawnSeepLine()
    {
        GameObject sweepLine = Instantiate(sweepLinePrefab, spawnTransform.transform.position, Quaternion.identity);
        sweepLine.GetComponent<SweepLine>().dir = sweepLineDir;
        sweepLine.GetComponent<SweepLine>().ready = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Stadium");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
