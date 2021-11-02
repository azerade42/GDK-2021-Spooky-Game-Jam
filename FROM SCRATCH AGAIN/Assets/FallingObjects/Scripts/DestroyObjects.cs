using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FallingObject"))
        {
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver = true;
        }
    }
}
