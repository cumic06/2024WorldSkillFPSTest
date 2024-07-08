using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Health health;
    public Weapon currentWeapon;
    public float moveSpeed;

    protected virtual void Awake()
    {

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

    protected virtual void Reload()
    {
        currentWeapon.SetMagazine();
    }
}