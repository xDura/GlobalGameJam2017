using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public bool hasWaved = false;

    public virtual void Wave()
    {
        hasWaved = true;
    }

    public virtual bool HasWaved()
    {
        return hasWaved;
    }
}
