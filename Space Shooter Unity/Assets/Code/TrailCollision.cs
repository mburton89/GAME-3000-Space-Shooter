using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCollision : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
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
        
        if (collision.GetComponent<PlayerShip>() && (timeSinceInitialization > 0.5))
        {
            Vector3 position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);


            GetComponent<TrailModifier>().sendBackParameter(gameObject, timeSinceInitialization, halfTimeSinceInitialization, position);
        }
    }

    public void returnSelf(GameObject ColliderA, double timeSinceInit, double timeSinceInitHalf, Vector3 pointA)
    {
        if ((timeSinceInitialization - halfTimeSinceInitialization) <= 0.00001)
        {
            Vector3 position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);

            GetComponent<TrailModifier>().createTrailAttack(ColliderA, gameObject, timeSinceInit, timeSinceInitHalf, pointA, position);
        }
    }


}
