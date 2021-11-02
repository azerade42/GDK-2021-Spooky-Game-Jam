using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectGroundCollider : FallingObject
{
    [SerializeField] FallingObject parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("DestroysBlocks"))
        {
            base.RemoveTerrain(collision, this);

            //Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);

            /*if (collision.CompareTag("Terrain") && parent is HayRespawner)
            {
                GameManager.Instance.AddInactiveTerrain(collision.gameObject);
            }*/
        }
    }
}
