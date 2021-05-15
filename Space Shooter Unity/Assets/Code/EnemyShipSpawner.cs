using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    public static EnemyShipSpawner Instance;
    public List<EnemyShip> enemyShipPrefabs;

    public Transform SpawnPivot;
    public Transform SpawnPoint;

    [HideInInspector] public int startingNumberOfShips;
    [HideInInspector] public int currentWave;


    private void Awake()
    {
        Instance = this;
        currentWave = 1;
        startingNumberOfShips = FindObjectsOfType<EnemyShip>().Length;
    }

    public void CountEnemyShips()
    {
        StartCoroutine(CountEnemyShipsCo());
    }

    private IEnumerator CountEnemyShipsCo()
    {
        yield return new WaitForSeconds(0.2f);

        int currentShips = FindObjectsOfType<EnemyShip>().Length;

        print("currentShips: " + currentShips);

        if (currentShips <= 0)
        {
            currentWave++;
            HUD.Instance.UpdateWaveText(currentWave);
            SpawnWaveOfShips();
        }
    }

    void SpawnWaveOfShips()
    {
        int enemyShipsToSpawn = startingNumberOfShips + currentWave;

        for (int i = 0; i < enemyShipsToSpawn; i++)
        {
            int rand = Random.Range(0, enemyShipPrefabs.Count);

            //Determine Random Position Off the screen
            float zRotation = Random.Range(0, 360);
            SpawnPivot.eulerAngles = new Vector3(0, 0, zRotation);

            Instantiate(enemyShipPrefabs[rand], SpawnPoint.position, transform.rotation, null);
        }

        if (FindObjectOfType<PlayerShip>() != null)
        {
            FindObjectOfType<PlayerShip>().DecideSpecial();
        }
    }
}
