using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagazinData", menuName = "ItemData/MagazinData")]
public class MagazineData : ItemData
{
    public int bulletDamageData;
    public int maxBulletData;

    public int GetMaxBulletData()
    {
        return maxBulletData;
    }

    public int GetBulletDamageData()
    {
        return bulletDamageData;
    }
}