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

    //assets
    [Header("Assets")]
    public SpriteRenderer r_cara;
    public SpriteRenderer r_gorro;
    public SpriteRenderer r_gafas;
    public SpriteRenderer r_camiseta;
    public SpriteRenderer r_raya;

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

    public void SetLayer(string layer)
    {
        r_cara.sortingLayerName = layer;
        r_gorro.sortingLayerName = layer;
        r_gafas.sortingLayerName = layer;
        r_camiseta.sortingLayerName = layer;
        r_raya.sortingLayerName = layer;
    }
}
