using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;          // The player to follow
    public EnemySpawner enemySpawner; // Reference to the spawner
    public float speed = 2f;          // Speed of the enemy

    // Initialize enemy properties
    public void Initialize(Transform player, float speed, EnemySpawner spawner)
    {
        this.player = player;
        this.speed = speed;
        this.enemySpawner = spawner;
    }

    void Update()
    {
        // Move towards the player
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    // When the enemy is disabled, return it to the pool
    private void OnDisable()
    {
        if (enemySpawner != null)
        {
            enemySpawner.ReturnEnemy(gameObject);
        }
    }

    public void Die()
    {
        enemySpawner.currentEnemyCount--;
        // Destroy this enemy
        gameObject.SetActive(false);
    }

}
