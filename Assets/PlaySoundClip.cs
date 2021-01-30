using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundClip : MonoBehaviour
{
    public AudioSource[] audioSources;

    private int currentAudioSourceIndex = -1;

    private void Start()
    {
        currentAudioSourceIndex = audioSources.Length - 1;
    }

    public void PlaySound(string source)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].isPlaying) return;
        }
        
        audioSources[currentAudioSourceIndex].Play();
    }
}
