using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType
    {
        allyProjectile,
        absorbProjectile,
        chargeShot,
        armorPickup
    }

    public PowerupType currentPowerupType;

    void Start()
    {
        
    }
}
