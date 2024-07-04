using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MagazinType
{
    Assult,
    Sniper,

}

public class Weapon : MonoBehaviour
{
    public float shootRange;
    public MagazinType magazinType;
    public int currentBullet;

    public void SetMagazine()
    {

    }

    public void GetCurrentBullet()
    {

    }

    public virtual void SpawnBullet()
    {

    }
}
