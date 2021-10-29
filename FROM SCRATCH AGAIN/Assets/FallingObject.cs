using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] protected float fallSpeed;

    private void Update()
    {
        MoveDownwards();
    }

    protected virtual void MoveDownwards()
    {
        float yTranslation = -fallSpeed * 0.1f * Time.deltaTime;
        transform.Translate(new Vector3(0, yTranslation, 0));
    }

    protected void RemoveTerrain(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    protected void DestroyOnPlayerTouch(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);

            // Fire an event for adding score, etc

            Destroy(gameObject);
        }
    }
}
