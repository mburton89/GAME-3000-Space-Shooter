using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CDManager : MonoBehaviour
{
    public GameObject cdHostPrefab;
    public GameObject trailAttackPrefab;
    public Rigidbody2D rigidBody2D;

    private void Start()
    {

    }

    private void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<TrailAttack>())
        {
            print("Passed");
            trailAttackPrefab.GetComponent<TrailAttack>().canSpawnCD = false;
            GameObject CDVisualInstance = Instantiate(cdHostPrefab, this.transform);
        }
    }
}
