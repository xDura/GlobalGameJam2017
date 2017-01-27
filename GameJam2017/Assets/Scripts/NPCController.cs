using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : Controller {
    [Header("Delay")]
    public float maxDelay;
    public float minDelay;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Wave()
    {
        StartCoroutine(DelayedWave());
    }

    IEnumerator DelayedWave()
    {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        base.Wave();
        yield break;
    }

    public void SetSprites(Sprite cara, Sprite camiseta, Sprite pantalon, Sprite l_arm, Sprite r_arm)
    {
        r_cara.sprite = cara;
        r_camiseta.sprite = camiseta;
        r_pantalon.sprite = pantalon;
        arm.sprite = l_arm;
        arm2.sprite = r_arm;
    }

}
