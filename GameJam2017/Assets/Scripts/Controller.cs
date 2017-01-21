using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Controller : MonoBehaviour {

    public Vector2 startPos;
    private Vector2 maxPos;
    public bool wentUp = false;
    public bool hasWaved = false;

    [Header("WaveParams")]
    float waveOffset;
    public float timeUp;
    public float timeDown;
    public float maxOffset;
    public float minOffset;
    public Ease easeJumpType;
    public Ease easeFallType;
    

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

        startPos = transform.position;
        maxPos = transform.position;
        waveOffset = Random.Range(maxOffset, minOffset);
        maxPos.y += waveOffset;

        gameObject.transform.DOMove(new Vector3(maxPos.x, maxPos.y, 0), timeUp).SetEase(easeJumpType).OnComplete(GoDown);
    }

    public virtual bool HasWaved()
    {
        return hasWaved;
    }

    public void GoDown()
    {
        gameObject.transform.DOMove(new Vector3(startPos.x, startPos.y, 0), timeDown).SetEase(easeFallType);
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
