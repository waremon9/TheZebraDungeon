using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    private float speed;
    private Vector2 direction;

    private bool fromPlayer;

    private Rigidbody2D rb;

    public ParticleSystem explosionParticle;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(int dmg, float s, Vector2 dir, bool player)
    {
        damage = dmg;
        speed = s;
        direction = dir;
        fromPlayer = player;
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //not shot by player and hit him
        if (!fromPlayer && other.gameObject == GameManager.Instance.player.gameObject)
        {
            Debug.Log("hit player");
            DestroyBullet();
            return;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Instantiate(explosionParticle, transform.position, quaternion.identity, GameManager.Instance.particleParent);
        Destroy(gameObject);
    }
}
