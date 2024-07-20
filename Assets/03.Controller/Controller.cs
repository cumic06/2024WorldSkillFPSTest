using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int damageValue);
}

public class Controller : MonoBehaviour, IDamageable
{
    public Health health;
    public Weapon currentWeapon;
    public float moveSpeed;
    public LayerMask enemyLayerMask;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        health.currentHp = health.maxHp;
    }

    protected virtual void FixedUpdate()
    {

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
        currentWeapon.Reload();
        Debug.Log("Reload");
    }

    public void TakeDamage(int damageValue)
    {

    }
}