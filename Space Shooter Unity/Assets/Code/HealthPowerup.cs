using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : MonoBehaviour
{
    public int minHealthToGive;
    public int maxHealthToGive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerShip>())
        {
            int healthToGive = Random.Range(minHealthToGive, maxHealthToGive);
            collision.GetComponent<PlayerShip>().GainArmor(healthToGive);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerShip>())
        {
            int healthToGive = Random.Range(minHealthToGive, maxHealthToGive);
            collision.gameObject.GetComponent<PlayerShip>().GainArmor(healthToGive);
            Destroy(gameObject);
        }
    }
}
