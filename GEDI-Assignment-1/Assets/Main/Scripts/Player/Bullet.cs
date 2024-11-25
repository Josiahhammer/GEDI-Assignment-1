using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;

    private void OnEnable()
    {
        // Set velocity when the bullet is enabled
        if (rb != null)
        {
            rb.velocity = transform.right * speed;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Die();
            }

            // Return to the pool instead of destroying
            ReturnToPool();
        }
        else if (collider.gameObject.CompareTag("Wall"))
        {
            // Return to the pool instead of destroying
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        BulletSpawner spawner = FindObjectOfType<BulletSpawner>();
        if (spawner != null)
        {
            spawner.ReturnBullet(gameObject);
        }
        else
        {
            Debug.LogWarning("No BulletSpawner found! Destroying bullet.");
            Destroy(gameObject); // Fallback in case no spawner exists
        }
    }
}
