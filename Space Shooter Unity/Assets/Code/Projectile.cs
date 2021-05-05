using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum ProjectileType
    {
        normal,
        absorb,
        ally
    }

    public ProjectileType activeProjectileType;

    public Rigidbody2D rigidBody2D;
    public int damageToGive;
    GameObject firingShip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ship>() && collision.gameObject != firingShip)
        {
            collision.GetComponent<Ship>().TakeDamage(damageToGive);
            Destroy(gameObject);

            if (activeProjectileType == ProjectileType.absorb)
            {
                FindObjectOfType<PlayerShip>().AddHealth(1);
            }
            else if (activeProjectileType == ProjectileType.ally)
            {
                collision.GetComponent<EnemyShip>().target = FindObjectOfType<EnemyShip>().transform;
                collision.GetComponent<EnemyShip>().isAlly = true;
            }
        }
    }

    public void Init(GameObject firer)
    {
        firingShip = firer;
    }
}
