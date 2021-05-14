using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailModifier : MonoBehaviour
{
    public Transform trailCollisionSpawnPoint;
    public TrailCollision trailCollisionPrefab;
    public bool speedBuffer;

    private void Start()
    {
        speedBuffer = true;
        InvokeRepeating("CreateTrail", 0, 0.01f);
    }

    void Update()
    {

    }

    void CreateTrail()
    {
        if (GetComponent<PlayerShip2>().rigidBody2D.velocity.magnitude >= GetComponent<PlayerShip2>().maxSpeed / 5)
        {
            speedBuffer = false;
        }

        else
        {
            speedBuffer = true;
        }

        if (!speedBuffer && GetComponent<PlayerShip2>().ringOfFire)
        {
            TrailCollision trailColl = Instantiate(trailCollisionPrefab, trailCollisionSpawnPoint.position, transform.rotation) as TrailCollision;
            Instantiate(trailCollisionPrefab, trailCollisionSpawnPoint.position, transform.rotation);
        }
    }
}
