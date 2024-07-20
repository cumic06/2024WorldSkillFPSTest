using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    protected override void FixedUpdate()
    {

    }

    protected bool CheckAttackRange()
    {
        Collider[] checkAttackRange = Physics.OverlapSphere(transform.position, currentWeapon.shootRange, enemyLayerMask);
        return checkAttackRange.Length > 0;
    }

    protected bool CheckObstacle()
    {
        return false;
    }

    protected void FollowEnemy()
    {

    }
}
