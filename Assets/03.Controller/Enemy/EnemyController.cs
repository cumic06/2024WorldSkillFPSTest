using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : AIController
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (CheckAttackRange())
        {
            if (IsCanAttack())
            {
                Attack();
            }
            currentWeapon.currentAttackDelay += Time.deltaTime;
        }
        else
        {
            FollowEnemy();
        }
    }
}