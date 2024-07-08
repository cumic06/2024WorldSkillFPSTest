using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineData : MonoBehaviour
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