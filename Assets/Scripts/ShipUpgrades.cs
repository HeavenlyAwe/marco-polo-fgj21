using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipUpgrade
{
    LIGHTS,
}

public class ShipUpgrades : MonoBehaviour
{
    public Light spotLight1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyUpgrade(ShipUpgrade upgrade)
    {
        Debug.Log("Applying upgrade " + upgrade);
        spotLight1.enabled = true;
    }
}
