using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PickupManager : MonoBehaviour
{
    public int minimumPickups = 1;

    public int availablePickups = 0;

    public int collectedPickups = 0;

    public float minimumDistanceToLastPickup = 50.0f;

    private GameObject ship = null;

    private AudioSource pickupSoundSource;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
        availablePickups = pickups.Length;
        minimumPickups = Mathf.Min(minimumPickups, availablePickups);

        pickupSoundSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        ship = GameObject.FindGameObjectWithTag("Ship");
    }

    private IEnumerator WaitBeforeAscend(float waitTimer)
    {
        yield return new WaitForSeconds(waitTimer);
        ship.SendMessage("StartAscend");
    }

    public void OnPickup()
    {
        pickupSoundSource.Play();

        collectedPickups += 1;
        if (collectedPickups >= minimumPickups)
        {
            print("Pickups collected: " + collectedPickups + "/" + minimumPickups);

            ship.SendMessage("PauseBeforeAscend");
            StartCoroutine(WaitBeforeAscend(1.0f));
        }

        GameObject.Find("SoundManager").SendMessage("SwitchToNextClip");
    }

}
