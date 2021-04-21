﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    void Update()
    {
        FollowMouse();
        HandleUserInput();
        ChargedShot();
        float x;
        x = chargePower/120;
        this.GetComponent<SpriteRenderer>().color = new Color(x,1,1);
    }

    void HandleUserInput()
    {
        if (Input.GetMouseButton(1) && !leCharge)
        {
            Thrust();
        }



        if (Input.GetMouseButton(0))
        {
            if (canShoot)
            {
            leCharge = true;
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
