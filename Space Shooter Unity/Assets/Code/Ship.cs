using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public Projectile projectilePrefab;
    public Transform projectileSpawnPoint;
    public AudioSource hitSound;
    public AudioSource fireProjectileSound;
    public GameObject thrustParticlePrefab;
    public Transform particleSpawnPoint;

    public float acceleration;
    public float maxSpeed;
    public int maxArmor;
    public float fireRate;
    public float projectileSpeed;
    public float chargePower;

    [HideInInspector] public float currentSpeed;
    [HideInInspector] public int currentArmor;
    [HideInInspector] public bool canShoot;
    [HideInInspector] public bool canMove;
    [HideInInspector] public bool leCharge;

    public void Awake()
    {
        currentArmor = maxArmor;
        canShoot = true;
        canMove = true;
    }

    void FixedUpdate()
    {
        if (rigidBody2D.velocity.magnitude > maxSpeed)
        {
            rigidBody2D.velocity = rigidBody2D.velocity.normalized * maxSpeed;
        }
        if(leCharge == true)
        {
            chargePower++;
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
        Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation) as Projectile;
        Instantiate(thrustParticlePrefab, projectileSpawnPoint.position, transform.rotation);
        projectile.rigidBody2D.AddForce(transform.up * projectileSpeed);
        projectile.Init(this.gameObject);
        fireProjectileSound.Play();
        Destroy(projectile, 4);
        StartCoroutine(FireRateBuffer());
    }
    public void ChargedShot()
    {


        
        // 0 frames normal shot
        if (Input.GetMouseButtonUp(0) && chargePower < 20)
        {
            Projectile projectile1 = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation) as Projectile;
            //Instantiate(thrustParticlePrefab, projectileSpawnPoint.position, transform.rotation);
            projectile1.rigidBody2D.AddForce(transform.up * projectileSpeed);
            projectile1.Init(this.gameObject);
            fireProjectileSound.Play();
            Destroy(projectile1.gameObject, 1);
            StartCoroutine(FireRateBuffer());
            chargePower = 0;
            leCharge = false;
        }
        // 20 frames Quick shot
        if (Input.GetMouseButtonUp(0) && chargePower >= 20)
        {
            Projectile projectile2 = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation) as Projectile;
            //Instantiate(thrustParticlePrefab, projectileSpawnPoint.position, transform.rotation);
            projectile2.rigidBody2D.AddForce(transform.up * projectileSpeed * 2); //doubled
            projectile2.Init(this.gameObject);
            fireProjectileSound.Play();                                       //sound
            Destroy(projectile2.gameObject, 4);
            StartCoroutine(FireRateBuffer());
            chargePower = 0;
            leCharge = false;
        }
        // 40 frames snipe shot

        //if (Input.GetMouseButtonUp(0) && chargePower >= 20)
        //{
        //    Projectile projectile3 = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation) as Projectile;
        //    Instantiate(thrustParticlePrefab, projectileSpawnPoint.position, transform.rotation);
        //    projectile3.rigidBody2D.AddForce(transform.up * projectileSpeed);
        //    projectile3.Init(this.gameObject);
        //    fireProjectileSound.Play();                                        //sound
        //    Destroy(projectile3, 4); //lasts longer
        //    StartCoroutine(FireRateBuffer());
        //    chargePower = 0;
        //    leCharge = false;
        //}
        //// 60 frames Power shot
        //Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation) as Projectile; //create multiple projectiles on each other
        //Instantiate(thrustParticlePrefab, projectileSpawnPoint.position, transform.rotation);
        //projectile.rigidBody2D.AddForce(transform.up * projectileSpeed);
        //projectile.Init(this.gameObject);
        //fireProjectileSound.Play();                                         //sound
        //Destroy(projectile, 1);
        //StartCoroutine(FireRateBuffer());
        //// 120 frames Ultra shot
        //Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation) as Projectile; //create multiple projectiles on each other
        //Instantiate(thrustParticlePrefab, projectileSpawnPoint.position, transform.rotation); 
        //projectile.rigidBody2D.AddForce(transform.up * projectileSpeed * 2); //doubled
        //projectile.Init(this.gameObject);
        //fireProjectileSound.Play();                                         //sound
        //Destroy(projectile, 4); //lasts longer
        //StartCoroutine(FireRateBuffer());
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
    //public void Regen()
    //{
    //    if(currentArmor < maxArmor)
    //    {
    //        currentArmor = currentArmor + 1;
    //    }
    //}
}
