using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{

    public Transform tankTopPivot;

    public float xDirectionToFace;
    public float yDirectionToFace;
    public Vector3 DirectionToFace;

    void Update()
    {
        FollowMouse();
        HandleUserInput();
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * maxSpeed * Time.deltaTime;
            transform.up = Vector3.left; //Faces Mouse. Assigns transform.up the Direction to Face
            //Thrust();
            xDirectionToFace = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * maxSpeed * Time.deltaTime;
            transform.up = Vector3.right;
            //Thrust();
            xDirectionToFace = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * maxSpeed * Time.deltaTime;
            transform.up = Vector3.up;
            //Thrust();
            yDirectionToFace = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * maxSpeed * Time.deltaTime;
            transform.up = Vector3.down;
            //Thrust();
            yDirectionToFace = -1;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            xDirectionToFace = 0;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            xDirectionToFace = 0;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            yDirectionToFace = 0;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            yDirectionToFace = 0;
        }

        DirectionToFace = new Vector3(xDirectionToFace, yDirectionToFace, 0);
        if (DirectionToFace != Vector3.zero)
        {
            transform.up = DirectionToFace;
        }
    }

    void HandleUserInput()
    {
       

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            FireProjectile(tankTopPivot);
        }
    }

    void FollowMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10)); //Finds Mouse Position on Screen
        Vector2 directionToFace = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y); //creates direction based on positon of ship and mouse cursor 
        tankTopPivot.up = directionToFace; //Faces Mouse. Assigns transform.up the Direction to Face
        //transform.up = directionToFace;
    }
    
}
