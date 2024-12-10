using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentAudio : MonoBehaviour
{
    public static PersistentAudio instance;

    private AudioSource audioSource;

    void Awake()
    {
        // Check if an instance already exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate audio player
            return;
        }

        // Set the instance to this script
        instance = this;
        
        // Mark this GameObject to not be destroyed on scene load
        DontDestroyOnLoad(gameObject);

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    // Play audio
    public void PlayAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Stop audio
    public void StopAudio()
    {
        audioSource.Stop();
    }

    // Set volume (0 to 1)
    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }
}
