using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public int minimumPickups = 1;

    public int availablePickups = 0;

    public int collectedPickups = 0;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
        availablePickups = pickups.Length;
        minimumPickups = Mathf.Min(minimumPickups, availablePickups);
    }

    public void OnPickup()
    {
        collectedPickups += 1;
        if (collectedPickups >= minimumPickups)
        {
            print("Pickups collected: " + collectedPickups + "/" + minimumPickups);
            GameObject.FindGameObjectWithTag("Ship").SendMessage("StartAscend");
        }

        GameObject.Find("SoundManager").SendMessage("SwitchToNextClip");
    }

}
