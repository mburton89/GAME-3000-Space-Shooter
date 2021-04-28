using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : MonoBehaviour
{
    PlayerArmor playerArmor;
    public int healthBonus = 20;

   void Awake()
    {
        playerArmor = FindObjectOfType<PlayArmor>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerArmor.currentArmor < playerArmor.maxArmor)
        {
            Destroy(GameObject);
            playerArmor.currentArmor = playerArmor.currentArmor + healthBonus;
        }
    }
}
