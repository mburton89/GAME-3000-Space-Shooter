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
    public float hittimer;
    public bool canShootPlayer;
    public bool canFlyTowardsPlayer;
    public bool IsHit;
    public float Hitdown = (0.2f);
    public float acceleration;
    public float maxSpeed;
    public int maxArmor;
    public bool canBeStunned;
    public float fireRate;
    public float projectileSpeed;
    public float dashspeed;
    public float dashtime;
    public float startDashTime;
    public int direction;
    public Transform DashSpawn;
    public GameObject dashEffect;
    public float cooldowntime = 3;
    public float nextDashTime = 0;
    [HideInInspector] public float currentSpeed;
    [HideInInspector] public int currentArmor;
    [HideInInspector] public bool canShoot;
    [HideInInspector] public bool canTakeDamage;


    public void Awake()
    {
        currentArmor = maxArmor;
        canShoot = true;
        canTakeDamage = true;
        canBeStunned = true;
     

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
        Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation) as Projectile;
        Instantiate(thrustParticlePrefab, projectileSpawnPoint.position, transform.rotation);
        projectile.rigidBody2D.AddForce(transform.up * projectileSpeed);
        projectile.Init(this.gameObject);
        fireProjectileSound.Play();
        Destroy(projectile, 4);
        StartCoroutine(FireRateBuffer());
    }

    public void FireProjectilestun()
    {
        Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation) as Projectile;
        Instantiate(thrustParticlePrefab, projectileSpawnPoint.position, transform.rotation);
        projectile.rigidBody2D.AddForce(transform.up * projectileSpeed);
        projectile.Init(this.gameObject);
        fireProjectileSound.Play();
        Destroy(projectile, 4);
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
        if (canTakeDamage == true)
        {
            currentArmor -= damageToTake;
            
            hitSound.Play();
            if (GetComponent<ActualCCship>() && GetComponent<EnemyShip>())
            {

                StartCoroutine(StunCo());
                StartCoroutine(StunDuration());



            }


            if (currentArmor <= 0)
            {
                Explode();
            }

            if (GetComponent<PlayerShip>() || GetComponent<ActualCCship>() || GetComponent<ActualDashShip>())
            {
                HUD.Instance.UpdateHealthBar(currentArmor, maxArmor);
            }
        }
      





    }


    public IEnumerator StunDuration()
    {
        print("can't be stunned");
        canBeStunned = false;
        yield return new WaitForSeconds(8);

        canBeStunned = true;
        print("can be stunned");

    }


    public IEnumerator StunCo()
    {
        canShootPlayer = false;
        canFlyTowardsPlayer = false;
        yield return new WaitForSeconds(3);
        canShootPlayer = true;
        canFlyTowardsPlayer = true;
        
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
