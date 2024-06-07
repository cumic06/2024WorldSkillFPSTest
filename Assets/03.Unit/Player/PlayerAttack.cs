using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Gun[] weapon;
    [SerializeField] private Gun currentWeapon;

    private readonly KeyCode[] weaponKeys = new KeyCode[3] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };

    private void Awake()
    {
        currentWeapon = GetComponent<Gun>();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentWeapon.ReLoad();
        }
    }
    private void Update()
    {
        for (int i = 0; i < weaponKeys.Length; i++)
        {
            if (Input.GetKeyDown(weaponKeys[i]))
            {
                currentWeapon = weapon[i];
            }
        }
    }

    private void Shoot()
    {
        currentWeapon.Shoot();
    }
}
