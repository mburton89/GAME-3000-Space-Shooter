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
    void Dash()
    {
        StartCoroutine(DashCo());;
    }

    private IEnumerator DashCo()
    {
        
        canTakeDamage = false;
        acceleration = acceleration * 4;
        maxSpeed = maxSpeed * 4;
        yield return new WaitForSeconds(0.8f);
        canTakeDamage = true;
        acceleration = acceleration / 4;
        maxSpeed = maxSpeed / 4;
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        Vector3 spawnPosition = new Vector3(DashSpawn.position.x + randomX, DashSpawn.position.y + randomY);


    }

    void FollowMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10)); //Finds Mouse Position on Screen
        Vector2 directionToFace = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y); //creates direction based on positon of ship and mouse cursor 
        transform.up = directionToFace; //Faces Mouse. Assigns transform.up the Direction to Face
    }







}
