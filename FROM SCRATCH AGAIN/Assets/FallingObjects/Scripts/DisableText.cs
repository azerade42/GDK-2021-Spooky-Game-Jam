using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableText : MonoBehaviour, IPooledObject
{
    public void OnObjectSpawn()
    {
        StartCoroutine(DisableTextAfterSeconds(1f));
    }

    IEnumerator DisableTextAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }

    public void DisableTextDisplay()
    {
        gameObject.SetActive(false);
    }
}
