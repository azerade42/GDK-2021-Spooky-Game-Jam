using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectGroundCollider : FallingObject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.RemoveTerrain(collision);
    }
}
