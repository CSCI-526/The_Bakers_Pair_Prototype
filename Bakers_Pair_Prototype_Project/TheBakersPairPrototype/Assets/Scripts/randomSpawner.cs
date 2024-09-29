using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawner : MonoBehaviour
{
    public GameObject cubePrefab;   // Assign the cube prefab in the Inspector
    public GameObject spherePrefab; // Assign the sphere prefab in the Inspector

    public float spawnRangeX = 8f;  // Horizontal range for spawning
    public float spawnY = 8f;       // Vertical position (top of the screen)
    public float spawnInterval = 2f; // Time interval between spawns

    void Start()
    {
        // Start spawning objects at regular intervals
        InvokeRepeating("SpawnRandomObject", 1f, spawnInterval);
    }

    void SpawnRandomObject()
    {
        // Randomly choose between cube and sphere
        GameObject objectToSpawn = Random.value > 0.5f ? cubePrefab : spherePrefab;

        // Randomly determine the X position within the spawn range
        float spawnX = Random.Range(-spawnRangeX, spawnRangeX);

        // Create a spawn position at the top of the screen
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, -9.0f);

        // Spawn the selected prefab at the determined position
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
