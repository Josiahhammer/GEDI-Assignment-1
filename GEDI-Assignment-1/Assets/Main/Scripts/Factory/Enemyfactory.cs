using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    private GameObject enemyPrefab;
    private Transform player;
    private EnemySpawner spawner;
    private ObjectPool objectPool;

    // Constructor to initialize the factory with necessary references
    public EnemyFactory(GameObject enemyPrefab, Transform player, EnemySpawner spawner, int poolSize)
    {
        this.enemyPrefab = enemyPrefab;
        this.player = player;
        this.spawner = spawner;

        // Initialize the object pool
        objectPool = new ObjectPool(enemyPrefab, poolSize);
    }

    // Method to create and configure a new enemy with a specific speed
    public GameObject CreateEnemy(Vector3 spawnPosition, float speed)
    {
        // Get an enemy from the pool
        GameObject newEnemy = objectPool.GetObject();

        // Configure the enemy
        Enemy enemy = newEnemy.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Initialize(player, speed, spawner);
        }

        // Position the enemy at the spawn point
        newEnemy.transform.position = spawnPosition;
        return newEnemy;
    }

    // Return enemy to the pool
    public void ReturnEnemy(GameObject enemy)
    {
        objectPool.ReturnObject(enemy);
    }
}
