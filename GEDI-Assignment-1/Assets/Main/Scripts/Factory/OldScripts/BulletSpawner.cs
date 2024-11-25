using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

/// <summary>
/// 
/// This script will showcase my understanding of the factory design pattern, 
/// and how both the bullets and the enemies spawn from the same script with 
/// transform and object info received from other scipts that are calling for it.
/// 
/// 
/// 
/// </summary>


public class BulletSpawner : MonoBehaviour
{

    private Transform firePoint;//entity location
    private GameObject entityPrefab;//entity object


    public void EntitySpawner(Transform transform, GameObject gameObject)//get values from game manager
    {
        firePoint = transform;//set transform
        entityPrefab = gameObject;//set object

        Instantiate(entityPrefab, firePoint.position, firePoint.rotation);//Instantiate object
    }
}
