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

    public GameObject credits;

    private AudioSource pickupSoundSource;

    private IEnumerator WaitAndDestroySunkenShips(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        GameObject[] sunkenShips = GameObject.FindGameObjectsWithTag("SunkenShip");
        for (int i = 0; i < sunkenShips.Length; i++)
        {
            Destroy(sunkenShips[i]);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
        availablePickups = pickups.Length;
        minimumPickups = Mathf.Min(minimumPickups, availablePickups);

        pickupSoundSource = GetComponent<AudioSource>();

        StartCoroutine(WaitAndDestroySunkenShips(6.0f));
    }

    private void Start()
    {
        ship = GameObject.FindGameObjectWithTag("Ship");
    }




    private IEnumerator WaitAndAscend(float waitTimer)
    {
        yield return new WaitForSeconds(waitTimer);
        ship.SendMessage("StartAscend");
        credits.SetActive(true);
    }

    public void OnPickup()
    {
        pickupSoundSource.Play();

        collectedPickups += 1;
        if (collectedPickups >= minimumPickups)
        {
            print("Pickups collected: " + collectedPickups + "/" + minimumPickups);

            ship.SendMessage("PauseBeforeAscend");
            StartCoroutine(WaitAndAscend(1.0f));
        }

        GameObject.Find("SoundManager").SendMessage("SwitchToNextClip");
    }
}
