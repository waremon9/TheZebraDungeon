using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MySingleton<GameManager>
{
    public override bool DoDestroyOnLoad { get; }

    [HideInInspector] public Player player;
    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        OnGameStart();
    }

    private void OnGameStart()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity).GetComponent<Player>();
        CameraMovement.Instance.SetPlayerRigidBody(player.playerMovement.rigidbody2D);
    }
}
