using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyShip>())
        {
            collision.GetComponent<Ship>().TakeDamage(10);
            
        }

    }
}
