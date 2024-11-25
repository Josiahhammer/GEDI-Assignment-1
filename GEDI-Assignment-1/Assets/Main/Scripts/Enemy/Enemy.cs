using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public Transform player; // Reference to the player's Transform
    public float speed = 2f; // Speed at which the enemy moves
    public EnemySpawner enemySpawner;

    void Start()
    {
        player = GameObject.Find("PF_Player").transform; // Adjust to your player GameObject name
    }

    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Die()
    {
        enemySpawner.currentEnemyCount--;
        // Destroy this enemy
        Destroy(gameObject);
    }
}
