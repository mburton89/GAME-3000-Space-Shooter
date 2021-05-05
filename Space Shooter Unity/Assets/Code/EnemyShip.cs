using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    public bool canShootPlayer;
    public bool canFlyTowardsPlayer;
    Transform target;
    public float sightDistance;
    private float DistanceFromPlayer;
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
        DistanceFromPlayer = Vector3.Distance(target.position, transform.position);
        base.Update();

        if (DistanceFromPlayer < sightDistance)
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
    }

    void FlyTowardsPlayer()
    {
        Vector2 directionToFace = new Vector2(target.position.x - transform.position.x, target.transform.position.y - transform.position.y);
        transform.up = directionToFace;
        Thrust();
    }
}
