using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform spawnPoint;
    
    private float lastShoot = 0;
    public Weapon currentWeapon;

    private void Update()
    {
        if(Input.GetButton("GameShoot1")) Shoot();
    }

    private void Shoot()
    {
        if (lastShoot + currentWeapon.reloadSpeed <= Time.time)
        {
            lastShoot = Time.time;
            currentWeapon.Shoot(spawnPoint.position, transform.up, true);
        }
    }
}
