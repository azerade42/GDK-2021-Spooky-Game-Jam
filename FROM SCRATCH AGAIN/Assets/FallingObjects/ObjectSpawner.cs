using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] FallingObject[] objectsToSpawn;
    FallingObject objectToSpawn;
    [SerializeField] Transform objectSpawner;
    SpriteRenderer objectSpawnerSR;
    [SerializeField] float objectSpawnerMoveSpeed;
    [SerializeField] int xRange;
    [SerializeField] float yPosition;
    [SerializeField] float startSpawnRate;
    [SerializeField] float spawnRateIncrease;

    private float spawnRate;
    private float currentTime = 0f;
    private float timeSinceLastSpawn = 0f;

    Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        objectSpawnerSR = objectSpawner.gameObject.GetComponent<SpriteRenderer>();
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

        Vector3 spawnerPos = objectSpawner.position;

        if (spawnerPos.x > spawnPosition.x)
            objectSpawnerSR.flipX = true;
        else
            objectSpawnerSR.flipX = false;

        objectSpawner.position = Vector3.Lerp(spawnerPos, spawnPosition, Time.deltaTime * 1/spawnRate * objectSpawnerMoveSpeed);
    }

    void SpawnFallingObject()
    {
        int randomXLocation = Random.Range(-xRange, xRange);
        bool randomAddon = (Random.value > 0.5f);
        float xLocation = randomXLocation;
        if (randomAddon) xLocation += 0.5f;

        spawnPosition = new Vector3(xLocation, yPosition, 0);

        StartCoroutine(MoveUFOTowardsFallingObject(spawnPosition, 0.5f));
        //objectSpawner.position = spawnPosition;

        

        //Object.Instantiate<FallingObject>(objectToSpawn, spawnPosition, Quaternion.identity);
    }

    IEnumerator MoveUFOTowardsFallingObject(Vector3 spawnPosition, float moveTime)
    {
        //print('g');
        //while (Vector2.Distance(objectSpawner.position, spawnPosition) > 0.01f)
            //print('f');
        
        yield return new WaitForSeconds(moveTime);
        ObjectPooler.Instance.SpawnFromPool("Human", spawnPosition);
    }
}
