using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip1;
    public AudioClip clip2;

    // script for playing laser sound. received from game manager.

    private void OnEnable()
    {
        // Find the player and subscribe to health updates
        var player = FindObjectOfType<PlayerCommand>();
        if (player != null)
        {
            player.OnHealthChanged += ReturnAudio;
        }
        else
        {
            Debug.LogWarning("PlayerCommand not found in scene!");
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        var player = FindObjectOfType<PlayerCommand>();
        if (player != null)
        {
            player.OnHealthChanged -= ReturnAudio;
        }
    }

    public void ReturnAudio(float clip)
    {
        if (clip == 1)
        {
            source.PlayOneShot(clip1);//play
            return;
        }
        else if (clip < 1)
        {
            source.PlayOneShot(clip2);//play
            return;
        }

    }
}
