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
    public PlayerController menuPlayerController;
    public float minSuccessScore;

    public bool successDone = false;

    public static SweepLine currentSweepLine;

	// Use this for initialization
	void Start () {
        menuPlayerController.SetKeyCode(KeyCode.Space);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Exit();

        timer += Time.deltaTime;
        if(timer >= sweepLineSpawnTime)
        {
            SpawnSeepLine();
            timer = 0.0f;
        }

        menuPlayerController.UpdateManually();
    }

    public void SpawnSeepLine()
    {

        SweepLine[] sl = FindObjectsOfType<SweepLine>();
        if(sl != null)
        {
            for(int i = 0; i < sl.Length; i++)
                Destroy(sl[i].gameObject);
        }

        if (successDone)
            return;

        if (menuPlayerController.distanceScore <= minSuccessScore)
        {
            successDone = true;
            StartCoroutine(Success());
            return;
        }

        GameObject sweepLine = Instantiate(sweepLinePrefab, spawnTransform.transform.position, Quaternion.identity);
        currentSweepLine = sweepLine.GetComponent<SweepLine>();
        currentSweepLine.dir = sweepLineDir;
        currentSweepLine.ready = true;

        menuPlayerController.hasWaved = false;
    }

    IEnumerator Success()
    {
        yield return new WaitForSeconds(1f);
        StartGame();
        yield break;
    }

    public void StartGame()
    {
        currentSweepLine = null;
        SceneManager.LoadScene("Stadium");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
