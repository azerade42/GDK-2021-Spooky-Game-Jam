using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayRespawner : FallingObject
{
    // Start is called before the first frame update
    void Start()
    {
        base.StartCustom();
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
