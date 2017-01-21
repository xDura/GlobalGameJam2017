using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float waveOffset;
    public Vector2 startPos;
    private Vector2 maxPos;

    public bool wentUp = false;
    public bool hasWaved = false;

    void Awake()
    {
        startPos = transform.position;
    }

    public virtual void Wave()
    {
        hasWaved = true;
        StartCoroutine(WaveCoroutine());
    }

    public virtual bool HasWaved()
    {
        return hasWaved;
    }

    public IEnumerator WaveCoroutine()
    {
        maxPos = transform.position;
        maxPos.y += waveOffset;

        while (!wentUp)
        {
            
            transform.position = Vector2.Lerp(transform.position, maxPos, 0.1f);
            if(Vector2.Distance(transform.position, startPos) >= waveOffset - 0.1f)
                wentUp = true;

            yield return null;
        }

        while (Vector2.Distance(transform.position, startPos) >= 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, startPos, 0.1f);
            yield return null;
        }

        transform.position = startPos;

        yield break;
    }
}
