using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// This is the obvious script for  game Manager
/// this is where data flows through in the unity project. very handy
/// 
/// </summary>


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private AudioSystem audioSystem;
    private BulletSpawner bulletSpawner;

    private void Awake()
    {
        if(instance == null)//proper instance
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSystem = gameObject.GetComponent<AudioSystem>();
        bulletSpawner = gameObject.GetComponent<BulletSpawner>();
    }



    public void PlayAudio()//play audio signal between scripts
    {
        audioSystem.ReturnAudio(1);//send data
    }


    public void ShootGun(Transform transform, GameObject gameObject)//spawn entity signal between scripts
    {
        bulletSpawner.EntitySpawner(transform, gameObject);//send data
    }
}


