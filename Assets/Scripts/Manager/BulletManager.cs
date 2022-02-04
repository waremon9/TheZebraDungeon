using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MySingleton<BulletManager>
{
    public override bool DoDestroyOnLoad
    {
        get { return true; }
    }

    [Header("Bullet")]
    public Transform bulletParent;
    public Bullet bulletPrefab;
}
