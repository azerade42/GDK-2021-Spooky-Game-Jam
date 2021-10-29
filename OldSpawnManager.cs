using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the instantiation of all animals
public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;

    [SerializeField] float max_X = 12;
    [SerializeField] float max_Z = 12;
    [SerializeField] [Tooltip("spawnPosZ + a range of -max_Z, max_Z = animal starting Z position")]
    int spawnPosZ = 20;

    [SerializeField] float startDelay = 2.0f;
    [SerializeField] float spawnInterval = 1.5f; 

    // Start is called before the first frame update
    void Start()
    {
        // Invokes a method after x seconds, then invokes it again every y seconds
        // InvokeRepeating(string methodName, x, y);
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
        // you could replace this with a while (!gameOver) loop and set gameOver through a gameManager.cs
    }

    // Spawns a random animal in a set amount of seconds
    void SpawnRandomAnimal()
    {
        float animalXPos = Random.Range(-max_X, max_X + 1);
        float animalZPos = Random.Range(-max_Z, max_Z + 1);
        Vector3 animalSpawnPos = new Vector3(animalXPos, 0, spawnPosZ + max_Z);

        int animalIndex = Random.Range(0, animalPrefabs.Length); // first parameter is inclusive, second is exclusive (int only)
        Quaternion animalRotation = animalPrefabs[animalIndex].transform.rotation;

        Instantiate(animalPrefabs[animalIndex], animalSpawnPos, animalRotation);
    }
}
