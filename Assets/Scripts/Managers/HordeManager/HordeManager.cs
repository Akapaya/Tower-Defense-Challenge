using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ObjectPool objectPool;

    [Header("Spawn Settings")]
    [SerializeField] private float initialSpawnInterval = 5f;
    [SerializeField] private float spawnIntervalDecreaseRate = 0.1f;
    [SerializeField] private float minimumSpawnInterval = 1f;
    [SerializeField] private float timeToIntervalDecrease = 60f;

    [SerializeField] private float currentSpawnInterval;
    private float timeSinceLastSpawn;
    private float timeSinceLastIntervalDecrease;

    private bool isSpawning = true;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        timeSinceLastSpawn = 0f;
        timeSinceLastIntervalDecrease = 0f;
    }

    void Update()
    {
        if (isSpawning)
        {
            timeSinceLastSpawn += Time.deltaTime;
            timeSinceLastIntervalDecrease += Time.deltaTime;

            if (timeSinceLastSpawn >= currentSpawnInterval)
            {
                SpawnEnemy();
                timeSinceLastSpawn = 0f;
            }

            if (timeSinceLastIntervalDecrease >= timeToIntervalDecrease)
            {
                DecreaseSpawnInterval();
                timeSinceLastIntervalDecrease = 0f;
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = objectPool.RandomizeObject();
    }

    private void DecreaseSpawnInterval()
    {
        currentSpawnInterval = Mathf.Max(minimumSpawnInterval, currentSpawnInterval - spawnIntervalDecreaseRate);
    }
}
