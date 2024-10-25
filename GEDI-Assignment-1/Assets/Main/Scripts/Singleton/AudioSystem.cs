using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    // script for playing laser sound. received from game manager.

    public string ReturnAudio()
    {

        source.PlayOneShot(clip);//play
        return "Audio playing";
        
    }
}
