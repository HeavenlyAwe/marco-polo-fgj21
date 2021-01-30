using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public AudioSource[] audioSources;

    private int currentAudioSourceIndex = -1;

    private void Start()
    {
        currentAudioSourceIndex = audioSources.Length - 1;
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

    public void PlaySound(string source)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].isPlaying) return;
        }
        
        audioSources[currentAudioSourceIndex].Play();
    }
}
