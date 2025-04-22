using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeSpawner : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;
    public int spawnCount = 5;
    public int mazeWidth = 10;
    public int mazeHeight = 10;
    public float cellSize = 1f;
    public float spawnDelay = 0.1f;

    void Start()
    {
        Invoke(nameof(SpawnPrefabs), spawnDelay);
    }

    void SpawnPrefabs()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            int randX = Random.Range(0, mazeWidth);
            int randZ = Random.Range(0, mazeHeight);
            Vector3 spawnPos = new Vector3(randX * cellSize, 0.2f, randZ * cellSize);


            GameObject prefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }
} 
