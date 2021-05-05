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

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollected(other.gameObject);
    }


    public void HandleCollected(GameObject other)
    {
        if (other.GetComponent<PlayerShip>())
        {
            if (currentPowerupType == PowerupType.absorbProjectile)
            {
                other.GetComponent<PlayerShip>().numberOfAbsorbProjectiles += 3;
            }
            else if (currentPowerupType == PowerupType.allyProjectile)
            {
                other.GetComponent<PlayerShip>().numberOfAllyProjectiles += 1;
            }

            Destroy(gameObject);
        }
    }
}
