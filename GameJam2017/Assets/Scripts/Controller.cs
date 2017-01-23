using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class Controller : MonoBehaviour {

    public Vector2 startPos;
    private Vector2 maxPos;
    public bool wentUp = false;
    public bool hasWaved = false;

    public Transform bodyReference;

    [Header("WaveParams")]
    float waveOffset;
    public float timeUp;
    public float timeDown;
    public float maxOffset;
    public float minOffset;
    public Ease easeJumpType;
    public Ease easeFallType;

    public GameObject armUpPos;
    public SpriteRenderer arm;
    public SpriteRenderer arm2;

    //assets
    [Header("Assets")]
    public SpriteRenderer r_cara;
    public SpriteRenderer r_camiseta;
    public SpriteRenderer r_pantalon;

    public virtual void Wave()
    {
        hasWaved = true;

        startPos = transform.position;
        maxPos = transform.position;
        waveOffset = Random.Range(maxOffset, minOffset);
        maxPos.y += waveOffset;

        //brazo
        arm.transform.position = armUpPos.transform.position;
        arm.flipY = true;
        arm2.transform.position = armUpPos.transform.position;
        arm2.flipY = true;

        gameObject.transform.DOMove(new Vector3(maxPos.x, maxPos.y, 0), timeUp).SetEase(easeJumpType).OnComplete(GoDown);
    }

    public virtual bool HasWaved()
    {
        return hasWaved;
    }

    public void GoDown()
    {
        gameObject.transform.DOMove(new Vector3(startPos.x, startPos.y, 0), timeDown).SetEase(easeFallType).OnComplete(FullDown);
    }

    public void FullDown()
    {
        //brazo
        arm.transform.position = transform.position;
        arm.flipY = false;

        arm2.transform.position = transform.position;
        arm2.flipY = false;
    }

    public void OnWaveCompleted()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
            hasWaved = false;
    }

    public void SetLayer(string layer)
    {
        r_cara.sortingLayerName = layer;
        r_camiseta.sortingLayerName = layer;
        r_pantalon.sortingLayerName = layer;
        arm.sortingLayerName = layer;
        arm2.sortingLayerName = layer;
    }
}
