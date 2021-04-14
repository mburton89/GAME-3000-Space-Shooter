using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public int damageToGive;
    GameObject firingShip;
    public bool isFlame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFlame && collision.GetComponent<PlayerShip>())
        {
            collision.GetComponent<Ship>().TakeDamage(damageToGive);
            Destroy(gameObject);
        }

        if (!isFlame && collision.GetComponent<Ship>() && collision.gameObject != firingShip)
        {
            collision.GetComponent<Ship>().TakeDamage(damageToGive);
            Destroy(gameObject);
        }
    }

    public void Init(GameObject firer)
    {
        firingShip = firer;
    }
}
