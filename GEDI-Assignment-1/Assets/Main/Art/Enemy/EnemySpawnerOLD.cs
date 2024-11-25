
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerOLD : MonoBehaviour
{
    public GameObject enemy;
    int randNum;
    public Transform spawnDest1, spawnDest2, spawnDest3, spawnDest4;
    public bool spawningbool = true;
    public float spawnTime;


    void Start()
    {
        StartCoroutine(spawning());
    }
         
    IEnumerator spawning()
    {
        while (spawningbool == true)
        {
            yield return new WaitForSeconds(spawnTime);
            randNum = UnityEngine.Random.Range(0, 4);
            if (randNum == 0)
            {
                GameManager.instance.ShootGun(spawnDest1, enemy);
            }
            
            if (randNum == 1)
            {
                GameManager.instance.ShootGun(spawnDest2, enemy);
            }

            if (randNum == 2)
            {
                GameManager.instance.ShootGun(spawnDest3, enemy);
            }

            if (randNum == 3)
            {
                GameManager.instance.ShootGun(spawnDest4, enemy);
            }
        }
    }
}