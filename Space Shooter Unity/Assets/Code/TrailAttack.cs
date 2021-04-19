using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailAttack : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, 1);    
    }

    public void changeScale(Vector3 pointA, Vector3 pointB)
    {
        Vector3 size = (pointA - pointB);
        gameObject.transform.localScale = size;
    }

}
