using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon", fileName = "Weapon")]
public class Weapon : ScriptableObject
{
    public int bulletDamage;
    public float bulletSpeed;
    public float reloadSpeed;
    public int bulletPerShot;
    public int spread;

    public void Shoot(Vector3 spawnPoint, Vector3 direction, bool fromplayer)
    {
        Bullet bullet = Instantiate(BulletManager.Instance.bulletPrefab, spawnPoint, Quaternion.identity, BulletManager.Instance.bulletParent);
        bullet.Init(bulletDamage, bulletSpeed, direction, fromplayer);
    }
}
