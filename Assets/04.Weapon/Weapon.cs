using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Pistol,
    Assult,
    Sniper,
    ShotGun
}

public class Weapon : MonoBehaviour
{
    public float shootRange;
    public BulletType bulletType;
    public int currentBullet;
    public float attackDelay;
    public float currentAttackDelay;
    public Transform bulletPos;
    public WeaponMagazineData weaponMagazineData;

    public void Shoot()
    {
        DisCountBullet();
    }

    public virtual void SpawnBullet()
    {
        Debug.Log("SpawnBullet");
        GameObject spawnBullet = Instantiate(weaponMagazineData.GetBulletPrefab(), bulletPos.position, bulletPos.rotation);

        if (bulletType == BulletType.ShotGun)
        {
            spawnBullet.transform.rotation = bulletPos.rotation * Quaternion.Euler(Random.insideUnitCircle * 15);
        }

        spawnBullet.TryGetComponent(out Bullet bullet);
        bullet.DestroyRange = shootRange;
    }

    public BulletType GetMagazineType()
    {
        return bulletType;
    }

    public int GetMaxBullet()
    {
        return weaponMagazineData.GetMaxBulletCount();
    }

    public int GetCurrentBullet()
    {
        return currentBullet;
    }

    public void DisCountBullet()
    {
        if (currentBullet <= 0)
        {
            Reload();
            return;
        }
        currentBullet--;

        if (bulletType == BulletType.ShotGun)
        {
            for (int i = 0; i < 15; i++)
            {
                SpawnBullet();
            }
        }
        else
        {
            SpawnBullet();
        }
    }

    public void Reload()
    {
        currentBullet = weaponMagazineData.GetMaxBulletCount();
    }
}