using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship

{
    [HideInInspector] public bool canCollideWithTrail;
    [HideInInspector] public bool canTrailAttack;
    public bool ringOfFire;


    private void Awake()
    {
        canCollideWithTrail = true;
        canTrailAttack = true;
        currentArmor = maxArmor;
    }
    void Update()
    {
        FollowMouse();
        HandleUserInput();
        
        if (ringOfFire)
        {
            this.canShoot = false;
            this.maxSpeed = 8;
        }
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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10)); //Finds Mouse Position on Screen
        Vector2 directionToFace = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y); //creates direction based on positon of ship and mouse cursor 
        transform.up = directionToFace; //Faces Mouse. Assigns transform.up the Direction to Face
    }

    public void startBuffer()
    {
        StartCoroutine(TrailAttackBuffer());
    }

    private IEnumerator TrailAttackBuffer()
    {
        canTrailAttack = false;
        yield return new WaitForSeconds(2);
        canTrailAttack = true;
    }
}
 