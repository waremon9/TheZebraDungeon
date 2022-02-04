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

}
