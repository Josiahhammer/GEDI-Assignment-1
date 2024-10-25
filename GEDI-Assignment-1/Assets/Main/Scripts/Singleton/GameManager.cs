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
    private FactorySpawner factorySpawner;

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
        factorySpawner = gameObject.GetComponent<FactorySpawner>();
    }

    public void PlayAudio()//play audio signal between scripts
    {
        Debug.Log(audioSystem.ReturnAudio());//send data
    }


    public void ShootGun(Transform transform, GameObject gameObject)//spawn entity signal between scripts
    {
        factorySpawner.EntitySpawner(transform, gameObject);//send data
    }
}


