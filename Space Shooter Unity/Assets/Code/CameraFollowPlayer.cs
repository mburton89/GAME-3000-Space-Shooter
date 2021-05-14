﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform _player;

    void Start()
    {
        _player = FindObjectOfType<PlayerShip2>().transform;
    }

    void Update()
    {
        if (_player != null)
        {
            transform.position = new Vector3(_player.position.x, _player.position.y, -10);
        }
    }
}
