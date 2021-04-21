using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    public bool canShootPlayer;
    public bool canShootFlames;
    public bool canFlyTowardsPlayer;
    Transform target;
    public bool CanExplode;
    [HideInInspector] public float distanceFromPlayer;
    public float minDistanceFromPlayer;

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
        distanceFromPlayer = Vector3.Distance(transform.position, target.position);
        //print("distance from player" + distanceFromPlayer);

        if (canShootPlayer && canShoot)
        {
            FireProjectile();
        }
        if (distanceFromPlayer > minDistanceFromPlayer && target != null)
        {
            Thrust();
        }
        if (canFlyTowardsPlayer && target != null)
        {
            FacePlayer();
        }
        if (canShootFlames && canShoot)
        {
            FireFlames();
        }
    }

    void FacePlayer()
    {
        Vector2 directionToFace = new Vector2(target.position.x - transform.position.x, target.transform.position.y - transform.position.y);
        transform.up = directionToFace;
    }

    void allowableDistance()
    {
      

    }
}
