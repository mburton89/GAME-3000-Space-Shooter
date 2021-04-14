using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailModifier : MonoBehaviour
{
    public Transform trailCollisionSpawnPoint;
    public TrailCollision trailCollisionPrefab;

    private void Start()
    {
        InvokeRepeating("CreateTrail", 0, 0.01f);
    }

    void Update()
    {

    }

    void CreateTrail()
    {

        TrailCollision trailColl = Instantiate(trailCollisionPrefab, trailCollisionSpawnPoint.position, transform.rotation) as TrailCollision;
        Instantiate(trailCollisionPrefab, trailCollisionSpawnPoint.position, transform.rotation);
    }
}
