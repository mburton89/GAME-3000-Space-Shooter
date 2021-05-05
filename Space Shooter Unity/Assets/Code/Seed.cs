using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public List<EnemyShip> enemyShipPrefabs;
    public Transform seedpoint;
    public float countup;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y);
        float x;
        float y;
        countup++;
        
        x = (countup/300);
        y = 1 - (countup/300);
        this.GetComponent<SpriteRenderer>().color = new Color(x, y, 0);
        //Instantiate(Resources.Load("ShipExplosion"), transform.position, transform.rotation);
        if (countup >= 300)
        {
            int rand = Random.Range(0, enemyShipPrefabs.Count);
            Instantiate(enemyShipPrefabs[rand], spawnPosition, transform.rotation, null);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame

}
