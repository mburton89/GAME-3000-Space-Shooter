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
    public int numberOfMines;

    public GameObject rope;
    public GameObject mace;

    public DropProxMine mineController;

    bool canDash;

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
        DecideSpecial();
        canDash = true;
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
                //if (numberOfAbsorbProjectiles > 0)
                //{
                //    FireAbsorbProjectile();
                //}
                //else if (numberOfAllyProjectiles > 0)
                //{
                //    FireAllyProjectile();
                //}
                //else
                //{
                    FireProjectile();
                //}
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseSpecial();
        }
    }

    public void DecideSpecial()
    {
        int rand = Random.Range(0, 5);
        if (rand == 0)
        {
            currentSpecial = Special.absorb;
            rope.SetActive(false);
            mace.SetActive(false);
            mineController.enabled = false;
            canDash = false;
            numberOfAbsorbProjectiles += 3;
            HUD.Instance.specialText.SetText("+3 Absorb Health Projectiles");
        }
        else if (rand == 1)
        {
            currentSpecial = Special.ally;
            rope.SetActive(false);
            mace.SetActive(false);
            mineController.enabled = false;
            canDash = false;
            numberOfAllyProjectiles ++;
            HUD.Instance.specialText.SetText("+1 Convert To Ally Projectile");
        }
        else if (rand == 2)
        {
            currentSpecial = Special.dash;
            rope.SetActive(false);
            mace.SetActive(false);
            mineController.enabled = false;
            canDash = true;
            HUD.Instance.specialText.SetText("Dash");
        }
        else if (rand == 3)
        {
            currentSpecial = Special.mines;
            rope.SetActive(false);
            mace.SetActive(false);
            mineController.enabled = true;
            canDash = false;
            HUD.Instance.specialText.SetText("+5 Mines");
            numberOfMines += 5;
        }
        else if (rand == 4)
        {
            currentSpecial = Special.mace;
            rope.SetActive(true);
            mace.SetActive(true);
            mineController.enabled = false;
            canDash = false;
            HUD.Instance.specialText.SetText("Mace");
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
            FireAllyProjectile();
        }
        else if (currentSpecial == Special.dash)
        {
            if (canDash)
            {
                StartCoroutine(DashCo());
            }
        }
        else if (currentSpecial == Special.mines && numberOfMines > 0)
        {
            mineController.DropMine();
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

    private IEnumerator DashCo()
    {
        canDash = false;
        canTakeDamage = false;
        float initialSpeed = maxSpeed;
        float initialAcceleration = acceleration;
        maxSpeed *= 10;
        acceleration *= 10;
        yield return new WaitForSeconds(0.5f);
        maxSpeed = initialSpeed;
        acceleration = initialAcceleration;
        canDash = true;
        canTakeDamage = true;
    }
}
