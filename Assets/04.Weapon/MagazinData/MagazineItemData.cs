using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagazineItemData", menuName = "ItemData/MagazineItemData")]
public class MagazineItemData : ItemData
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