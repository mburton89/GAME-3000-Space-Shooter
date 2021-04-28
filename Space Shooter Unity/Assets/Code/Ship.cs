﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public Projectile FlamesPrefab;
    public Projectile projectilePrefab;
    public Transform projectileSpawnPoint;
    public AudioSource hitSound;
    public AudioSource fireProjectileSound;
    //public AudioSource flameSound;
    //public GameObject thrustParticlePrefab;
    //public Transform particleSpawnPoint;

    public float acceleration;
    public float maxSpeed;
    public int maxArmor;
    public float fireRate;
    public float projectileSpeed;

    [HideInInspector] public float currentSpeed;
    [HideInInspector] public int currentArmor;
    [HideInInspector] public bool canShoot;


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
        if (GetComponent<EnemyShip>())
        {
        rigidBody2D.AddForce(transform.up * acceleration); //Add force in the direction we're facing
        }
        currentSpeed = maxSpeed; //Set our speed to our max speed
        float randomX = Random.Range(-0.1f, 0.1f);
        float randomY = Random.Range(-0.1f, 0.1f);
        //Vector3 spawnPosition = new Vector3(particleSpawnPoint.position.x + randomX, particleSpawnPoint.position.y + randomY);
        //Instantiate(thrustParticlePrefab, spawnPosition, transform.rotation);
    }

    public void FireProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation) as Projectile;
        //Instantiate(thrustParticlePrefab, projectileSpawnPoint.position, transform.rotation);
        projectile.rigidBody2D.AddForce(transform.up * projectileSpeed);
        projectile.Init(this.gameObject);
        fireProjectileSound.Play();
        Destroy(projectile, 4);
        StartCoroutine(FireRateBuffer());
    }

    public void FireProjectile(Transform newTransform)
    {
        //TODO pass in Direction
        Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, newTransform.rotation) as Projectile;
        //Instantiate(thrustParticlePrefab, projectileSpawnPoint.position, newTransform.rotation);
        projectile.rigidBody2D.AddForce(newTransform.up * projectileSpeed);
        projectile.Init(this.gameObject);
        fireProjectileSound.Play();
        Destroy(projectile, 4);
        StartCoroutine(FireRateBuffer());
    }

    public void FireFlames()
    {
        Projectile projectile = Instantiate(FlamesPrefab, projectileSpawnPoint.position, transform.rotation);
        //Instantiate(FlamesPrefab, projectileSpawnPoint.position, transform.rotation);
        projectile.rigidBody2D.AddForce(transform.up * projectileSpeed);
        projectile.Init(this.gameObject);
        fireProjectileSound.Play();
        Destroy(projectile.gameObject, .3f);
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
