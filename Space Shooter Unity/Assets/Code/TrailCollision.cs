using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCollision : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public TrailCollision trailCollisionPrefab;
    [HideInInspector] public Vector3 attackSpawn;
    public TrailAttack trailAttackPrefab;
    [HideInInspector] public double initializationTime;
    [HideInInspector] public double timeSinceInitialization;
    [HideInInspector] public double halfTimeSinceInitialization;

    private void Start()
    {
        initializationTime = Time.timeSinceLevelLoad;
        Destroy(gameObject, 2);
    }

    private void Update()
    {
        timeSinceInitialization = Time.timeSinceLevelLoad - initializationTime;
        halfTimeSinceInitialization = timeSinceInitialization / 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerShip>() && (timeSinceInitialization > 0.5) && collision.GetComponent<PlayerShip>().canCollideWithTrail)
        {
            collision.GetComponent<PlayerShip>().canCollideWithTrail = false;
            TrailCollision[] allTrailCollisions = FindObjectsOfType<TrailCollision>();
            print("amount " + allTrailCollisions.Length);
            foreach (TrailCollision trailCollision in allTrailCollisions)
            {
                trailCollision.findPointB(this.gameObject, timeSinceInitialization, halfTimeSinceInitialization, gameObject.transform.position);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerShip>())
        {
            collision.GetComponent<PlayerShip>().canCollideWithTrail = true;
        }
    }

    public void findPointB(GameObject ColliderA, double timeSinceInit, double timeSinceInitHalf, Vector3 pointA)
    {
        if ((timeSinceInitialization - timeSinceInitHalf) <= 0.0025 && (timeSinceInitialization - timeSinceInitHalf) >= 0)
        {
            print(timeSinceInitialization - timeSinceInitHalf);
            createTrailAttack(ColliderA, this.gameObject, timeSinceInit, timeSinceInitHalf, pointA, gameObject.transform.position);
            StartCoroutine(GetComponent<PlayerShip>().TrailAttackBuffer());
        }
    }

    public void createTrailAttack(GameObject ColliderA, GameObject ColliderB, double timeSinceInit, double timeSinceInitHalf, Vector3 pointA, Vector3 pointB)
    {
        attackSpawn = new Vector3(pointA.x + ((pointB.x - pointA.x) / 2), pointA.y + ((pointB.y - pointA.y) / 2));
        print("pointA" + pointA);
        print("pointB" + pointB);   
        print("attackSpawn" + attackSpawn);
        TrailAttack attack = Instantiate(trailAttackPrefab, attackSpawn, transform.rotation) as TrailAttack;
        Instantiate(trailAttackPrefab, attackSpawn, transform.rotation);
        GetComponent<TrailAttack>().changeScale(pointA, pointB);
    }
}