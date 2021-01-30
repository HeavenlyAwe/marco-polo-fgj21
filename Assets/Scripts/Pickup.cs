using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pickup : MonoBehaviour
{
    public AudioClip[] audioClips;

    public ShipUpgrade upgrade;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ship"))
        {
            // Enable the upgrade
            other.gameObject.SendMessage("ApplyUpgrade", upgrade);

            // Move the backseat driver to the player
            BackseatDriver backseatDriver = gameObject.GetComponentInChildren<BackseatDriver>();
            backseatDriver.gameObject.transform.parent = other.gameObject.transform;

            GameObject.Destroy(gameObject);

            GameObject.Find("PickupManager").SendMessage("OnPickup");
        }
    }


    private IEnumerator WaitAndPlaySound(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        audioSource.Play();
    }


    public void PlaySound(float timer)
    {
        if (audioSource.isPlaying) return;

        audioSource.clip = audioClips[Random.Range(0, audioClips.Length - 1)];
        StartCoroutine(WaitAndPlaySound(timer));
    }
}
