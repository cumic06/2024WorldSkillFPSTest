using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChangeHpable
{
    public int ChangeHp(int value);
    public void OnDead();
}

public interface IDamageable
{
    public void TakeDamage(int damage);
}


public interface IHealable
{
    public void TakeHeal(int heal);
}