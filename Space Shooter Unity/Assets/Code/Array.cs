using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Array : MonoBehaviour
{

    public GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {




    }

    public void chooseRandomShip()
    {
        int rand = Random.Range(0, players.Length);

        GameObject newship = players[rand];

    }



}
