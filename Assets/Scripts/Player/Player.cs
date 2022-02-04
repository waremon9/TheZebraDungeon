using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    public PlayerMovement playerMovement
    {
        get
        {
            if (_playerMovement) return _playerMovement;
            _playerMovement = GetComponent<PlayerMovement>();
            return _playerMovement;
        }
    }

    private PlayerShoot _playerShoot;
    public PlayerShoot playerShoot
    {
        get
        {
            if (_playerShoot) return _playerShoot;
            _playerShoot = GetComponent<PlayerShoot>();
            return _playerShoot;
        }
    }

}
