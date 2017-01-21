using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllersManager : MonoBehaviour {

    PlayerController[] controllers;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (controllers == null) return;

        for(int i = 0; i < controllers.Length; i++)
        {
            if (GetKeyForPlayer(i))
                controllers[i].Wave();
        }
	}

    public void SetController(PlayerController[] nc)
    {
        controllers = nc;
    }

    public bool GetKeyForPlayer(int num)
    {
        switch (num)
        {
            case 1:
                return Input.GetKeyDown("Space");
            case 2:
                return Input.GetKeyDown(KeyCode.KeypadEnter);
            case 3:
                return Input.GetKeyDown(KeyCode.Q);
            case 4:
                return Input.GetKeyDown(KeyCode.P);
        }

        Debug.LogError("Esto no tiene que pasar");
        return Input.GetKeyDown("Space");
    }
}
