using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject prefab; // The prefab to pool
    private Queue<GameObject> pool; // Queue to hold pooled objects
    private int maxSize; // Maximum pool size

    public ObjectPool(GameObject prefab, int maxSize)
    {
        this.prefab = prefab;
        this.maxSize = maxSize;
        pool = new Queue<GameObject>();

        // Prepopulate the pool
        for (int i = 0; i < maxSize; i++)
        {
            GameObject obj = Object.Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // Optionally expand pool if maxSize isn't strict
            GameObject obj = Object.Instantiate(prefab);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
