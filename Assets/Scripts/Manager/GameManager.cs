using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MySingleton<GameManager>
{
    public override bool DoDestroyOnLoad { get; }

    [HideInInspector] public Player player;

    [Header("Player")]
    [SerializeField] private GameObject playerPrefab;

    [Header("Parents")]
    public Transform particleParent;

    private void Start()
    {
        OnGameStart();
    }
    private void OnGameStart()
    {
        DungeonManager.Instance.GenerateDungeon();
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, DungeonManager.Instance.GetPlayerSpawn(), Quaternion.identity).GetComponent<Player>();
        CameraMovement.Instance.SetPlayerRigidBody(player.playerMovement.rb);
        CameraMovement.Instance.transform.position = player.transform.position;
    }
}
