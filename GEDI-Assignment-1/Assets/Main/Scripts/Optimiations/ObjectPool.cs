using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    private struct PooledObject
    {
        public GameObject prefab;
        public int numToSpawn;
    }

    [SerializeField] private PooledObject[] pools;

    private static readonly Dictionary<string, Queue<GameObject>> pooledObjects = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        pooledObjects.Clear();

        foreach (PooledObject pool in pools) 
        {
            string name = pool.prefab.name;
            Transform parent = new GameObject(name).transform;
            parent.SetParent(transform);
            Queue<GameObject> objectsToSpawn = new(pool.numToSpawn);
            for (int i = 0; i < pool.numToSpawn; i++)
            {
                GameObject rb = Instantiate(pool.prefab, parent);
                rb.gameObject.SetActive(false);
                objectsToSpawn.Enqueue(rb);
            }
            pooledObjects.Add(name, objectsToSpawn);
        }


    }

    public static GameObject Shoot(string name, Vector3 position, float speed, Quaternion rotation)
    {
        if (!pooledObjects.ContainsKey(name))
        {
            Debug.LogAssertion("Pool does not contain key: " + name);
            return null;
        }
        GameObject rb = pooledObjects[name].Dequeue();

        rb.transform.position = position;
        rb.gameObject.SetActive(true);

        pooledObjects[name].Enqueue(rb);
        return rb;
    }
}
