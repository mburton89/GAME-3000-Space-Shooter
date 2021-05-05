using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    public static EnemyShipSpawner Instance;
    public Seed Seeder;
    //public List<EnemyShip> enemyShipPrefabs;

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
        int currentShips = FindObjectsOfType<EnemyShip>().Length + FindObjectsOfType<Seed>().Length;

       // print(currentShips);

        if (currentShips == 1)
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
            float zLength = Random.Range(0,100);
            transform.localScale = new Vector3(zLength/100, zLength/100, 0);
            //Determine Random Position Off the screen
            float zRotation = Random.Range(0, 360);
            SpawnPivot.eulerAngles = new Vector3(0, 0, zRotation);

            Instantiate(Seeder, SpawnPoint.position, transform.rotation, null);
        }
    }

}
