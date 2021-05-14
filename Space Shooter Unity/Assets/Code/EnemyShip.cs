using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    public bool canShootPlayer;
    public bool canFlyTowardsPlayer;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool isAlly;
    public float sightDistance;
    private float DistanceFromPlayer;

    private float _initialMaxSpeed;
    private float _initialAcceleration;
    [SerializeField] private float _hyperSpeed;
    [SerializeField] private float _hyperAcceleration;
    [SerializeField] private float _maxDistanceFromPlayer;

    void Awake()
    {
        base.Awake();
        _initialMaxSpeed = maxSpeed;
        _initialAcceleration = acceleration;
        isAlly = false;
        target = FindObjectOfType<PlayerShip>().transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerShip>())
        {
            Explode();
            collision.gameObject.GetComponent<PlayerShip>().TakeDamage(1);
        }
        else if (collision.gameObject.GetComponent<EnemyShip>() && isAlly)
        {
            TakeDamage(1);
            collision.gameObject.GetComponent<EnemyShip>().Explode();
        }
    }

    void Update()
    {
        if (target == null || target == this.transform)
        {
            if (isAlly)
            {
                int rand = Random.Range(0, FindObjectsOfType<EnemyShip>().Length);
                target = FindObjectsOfType<EnemyShip>()[rand].transform;
            }
            else
            {
                return;
            }
        } 

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

        float distanceFromPlayer = Vector3.Distance(transform.position, target.position);
        if (distanceFromPlayer > _maxDistanceFromPlayer)
        {
            maxSpeed = _hyperSpeed;
            acceleration = _hyperAcceleration;
        }
        else
        {
            maxSpeed = _initialMaxSpeed;
            acceleration = _initialAcceleration;
        }

        Thrust();
    }
}
