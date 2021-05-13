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
    private bool canResetMines;

    public TextMeshProUGUI mineWarningText;

    
    private void Awake()
    {
        canDrop = true;
        canResetMines = true;
        mineWarningText.SetText("Mines left: " + minesLeftCounter);
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDrop == true)
        {
            Projectile proxMine = Instantiate(proxMinePrefab, mineSpawnPoint.position, transform.rotation)as Projectile;
            proxMine.Init(GetComponent<PlayerShip>().gameObject);

            Vector3 shipVelocity = new Vector3();
            shipVelocity = FindObjectOfType<PlayerShip>().rigidBody2D.velocity;
            proxMine.rigidBody2D.velocity = shipVelocity / 3;

            minesLeftCounter--;
            mineWarningText.SetText("Mines left: " + minesLeftCounter);
        }

        if (minesLeftCounter == 0 && canResetMines)
        {
            canResetMines = false;
            StartCoroutine(MineDropWait());
            mineWarningText.SetText("OUT OF MINES! RESTOCK!");
        }
    }

    private IEnumerator MineDropWait()
    {
        canDrop = false;
        yield return new WaitForSeconds(mineWaitTime);
        canDrop = true;
        minesLeftCounter = 5;

        if (minesLeftCounter > 0)
        {
            mineWarningText.SetText("Mines left: " + minesLeftCounter);
        }

        canResetMines = true;
    }
}
