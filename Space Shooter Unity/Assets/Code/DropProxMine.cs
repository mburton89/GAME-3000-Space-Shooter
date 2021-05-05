using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class DropProxMine : Ship
{
    public Projectile proxMinePrefab;
    public int minesLeftCounter;
    public Transform mineSpawnPoint;
    public AudioSource explodeSound;
    public Animation explodeAnimation;

    public TextMeshProUGUI mineWarningText;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Projectile proxMine = Instantiate(proxMinePrefab, mineSpawnPoint.position, transform.rotation)as Projectile;
            proxMine.Init(GetComponent<PlayerShip>().gameObject);
        }

        if (minesLeftCounter == 0)
        {
            mineWarningText.SetText("OUT OF MINES! RESTOCK!");
        }
        else
        {
            minesLeftCounter--;
            mineWarningText.SetText("Mines left: " + minesLeftCounter);
        }

    }


    public void MineTrigger(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyShip>())
        {
            explodeSound.Play();
            explodeAnimation.Play();
            Explode();
            collision.gameObject.GetComponent<EnemyShip>().TakeDamage(1);
        }
    }

}
