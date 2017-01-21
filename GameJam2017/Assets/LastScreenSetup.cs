using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastScreenSetup : MonoBehaviour {

    public List<GameObject> gameObjectsToActivate;
    public List<GameObject> playersCross;

    public void Restore()
    {
        for (int i = 0; i < playersCross.Count; i++)
            playersCross[i].SetActive(false);
        for (int i = 0; i < gameObjectsToActivate.Count; i++)
            gameObjectsToActivate[i].SetActive(false);
    }

    public void SetUp(bool _1, bool _2, bool _3, bool _4)
    {
        //Restore();
        for (int i = 0; i < gameObjectsToActivate.Count; i++)
            gameObjectsToActivate[i].SetActive(true);
        if (_1)
            playersCross[0].SetActive(true);
        if (_2)
            playersCross[1].SetActive(true);
        if (_3)
            playersCross[2].SetActive(true);
        if (_4)
            playersCross[3].SetActive(true);

    }

    public void Kill(int i)
    {
        playersCross[3].SetActive(true);
    }

}
