using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private Transform firePoint; // Entity location
    public GameObject entityPrefab; // Entity object
    private ObjectPool bulletPool; // Object pool for bullets

    [SerializeField] private int poolSize = 20; // Size of the bullet pool

    private void Start()
    {
        // Initialize the object pool
        bulletPool = new ObjectPool(entityPrefab, poolSize);
    }

    public void EntitySpawner(Transform transform, GameObject gameObject) // Get values from GameManager
    {
        // Set firePoint and entityPrefab
        firePoint = transform;
        entityPrefab = gameObject;

        // Get a bullet from the pool
        GameObject bullet = bulletPool.GetObject();
        if (bullet != null)
        {
            // Set bullet's position and rotation
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;

            // Reactivate the bullet
            bullet.SetActive(true);

            // Optionally reset bullet's state if needed (e.g., velocity)
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = bullet.transform.right * 20f; // Reset velocity
            }
        }
        else
        {
            Debug.LogWarning("Bullet pool is empty! Consider increasing pool size.");
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        // Return the bullet to the pool
        bulletPool.ReturnObject(bullet);
    }
}
