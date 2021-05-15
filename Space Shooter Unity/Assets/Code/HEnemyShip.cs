﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HEnemyShip : Ship
{
    public float turretDistance;
    public bool canShootPlayer;
    public bool canFlyTowardsPlayer;
    public bool turret;
    Transform target;

    void Awake()
    {
        base.Awake();
        target = FindObjectOfType<PlayerShip>().transform;
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
        if (target == null) return;
        float distance = Vector3.Distance(target.position, transform.position);

        if (canShootPlayer && canShoot)
        {
            FireProjectile();
        }
        if (canFlyTowardsPlayer && target != null)
        {
            FlyTowardsPlayer();
        }

        if (turret)
        {
            if (distance >= turretDistance)
            {
                canShootPlayer = false;
            }

            else
            {
                canShootPlayer = true;
            }
        }
    }

    void FlyTowardsPlayer()
    {
        if (target != null)
        {
            Vector2 directionToFace = new Vector2(target.position.x - transform.position.x, target.transform.position.y - transform.position.y);
            transform.up = directionToFace;
            Thrust();
        }
    }
}