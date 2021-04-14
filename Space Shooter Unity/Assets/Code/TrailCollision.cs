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
    [HideInInspector] public bool triggerCheck = false;
    
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
            //Vector3 position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
            //GameObject ColliderA = this.gameObject;

            triggerCheck = true;

            TrailCollision[] allTrailCollisions = FindObjectsOfType<TrailCollision>();
            print("amount " + allTrailCollisions.Length);
            foreach (TrailCollision trailCollision in allTrailCollisions)
            {
                trailCollision.findPointB(this.gameObject, timeSinceInitialization, halfTimeSinceInitialization, gameObject.transform.position);
            }

            Destroy(gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerShip>())
        {
            //triggerCheck = false;
            collision.GetComponent<PlayerShip>().canCollideWithTrail = true;
        }
    }

    public void findPointB(GameObject ColliderA, double timeSinceInit, double timeSinceInitHalf, Vector3 pointA)
    {
        if ((timeSinceInitialization - timeSinceInitHalf) <= 0.00000000000001)
        {
            print("If statement passed");    
            
            
            //Vector3 position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
            //GameObject ColliderB = this.gameObject;

            //createTrailAttack(ColliderA, this.gameObject, timeSinceInit, timeSinceInitHalf, pointA, gameObject.transform.position);
        }
    }

    public void createTrailAttack(GameObject ColliderA, GameObject ColliderB, double timeSinceInit, double timeSinceInitHalf, Vector3 pointA, Vector3 pointB)
    {
        attackSpawn = new Vector3(pointA.x - pointB.x, pointA.y - pointB.y);

        //print(ColliderA);
        //print(ColliderB);
        //print(timeSinceInit);
        //print(timeSinceInitHalf);
        //print(pointA);
        //print(pointB);
        //print("Everything passed");


        TrailAttack attack = Instantiate(trailAttackPrefab, attackSpawn, transform.rotation) as TrailAttack;
        Instantiate(trailAttackPrefab, attackSpawn, transform.rotation);
    }
}