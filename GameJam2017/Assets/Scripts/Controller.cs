using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float waveOffset;
    public Vector2 startPos;
    private Vector2 maxPos;
    public float clanckValue = 0.05f;

    public bool wentUp = false;
    public bool hasWaved = false;

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
        startPos = transform.position;
        maxPos = transform.position;
        maxPos.y += waveOffset;

        while (!wentUp)
        {
            
            transform.position = Vector2.Lerp(transform.position, maxPos, 0.1f);
            if(Vector2.Distance(transform.position, startPos) >= waveOffset - clanckValue)
                wentUp = true;

            yield return null;
        }

        while (Vector2.Distance(transform.position, startPos) >= 0.01f)
        {
            transform.position = Vector2.Lerp(transform.position, startPos, clanckValue);
            yield return null;
        }

        transform.position = startPos;

        yield break;
    }
}
