using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MagazinType
{
    Pistol,
    Assult,
    Sniper,
    ShotGun
}

public class Weapon : MonoBehaviour
{
    public float shootRange;
    public MagazinType magazinType;
    public int currentBullet;

    public void SetMagazine()
    {
        switch (magazinType)
        {
            case MagazinType.Pistol:

                break;
            case MagazinType.Assult:
                break;
            case MagazinType.Sniper:
                break;
        }
    }

    public virtual void SpawnBullet()
    {

    }
    public int GetCurrentBullet()
    {
        return currentBullet;
    }
}
