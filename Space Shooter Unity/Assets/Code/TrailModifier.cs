using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailModifier : MonoBehaviour
{
    public TrailAttack trailAttackPrefab;
    public Transform trailCollisionSpawnPoint;
    public TrailCollision trailCollisionPrefab;
    [HideInInspector] public Vector3 attackSpawn;

    void Update()
    {
        TrailCollision trailColl = Instantiate(trailCollisionPrefab, trailCollisionSpawnPoint.position, transform.rotation) as TrailCollision;
        Instantiate(trailCollisionPrefab, trailCollisionSpawnPoint.position, transform.rotation);
    }

    public void sendBackParameter(GameObject ColliderA, double timeSinceInitializationA, double halfTimeSinceInitialization, Vector3 pointA)
    {
        GetComponent<TrailCollision>().returnSelf(ColliderA, timeSinceInitializationA, halfTimeSinceInitialization, pointA);
    }

    public void createTrailAttack(GameObject ColliderA, GameObject ColliderB, double timeSinceInit, double timeSinceInitHalf, Vector3 pointA, Vector3 pointB)
    {
        attackSpawn = new Vector3(pointA.x - pointB.x, pointA.y - pointB.y);

        TrailAttack attack = Instantiate(trailAttackPrefab, attackSpawn, transform.rotation) as TrailAttack;
        Instantiate(trailAttackPrefab, attackSpawn, transform.rotation);
    }
}
