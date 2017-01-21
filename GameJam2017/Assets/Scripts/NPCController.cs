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

    public void SetSprites(Sprite gorro, Sprite cara, Sprite gafas, Sprite camiseta, Sprite raya)
    {
        r_cara.sprite = cara;
        r_gorro.sprite = gorro;
        r_gafas.sprite = gafas;
        r_camiseta.sprite = camiseta;
        r_raya. sprite = raya;
    }

}
