using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Array : MonoBehaviour
{
    int totalPlayers = 1;
    public int currentplayerIndex;
    public GameObject[] players;
    public GameObject currentPlayer;
    public GameObject playerHolder;

    // Start is called before the first frame update
    void Start()
    {
        //totalPlayers = playerHolder.transform.childCount;
        //players = new GameObject[totalPlayers];

        //for (int i = 0; i < totalPlayers; i++)
        //{




        //}


        



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
