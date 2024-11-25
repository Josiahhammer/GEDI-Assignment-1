using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    private GameObject enemyPrefab;
    private Transform player;
    private EnemySpawner spawner;

    // Constructor to initialize the factory with necessary references
    public EnemyFactory(GameObject enemyPrefab, Transform player, EnemySpawner spawner)
    {
        this.enemyPrefab = enemyPrefab;
        this.player = player;
        this.spawner = spawner;
    }

    // Method to create and configure a new enemy with a specific speed
    public GameObject CreateEnemy(Vector3 spawnPosition, float speed)
    {
        // Instantiate the enemy prefab
        GameObject newEnemy = Object.Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Configure the enemy
        Enemy enemy = newEnemy.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.player = player;         // Assign the player reference
            enemy.enemySpawner = spawner; // Assign the spawner reference
            enemy.speed = speed;           // Set the enemy speed
        }

        return newEnemy;
    }
}

