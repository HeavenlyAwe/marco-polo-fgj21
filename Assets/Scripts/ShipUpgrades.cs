using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipUpgrade
{
    LIGHT_CENTER,
    LIGHT_SIDES,
}

public class ShipUpgrades : MonoBehaviour
{
    public Light spotLight1;
    public Light sideLightLeft;
    public Light sideLightRight;

    public void ApplyUpgrade(ShipUpgrade upgrade)
    {
        Debug.Log("Applying upgrade " + upgrade);
        switch (upgrade)
        {
            case ShipUpgrade.LIGHT_CENTER:
                spotLight1.enabled = true;
                break;
            case ShipUpgrade.LIGHT_SIDES:
                sideLightLeft.enabled = true;
                sideLightRight.enabled = true;
                break;
            default:
                Debug.LogWarning("ShipUpgrade " + upgrade + " not used!");
                break;
        }
    }
}
