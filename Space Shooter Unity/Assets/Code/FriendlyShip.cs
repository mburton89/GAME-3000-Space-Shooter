using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyShip : Ship
{
    public bool canShootEnemyShip;
    public bool canFlyTowardsEnemyShip;
    Transform target;

    void Awake()
    {
        base.Awake();
        target = FindObjectOfType<EnemyShip>().transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyShip>())
        {
            //Explode();
            currentArmor--;
            collision.gameObject.GetComponent<EnemyShip>().TakeDamage(1);
        }
    }

    void Update()
    {
        if (canShootEnemyShip && canShoot)
        {
            FireProjectile();
        }
        if (canFlyTowardsEnemyShip && target != null)
        {
            FlyTowardsEnemyShip();
        }
    }

    void FlyTowardsEnemyShip()
    {
        Vector2 directionToFace = new Vector2(target.position.x - transform.position.x, target.transform.position.y - transform.position.y);
        transform.up = directionToFace;
        Thrust();
    }
}
