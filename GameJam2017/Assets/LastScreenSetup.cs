using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastScreenSetup : MonoBehaviour {

    public URSSManager urssManager;

    public List<GameObject> gameObjectsToActivate;
    public List<GameObject> playersCross;
    public List<Light> lights;
    public bool reseted = false;

    public void Restore()
    {
        reseted = false;
        for (int i = 0; i < playersCross.Count; i++)
            playersCross[i].SetActive(false);
        for (int i = 0; i < gameObjectsToActivate.Count; i++)
            gameObjectsToActivate[i].SetActive(false);
        for (int i = 0; i < lights.Count; i++)
            lights[i].gameObject.SetActive(true);
    }

    public void SetUp(bool _1, bool _2, bool _3, bool _4)
    {
        //Restore();
        for (int i = 0; i < gameObjectsToActivate.Count; i++)
            gameObjectsToActivate[i].SetActive(true);
        if (_1)
        {
            playersCross[0].SetActive(true);
            lights[0].gameObject.SetActive(false);
        }
        if (_2)
        {
            playersCross[1].SetActive(true);
            lights[1].gameObject.SetActive(false);
        }
        if (_3)
        {
            playersCross[2].SetActive(true);
            lights[2].gameObject.SetActive(false);
        }
        if (_4)
        {
            playersCross[3].SetActive(true);
            lights[3].gameObject.SetActive(false);
        }

    }

    public void Kill(int i)
    {
        playersCross[i].SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        if(!reseted)
            urssManager.Start();
        reseted = true;
    }

    public IEnumerator RestartGameCoroutien()
    {
        yield return new WaitForSeconds(1f);

        Fader.FadeOut();

        yield return new WaitForSeconds(2f);

        urssManager.Start();
    }

}
