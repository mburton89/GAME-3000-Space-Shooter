using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailModifier : MonoBehaviour
{
    public Transform trailCollisionSpawnPoint;
    public TrailCollision trailCollisionPrefab;

    void Update()
    {
        TrailCollision trailColl = Instantiate(trailCollisionPrefab, trailCollisionSpawnPoint.position, transform.rotation) as TrailCollision;
        Instantiate(trailCollisionPrefab, trailCollisionSpawnPoint.position, transform.rotation);
    }
}
