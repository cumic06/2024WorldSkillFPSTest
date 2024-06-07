using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public abstract class Unit : MonoBehaviour, IChangeHpable, IDamageable
{
    #region Veriable
    [SerializeField] protected UnitStat unitStat;
    protected Rigidbody rigid;
    public Rigidbody Rigid => rigid;
    #endregion

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        ResetAllStats();
    }

    public UnitStat GetUnitStat()
    {
        return unitStat;
    }

    #region ResetStat
    public void ResetAllStats()
    {
        ResetHp();
        ResetMoveSpeed();
    }

    public void ResetHp()
    {
        unitStat.currentHp = unitStat.maxHp;
    }

    public void ResetMoveSpeed()
    {
        unitStat.currentMoveSpeed = unitStat.maxMoveSpeed;
    }
    #endregion

    #region Hp
    public virtual void TakeDamage(int damage)
    {
        unitStat.currentHp = ChangeHp(-damage);
    }

    public int ChangeHp(int value)
    {
        int changeHpValue = unitStat.currentHp + value;

        if (changeHpValue >= unitStat.maxHp)
        {
            return unitStat.maxHp;
        }
        else if (changeHpValue <= 0)
        {
            return 0;
        }
        return changeHpValue;
    }

    public abstract void OnDead();
    #endregion
}
