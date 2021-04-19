using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailAttack : MonoBehaviour
{

    [HideInInspector] public float scaleX;
    [HideInInspector] public float scaleY;

    private void Start()
    {
        Destroy(gameObject, 1);    
    }

    public void changeScale(double timeSinceInitHalf)
    {
        gameObject.transform.localScale = new Vector3((float)(timeSinceInitHalf * 3.5), (float)(timeSinceInitHalf * 3.5), 1);
    }

}
