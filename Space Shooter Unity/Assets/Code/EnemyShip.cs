using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    public bool canShootPlayer;
    public bool canFlyTowardsPlayer;
    Transform target;

    void Awake()
    {
        base.Awake();
        target = FindObjectOfType<PlayerShip>().transform;
    }

    void Update()
    {
        if (canShootPlayer)
        {
            ShootPlayer();
        }
        if (canFlyTowardsPlayer)
        {
            FlyTowardsPlayer();
        }
    }

    void ShootPlayer()
    {

    }

    void FlyTowardsPlayer()
    {
        Vector2 directionToFace = new Vector2(target.position.x - transform.position.x, target.transform.position.y - transform.position.y);
        transform.up = directionToFace;
        Thrust();
    }
}
