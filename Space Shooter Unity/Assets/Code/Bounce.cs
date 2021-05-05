using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ship>())
        {
            BounceShip(collision.gameObject.GetComponent<Ship>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ship>())
        {
            BounceShip(collision.gameObject.GetComponent<Ship>());
        }
    }

    void BounceShip(Ship shipToBounce)
    {
        print("bounce ship");
        shipToBounce.transform.position = shipToBounce.previousPosition;
        shipToBounce.rigidBody2D.velocity = new Vector2(-shipToBounce.rigidBody2D.velocity.x * 2, -shipToBounce.rigidBody2D.velocity.y * 2);

        print(shipToBounce.rigidBody2D.velocity);
        shipToBounce.TakeDamage(1);
    }
}
