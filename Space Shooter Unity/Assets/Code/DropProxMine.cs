using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class DropProxMine : MonoBehaviour
{
    public GameObject proxMinePrefab;
    public float mineTriggerDistance;
    public int minesLeftCounter;
    public Transform mineSpawnPoint;

    public TextMeshProUGUI mineWarningText;


    public void DropMine()
    {
        if (Input.GetButtonDown("space"))
        {
            GameObject proxMine = Instantiate(proxMinePrefab, mineSpawnPoint.position, transform.rotation)as GameObject;
        }

        if(minesLeftCounter == 0)
        {
            mineWarningText.SetText("OUT OF MINES! RESTOCK!");
        }
        else
            minesLeftCounter--;
    }


    public void MineTrigger()
    {
        if()//if player ship distnace is within mineTriggerDistance, then detonate mine
        {

        }

        Explode();
    }
    



    public void Explode()
    {
        Instantiate(Resources.Load("ShipExplosion"), transform.position, transform.rotation);
        //ScreenShaker.Instance.ShakeScreen();
        ScreenShakeManager.Instance.ShakeScreen();
        EnemyShipSpawner.Instance.CountEnemyShips();

        if (GetComponent<PlayerShip>())
        {
            GameManager.Instance.HandlePlayerDestroyed();
        }

        Destroy(gameObject);
    }

}
