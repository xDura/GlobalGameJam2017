using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : Controller {

    public SpriteRenderer r_cara;
    public SpriteRenderer r_gorro;
    public SpriteRenderer r_gafas;
    public SpriteRenderer r_camiseta;
    public SpriteRenderer r_raya;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
