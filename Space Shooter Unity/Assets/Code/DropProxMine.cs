using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class DropProxMine : MonoBehaviour
{
    public Projectile proxMinePrefab;
    public int minesLeftCounter;
    public int mineWaitTime;
    public Transform mineSpawnPoint;
    public bool canDrop;

    public TextMeshProUGUI mineWarningText;

    private void Awake()
    {
        minesLeftCounter = 5;
        canDrop = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDrop == true)
        {
            Projectile proxMine = Instantiate(proxMinePrefab, mineSpawnPoint.position, transform.rotation)as Projectile;
            proxMine.Init(GetComponent<PlayerShip>().gameObject);
            minesLeftCounter--;
        }

        if (minesLeftCounter == 0)
        {
            StartCoroutine(MineDropWait());
            mineWarningText.SetText("OUT OF MINES! RESTOCK!");
        }
        else if (minesLeftCounter > 0)
        {
            mineWarningText.SetText("Mines left: " + minesLeftCounter);
        }

    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    MineTrigger(collision);
    //}

    private IEnumerator MineDropWait()
    {
        canDrop = false;
        yield return new WaitForSeconds(mineWaitTime);
        canDrop = true;
        minesLeftCounter = 5;
    }


    //public void MineTrigger(Collider2D collision)
    //{
    //    if (collision.gameObject.GetComponent<EnemyShip>())
    //    {
    //        MineExplode();
    //        collision.gameObject.GetComponent<EnemyShip>().TakeDamage(2);
    //    }
    //}


    //public void MineExplode()
    //{
    //    print("hello");
    //    Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation);
    //    ScreenShakeManager.Instance.ShakeScreen();
    //    EnemyShipSpawner.Instance.CountEnemyShips();

    //    if (GetComponent<EnemyShip>())
    //    {
    //        GameManager.Instance.HandlePlayerDestroyed();
    //    }

    //    Destroy(gameObject);
    //}
}
