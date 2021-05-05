using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public Vector3 pointB;

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

    IEnumerator Start()
    {
        var pointA = transform.position;
        while(true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
        }
    } 

    IEnumerator MoveObject (Transform thisTranform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
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
