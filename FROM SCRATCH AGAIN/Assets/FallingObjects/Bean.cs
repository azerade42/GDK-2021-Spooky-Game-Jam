using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bean : FallingObject
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        base.StartCustom();

        spriteRenderer = GetComponent<SpriteRenderer>();
        int rX = Random.Range(0, 2);
        int rY = Random.Range(0, 2);
        if (rX != 0)
            spriteRenderer.flipX = true;
        if (rY != 0)

        spriteRenderer.flipY = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        base.MoveDownwards();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.DestroyOnPlayerTouch(collision);
    }
}
