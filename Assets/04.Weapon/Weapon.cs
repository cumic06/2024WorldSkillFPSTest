using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Pistol,
    Assult,
    Sniper,
    ShotGun
}

public class Weapon : MonoBehaviour
{
    public float shootRange;
    public WeaponType weaponType;
    public int currentBullet;
    public float attackDelay;
    public float currentAttackDelay;
    public Transform bulletPos;
    public WeaponMagazineData weaponMagazineData;

    private void Start()
    {
        currentBullet = weaponMagazineData.GetMaxBulletCount();
    }

    public void Shoot()
    {
        DisCountBullet();
    }

    public virtual void SpawnBullet()
    {
        GameObject spawnBullet = Instantiate(weaponMagazineData.GetBulletPrefab(), bulletPos.position, bulletPos.rotation);

        if (weaponType == WeaponType.ShotGun)
        {
            spawnBullet.transform.rotation = bulletPos.rotation * Quaternion.Euler(Random.insideUnitCircle * 15);
        }

        spawnBullet.TryGetComponent(out Bullet bullet);
        bullet.DestroyRange = shootRange;
    }

    public WeaponType GetWeaponType()
    {
        return weaponType;
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
        if (IsBulletZero()) return;

        currentBullet--;

        if (weaponType == WeaponType.ShotGun)
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

    public bool IsBulletZero()
    {
        return currentBullet <= 0;
    }

    public void Reload()
    {
        currentBullet = weaponMagazineData.GetMaxBulletCount();
    }
}