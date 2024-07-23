using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponMagazineData
{
    public WeaponType magazineType;
    public MagazineItemData magazineData;
    public GameObject bulletPrefab;

    public WeaponType GetMagazineType()
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