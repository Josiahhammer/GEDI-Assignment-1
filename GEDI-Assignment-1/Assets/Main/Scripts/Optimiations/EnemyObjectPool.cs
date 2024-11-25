using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool
{
    private GameObject enemyPrefab;
    private Queue<GameObject> pool;
    private int poolSize;

    public EnemyObjectPool(GameObject enemyPrefab, int poolSize)
    {
        this.enemyPrefab = enemyPrefab;
        this.poolSize = poolSize;
        pool = new Queue<GameObject>();

        // Pre-fill the pool with enemy objects
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Object.Instantiate(enemyPrefab);
            enemy.SetActive(false); // Start with all enemies inactive
            pool.Enqueue(enemy);
        }
    }

    // Get an enemy from the pool
    public GameObject GetEnemy()
    {
        if (pool.Count > 0)
        {
            GameObject enemy = pool.Dequeue();  // Dequeue an enemy from the pool
            enemy.SetActive(true);  // Activate the enemy
            return enemy;
        }
        else
        {
            Debug.LogWarning("Enemy pool is empty! Consider increasing pool size.");
            return null;  // Return null if the pool is empty
        }
    }

    // Return an enemy back to the pool
    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);  // Deactivate the enemy
        pool.Enqueue(enemy);  // Return it to the pool
    }
}
