using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
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
            FireProjectile();
        }
    }

    void FollowMouse()
    {
        Vector3 perspectiveMousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(perspectiveMousePos); //Finds Mouse Position on Screen
        Vector2 directionToFace = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y); //creates direction based on positon of ship and mouse cursor 
        transform.up = directionToFace; //Faces Mouse. Assigns transform.up the Direction to Face
    }
}
