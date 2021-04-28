﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    public bool canShootPlayer;
    public bool canFlyTowardsPlayer;
    public float fedUp;
    public bool quicked;
    Transform target;
    void Awake()
    {
        base.Awake();
        target = FindObjectOfType<PlayerShip>().transform;
        fedUp = 0;
        quicked = true;
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
        float dist = Vector3.Distance(target.position, transform.position);

        if(dist >= 15 && quicked == true )
        {
            acceleration += 10;
            maxSpeed += 40;
            fedUp++;
            quicked = false;
        }
        if (dist<=2 && fedUp <=3 && quicked == false)
        {
            acceleration -= 10;
            maxSpeed -= 40;
            quicked = true;
        }
        transform.up = directionToFace;
        Thrust();
    }
}
