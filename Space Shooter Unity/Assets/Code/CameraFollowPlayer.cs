using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public static CameraFollowPlayer Instance;

    [HideInInspector] public Transform player;

    void Start()
    {
        Instance = this;
        FindPlayer();
    }

    public void FindPlayer()
    {
        player = FindObjectOfType<PlayerShip>().transform;
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y, -10);
        }
    }
}
