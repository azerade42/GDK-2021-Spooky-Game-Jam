using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] FallingObject[] objectsToSpawn;
    FallingObject objectToSpawn;
    [SerializeField] int xRange;
    [SerializeField] float yPosition;
    [SerializeField] float startSpawnRate;
    [SerializeField] float spawnRateIncrease;

    private float spawnRate;
    private float currentTime = 0f;
    private float timeSinceLastSpawn = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnRate = Mathf.Clamp(Mathf.Log(currentTime, 0.05f) + 2f, 0.2f, 2);
    }
    
    void HandleSpawnRate()
    {
        objectToSpawn = objectsToSpawn[0];
        if (objectToSpawn) // change to include "and not game over"
        {
            spawnRate = Mathf.Clamp(Mathf.Log(currentTime, 0.05f) + 2f, 0.2f, 2);
            SpawnFallingObject();
        }
    }

    private void Update()
    {
        // Wait for game to start, then do this
        currentTime += Time.deltaTime;
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn > spawnRate)
        {
            timeSinceLastSpawn = 0f;
            HandleSpawnRate();
        }
    }

    void SpawnFallingObject()
    {
        int randomXLocation = Random.Range(-xRange, xRange);
        bool randomAddon = (Random.value > 0.5f);
        float xLocation = randomXLocation;
        if (randomAddon) xLocation += 0.5f;

        Vector3 spawnPosition = new Vector3(xLocation, yPosition, 0);

        ObjectPooler.Instance.SpawnFromPool("Human", spawnPosition);

        //Object.Instantiate<FallingObject>(objectToSpawn, spawnPosition, Quaternion.identity);
    }    
}
