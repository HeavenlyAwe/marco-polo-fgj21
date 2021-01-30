using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public AudioClip[] audioClips;

    private AudioSource audioSource;

    public int currentClip;
    public int nextClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentClip = -1;
        nextClip = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (nextClip != currentClip)
        {
            if (audioSource.isPlaying)
            {
                audioSource.loop = false;
            }
            else
            {
                currentClip = nextClip;
                audioSource.clip = audioClips[currentClip];
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }

    public void SwitchToNextClip()
    {
        nextClip += 1;
        nextClip = Mathf.Clamp(nextClip, 0, audioClips.Length - 1);
    }
}
