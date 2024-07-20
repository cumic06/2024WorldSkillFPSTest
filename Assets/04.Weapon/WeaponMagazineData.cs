using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponMagazineData
{
    public BulletType magazineType;
    public MagazineData magazineData;
    public GameObject bulletPrefab;

    public BulletType GetMagazineType()
    {
        return magazineType;
    }

    public int GetMaxBulletCount()
    {
        return magazineData.GetMaxBulletData();
    }

    public GameObject GetBulletPrefab()
    {
        return bulletPrefab;
    }
}