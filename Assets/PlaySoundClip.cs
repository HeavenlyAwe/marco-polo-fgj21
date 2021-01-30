using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundClip : MonoBehaviour
{
    private AudioSource audioSource;

    //Play the music
    bool m_Play;
    //Detect when you use the toggle, ensures music isn’t played multiple times
    bool m_ToggleChange;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //Ensure the toggle is set to true for the music to play at start-up
        m_Play = true;
    }

    public void PlaySound(string source)
    {
        if (audioSource.isPlaying) return;

        audioSource.Play();
    }
}
