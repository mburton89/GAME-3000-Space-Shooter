﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public Projectile projectilePrefab;
    public Transform projectileSpawnPoint;
    public Transform projectileSpawnPointLeft;
    public Transform projectileSpawnPointRight;
    public AudioSource hitSound;
    public AudioSource fireProjectileSound;
    public GameObject thrustParticlePrefab;
    public Transform particleSpawnPoint;

    public float acceleration;
    public float maxSpeed;
    public int maxArmor;
    public float fireRate;
    public float projectileSpeed;

    [HideInInspector] public float currentSpeed;
    [HideInInspector] public int currentArmor;
    [HideInInspector] public bool canShoot;

    public bool fireDouble;

    public void Awake()
    {
        currentArmor = maxArmor;
        canShoot = true;
    }

    void FixedUpdate()
    {
        if (rigidBody2D.velocity.magnitude > maxSpeed)
        {
            rigidBody2D.velocity = rigidBody2D.velocity.normalized * maxSpeed;
        }
    }

    public void Thrust()
    {
        rigidBody2D.AddForce(transform.up * acceleration); //Add force in the direction we're facing
        currentSpeed = maxSpeed; //Set our speed to our max speed
        float randomX = Random.Range(-0.1f, 0.1f);
        float randomY = Random.Range(-0.1f, 0.1f);
        Vector3 spawnPosition = new Vector3(particleSpawnPoint.position.x + randomX, particleSpawnPoint.position.y + randomY);
        Instantiate(thrustParticlePrefab, spawnPosition, transform.rotation);
    }

    public void FireProjectile()
    {
        if (fireDouble)
        {
            Projectile projectile1 = Instantiate(projectilePrefab, projectileSpawnPointLeft.position, transform.rotation) as Projectile;
            Instantiate(thrustParticlePrefab, projectileSpawnPointLeft.position, transform.rotation);
            projectile1.rigidBody2D.AddForce(transform.up * projectileSpeed);
            projectile1.Init(this.gameObject);
            Destroy(projectile1.gameObject, 4);

            Projectile projectile2 = Instantiate(projectilePrefab, projectileSpawnPointRight.position, transform.rotation) as Projectile;
            Instantiate(thrustParticlePrefab, projectileSpawnPointRight.position, transform.rotation);
            projectile2.rigidBody2D.AddForce(transform.up * projectileSpeed);
            projectile2.Init(this.gameObject);
            Destroy(projectile2.gameObject, 4);
        }
        else
        {
            Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation) as Projectile;
            Instantiate(thrustParticlePrefab, projectileSpawnPoint.position, transform.rotation);
            projectile.rigidBody2D.AddForce(transform.up * projectileSpeed);
            projectile.Init(this.gameObject);
            Destroy(projectile.gameObject, 4);
        }
        fireProjectileSound.Play();
        StartCoroutine(FireRateBuffer());
    }

    private IEnumerator FireRateBuffer()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public void TakeDamage(int damageToTake)
    {
        currentArmor -= damageToTake;
        hitSound.Play();
        if (currentArmor <= 0)
        {
            Explode();
        }

        if (GetComponent<PlayerShip>())
        {
            HUD.Instance.UpdateHealthBar(currentArmor, maxArmor);
        }
    }

    public void Explode()
    {
        Instantiate(Resources.Load("ShipExplosion"), transform.position, transform.rotation);
        //ScreenShaker.Instance.ShakeScreen();
        ScreenShakeManager.Instance.ShakeScreen();
        EnemyShipSpawner.Instance.CountEnemyShips();

        if (GetComponent<PlayerShip>())
        {
            GameManager.Instance.HandlePlayerDestroyed();
        }

        Destroy(gameObject);
    }
}
