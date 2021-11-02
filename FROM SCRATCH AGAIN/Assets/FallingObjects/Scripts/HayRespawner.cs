using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayRespawner : FallingObject
{
    List<GameObject> inactiveTerrains;

    // Start is called before the first frame update
    void Start()
    {
        inactiveTerrains = GameManager.Instance.inactiveTerrains;
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

        if (collision.CompareTag("Player"))
        {
            RespawnTerrain();
        }
    }

    private void RespawnTerrain()
    {
        float loDist = Mathf.Infinity;
        //float hiDist = 0;

        GameObject loDistTerrain = null;

        Vector2 plrPos = PlayerController.Instance.GetPlayerPos();

        foreach (GameObject terrain in inactiveTerrains)
        {
            float distFromPlayer = Vector2.Distance(plrPos, terrain.transform.position);

            if (distFromPlayer < loDist)
            {
                loDist = distFromPlayer;
                loDistTerrain = terrain;
                
            }
        }

        if (loDistTerrain)
        {
            GameManager.Instance.RemoveInactiveTerrain(loDistTerrain);
            loDistTerrain.SetActive(true);
        }

    }
}
