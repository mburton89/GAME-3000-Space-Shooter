using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    public bool canShootPlayer;
    public bool canFlyTowardsPlayer;
    public float fedUp;
    Transform target;

    void Awake()
    {
        base.Awake();
        target = FindObjectOfType<PlayerShip>().transform;
        fedUp = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerShip>())
        {
            Explode();
            collision.gameObject.GetComponent<PlayerShip>().TakeDamage(1);
        }
    }

    void Update()
    {
        if (canShootPlayer && canShoot)
        {
            FireProjectile();

        }
        if (canFlyTowardsPlayer && target != null)
        {
            FlyTowardsPlayer();
        }
    }

    void FlyTowardsPlayer()
    {
        Vector2 directionToFace = new Vector2(target.position.x - transform.position.x, target.transform.position.y - transform.position.y);

        float distancex = Mathf.Abs(target.transform.position.x) - Mathf.Abs(transform.position.x);
        float distancey = Mathf.Abs(target.transform.position.y) - Mathf.Abs(transform.position.y);
        if (distancex >= 15 || distancex <= -15 || distancey >= 15 || distancey <= -15)
        {
            acceleration = 10;
            maxSpeed = 40;
            fedUp++;

        }
        if (distancex <= 2 && distancex >= -2 && fedUp <=60    || distancey <= 2 && distancey >= -2 && fedUp <=60)
        {
            acceleration = 1;
            maxSpeed = 4;
        }
        transform.up = directionToFace;
        Thrust();
    }
}
