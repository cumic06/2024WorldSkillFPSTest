using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int damageValue);
}

public class Controller : MonoBehaviour, IDamageable
{
    #region Weapon
    public Weapon currentWeapon;
    public WeaponMagazineInventory weaponMagazineInventory;
    #endregion

    #region Stat
    public Health health;
    public float moveSpeed;
    public float currentMoveSpeed;
    #endregion

    [Header("MiniMap")]
    public GameObject miniMapImage;
    protected GameObject miniMapSpawnImage;
    public Transform miniMapBackground;

    public LayerMask enemyLayerMask;
    protected Rigidbody rigid;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        currentMoveSpeed = moveSpeed;
        health.currentHp = health.maxHp;

        miniMapSpawnImage = Instantiate(miniMapImage, transform);
    }

    protected virtual void FixedUpdate()
    {
        miniMapSpawnImage.transform.position = transform.position;
    }

    protected virtual void Update()
    {

    }

    protected virtual void Attack()
    {

    }

    protected bool IsCanAttack()
    {
        return currentWeapon.currentAttackDelay >= currentWeapon.attackDelay;
    }

    protected virtual void Reload()
    {
        if (currentWeapon.GetCurrentBullet() == currentWeapon.GetMaxBullet()) return;

        if (weaponMagazineInventory.GetMagazineCount(currentWeapon.GetWeaponType()) <= 0) return;

        currentWeapon.Reload();
        weaponMagazineInventory.RemoveMagazine(currentWeapon.GetWeaponType(), 1);
    }

    public virtual void TakeDamage(int damageValue)
    {
        health.currentHp -= damageValue;
    }
}