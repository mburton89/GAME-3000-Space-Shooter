using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public int damageToGive;
    public int projectilehp;
    public int currenthp;
    GameObject firingShip;
    public void Awake()
    {
        currenthp = projectilehp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ship>() && collision.gameObject != firingShip)//&& collisision.GetComponent<Ship>(currentArmor > 0)
        {
            this.currenthp--;
            collision.GetComponent<Ship>().TakeDamage(damageToGive);
            //firingShip.GetComponent<Ship>().Regen();
            if (currenthp <= 0)
            {
                Destroy(gameObject);
            }

        }
    }
    public void Init(GameObject firer)
    {
        firingShip = firer;
    }
}
