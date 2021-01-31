using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackseatDriver : MonoBehaviour
{
    public AudioClip[] audioClips;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    public void PlayClip()
    {
        audioSource.Play();
    }

    public void SetRandomClip()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length - 1)];
    }

    public float Duration()
    {
        return audioSource.clip.length;
    }
}
