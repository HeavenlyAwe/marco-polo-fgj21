using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipUpgrade
{
    LIGHTS,
}

public class ShipUpgrades : MonoBehaviour
{
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
    }
}
