using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailAttack : MonoBehaviour
{
    [HideInInspector] public float scaleX;
    [HideInInspector] public float scaleY;
    public Rigidbody2D rigidBody2D;
    public AudioClip explosionSound;
    [HideInInspector] public bool canSpawnCD = true;

    private void Start()
    {
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        canSpawnCD = true;

        Destroy(gameObject, 1);
    }

    public void changeScale(double timeSinceInitHalf)
    {
        gameObject.transform.localScale = new Vector3((float)(timeSinceInitHalf * 2), (float)(timeSinceInitHalf * 2), 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HEnemyShip>())
        {
            //collision.GetComponent<HEnemyShip>().canShake = false;
            collision.GetComponent<HEnemyShip>().Explode();
        }
    }

}
