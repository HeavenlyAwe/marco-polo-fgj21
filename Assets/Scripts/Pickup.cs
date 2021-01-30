using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public AudioSource[] audioSources;

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
            Debug.Log("Player collided " + other.gameObject.name);
            other.gameObject.SendMessage("ApplyUpgrade", ShipUpgrade.LIGHTS);
            GameObject.Destroy(gameObject);
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

        Debug.Log("Timer set to: " + playSoundAfterTimer);
    }
}
