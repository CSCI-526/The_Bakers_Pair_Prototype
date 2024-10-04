using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawner : MonoBehaviour
{
    public GameObject cubePrefab;   
    public GameObject spherePrefab; 

    public float spawnRangeX = 8f;  
    public float spawnY = 8f;       
    public float spawnInterval = 2f; 
    public float spawnSpeedIncreaseInterval = 20f; 
    public float spawnSpeedFactor = 0.8f; 

    void Start()
    {
        InvokeRepeating("SpawnRandomObject", 1f, spawnInterval);
        StartCoroutine(IncreaseSpawnSpeedOverTime());
    }

    void SpawnRandomObject()
    {
        GameObject objectToSpawn = Random.value > 0.5f ? cubePrefab : spherePrefab;
        float spawnX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, -9.0f);
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }

    IEnumerator IncreaseSpawnSpeedOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnSpeedIncreaseInterval);
            CancelInvoke("SpawnRandomObject");
            spawnInterval *= spawnSpeedFactor;
            InvokeRepeating("SpawnRandomObject", 0f, spawnInterval);
        }
    }
}
