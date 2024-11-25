using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;   // The enemy prefab to instantiate
    public Transform player;         // The player to assign to the enemies
    public int maxEnemies = 10;      // Maximum number of enemies to spawn
    public float spawnInterval = 2f; // Time interval between spawns (in seconds)

    public int currentEnemyCount = 0; // Current number of enemies spawned

    private EnemyFactory enemyFactory; // The factory for creating enemies

    void Start()
    {
        // Initialize the factory with the prefab, player, and spawner reference
        enemyFactory = new EnemyFactory(enemyPrefab, player, this);

        // Start the coroutine to spawn enemies
        StartCoroutine(SpawnEnemies());
    }

    // Coroutine to spawn enemies at a regular interval
    IEnumerator SpawnEnemies()
    {
        while (currentEnemyCount < maxEnemies)
        {
            // Spawn a new enemy with a random speed between 1.5 and 3.5
            float randomSpeed = Random.Range(1.5f, 3.5f);
            SpawnEnemy(randomSpeed);

            // Wait for the specified interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Method to spawn a single enemy using the factory
    public void SpawnEnemy(float speed)
    {
        Vector3 spawnPosition = GetRandomPosition();
        GameObject newEnemy = enemyFactory.CreateEnemy(spawnPosition, speed);

        // Increment the enemy count
        currentEnemyCount++;
    }

    // Example method to generate random spawn positions
    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-10f, 10f);
        float y = 1f; // Keep y constant, adjust for your environment
        float z = Random.Range(-10f, 10f);
        return new Vector3(x, y, z);
    }
}
