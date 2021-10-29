using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bean : FallingObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.MoveDownwards();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.RemoveTerrain(collision);
        base.DestroyOnPlayerTouch(collision);
    }
}
