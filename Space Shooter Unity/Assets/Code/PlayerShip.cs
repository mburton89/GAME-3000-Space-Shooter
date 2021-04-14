using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{

    public bool hasBulletLimit; //on/off switch for the bullet limit challenege.

    void Start()
    {
        currentAmmo = 0;
        HUD.Instance.UpdateAmmoCountText(currentAmmo, maxAmmo);
    }

    void Update()
    {
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
            else
            {
                FireProjectile();
            }
        }
    }

    void FollowMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10)); //Finds Mouse Position on Screen
        Vector2 directionToFace = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y); //creates direction based on positon of ship and mouse cursor 
        transform.up = directionToFace; //Faces Mouse. Assigns transform.up the Direction to Face
    }
}
