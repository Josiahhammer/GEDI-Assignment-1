using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player; // Player transform to follow
    public float speed = 2f; // Speed at which the enemy moves

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the enemy toward the player
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
