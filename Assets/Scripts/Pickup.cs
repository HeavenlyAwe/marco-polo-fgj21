using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public AudioSource[] audioSources;

    // Parameters to setup the cockpit screamers
    public AudioClip audioClipMarco;

    [Range(0.0f, 1.0f)]
    public float audioClipMarcoVolume = 0.1f;

    public ShipUpgrade upgrade;


    // Parameters supporting multiple versions of the "Polo" answering sound
    private int currentAudioSourceIndex = -1;

    private bool playingSound = false;
    private float playSoundAfterTimer = 0.0f;


    void Start()
    {
        currentAudioSourceIndex = audioSources.Length - 1;
    }

    void Update()
    {
        if (playingSound)
        {
            playSoundAfterTimer -= Time.deltaTime;
            if (playSoundAfterTimer <= 0.0f)
            {
                audioSources[currentAudioSourceIndex].Play();
                playingSound = false;
            }
        }
    }

    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ship"))
        {
            // Enable the upgrade
            other.gameObject.SendMessage("ApplyUpgrade", upgrade);
            GameObject.Destroy(gameObject);

            // Add and enable another audio source on the ship for calling out "Marco"
            AudioSource audioSource = other.gameObject.AddComponent<AudioSource>();
            audioSource.clip = audioClipMarco;
            audioSource.volume = audioClipMarcoVolume;
        }
    }

    public void PlaySound(float timer)
    {
        if (playingSound) return;

        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].isPlaying) return;
        }

        playSoundAfterTimer = timer;
        playingSound = true;

        // Debug.Log("Timer set to: " + playSoundAfterTimer);
    }
}
