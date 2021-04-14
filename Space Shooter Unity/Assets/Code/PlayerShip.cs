using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    
    void Update()
    {
        FollowMouse();
        HandleUserInput();
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * maxSpeed * Time.deltaTime;
            Thrust();
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * maxSpeed * Time.deltaTime;
            Thrust();
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * maxSpeed * Time.deltaTime;
            Thrust();
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * maxSpeed * Time.deltaTime;
            Thrust();
        }
    }

    void HandleUserInput()
    {
       

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            FireProjectile();
        }
    }

    void FollowMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10)); //Finds Mouse Position on Screen
        Vector2 directionToFace = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y); //creates direction based on positon of ship and mouse cursor 
        transform.up = directionToFace; //Faces Mouse. Assigns transform.up the Direction to Face
    }
    
}
