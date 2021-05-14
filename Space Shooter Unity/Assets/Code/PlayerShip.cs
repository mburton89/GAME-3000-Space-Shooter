using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    public Projectile absorbProjectilePrefab;
    public Projectile allyProjectilePrefab;

    public bool hasBulletLimit; //on/off switch for the bullet limit challenege.
    public int numberOfAbsorbProjectiles;
    public int numberOfAllyProjectiles;

    public GameObject rope;
    public GameObject mace;

    public enum Special
    {
        absorb,
        ally,
        dash,
        mines,
        mace
    }

    public Special currentSpecial;

    void Start()
    {
        currentAmmo = 0;
        UpdateHud();
    }

    void Update()
    {
        base.Update();
        FollowMouse();
        HandleUserInput();
    }

    void HandleUserInput()
    {
        if (Input.GetMouseButton(1))
        {
            Thrust();
        }

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            if (hasBulletLimit) //if the bullet limiter is checked.
            {
                if (currentAmmo < maxAmmo) //if the current ammo is less than the max ammo.
                {
                    FireProjectile();
                    currentAmmo++;
                    HUD.Instance.UpdateAmmoCountText(currentAmmo, maxAmmo);
                }
            }
            else//if the bullet limiter is not cheecked
            {
                if (numberOfAbsorbProjectiles > 0)
                {
                    FireAbsorbProjectile();
                }
                else if (numberOfAllyProjectiles > 0)
                {
                    FireAllyProjectile();
                }
                else
                {
                    FireProjectile();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseSpecial();
        }
    }

    void UseSpecial()
    {
        if (currentSpecial == Special.absorb && numberOfAbsorbProjectiles > 0)
        {
            FireAbsorbProjectile();
        }
        else if (currentSpecial == Special.ally && numberOfAllyProjectiles > 0)
        {

        }
        else if (currentSpecial == Special.dash)
        {

        }
        else if (currentSpecial == Special.mines)
        {

        }
        else if (currentSpecial == Special.mace)
        {

        }
    }

    public void FireAbsorbProjectile()
    {
        Projectile projectile = Instantiate(absorbProjectilePrefab, projectileSpawnPoint.position, transform.rotation) as Projectile;
        Instantiate(thrustParticlePrefab, projectileSpawnPoint.position, transform.rotation);
        projectile.rigidBody2D.AddForce(transform.up * projectileSpeed);
        projectile.Init(this.gameObject);
        Destroy(projectile.gameObject, 4);
        fireProjectileSound.Play();
        numberOfAbsorbProjectiles--;
    }

    public void FireAllyProjectile()
    {
        Projectile projectile = Instantiate(allyProjectilePrefab, projectileSpawnPoint.position, transform.rotation) as Projectile;
        Instantiate(thrustParticlePrefab, projectileSpawnPoint.position, transform.rotation);
        projectile.rigidBody2D.AddForce(transform.up * projectileSpeed);
        projectile.Init(this.gameObject);
        Destroy(projectile.gameObject, 4);
        fireProjectileSound.Play();
        numberOfAllyProjectiles--;
    }

    void FollowMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10)); //Finds Mouse Position on Screen
        Vector2 directionToFace = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y); //creates direction based on positon of ship and mouse cursor 
        transform.up = directionToFace; //Faces Mouse. Assigns transform.up the Direction to Face
    }

    public void AddHealth(int healthToAdd)
    {
        if (currentArmor < maxArmor)
        {
            currentArmor += healthToAdd;
            HUD.Instance.UpdateHealthBar(currentArmor, maxArmor);
        }
    }

    public void UpdateHud()
    {
        HUD.Instance.UpdateAmmoCountText(currentAmmo, maxAmmo);
    }
}
