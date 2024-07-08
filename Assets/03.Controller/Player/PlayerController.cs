using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    private PlayerHUDController playerHUDController;
    private PlayerMagazineInventory playerMagazineInventory;
    public float rotSpeed;

    protected override void Awake()
    {
        base.Awake();
        playerHUDController = GetComponent<PlayerHUDController>();
        playerMagazineInventory = GetComponent<PlayerMagazineInventory>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        MoveMent();
        Rotate();
    }

    private void MoveMent()
    {
        Vector3 moveVec = new(InputManager.GetInputHorizontal(), 0, InputManager.GetInputVertical());
        moveVec.Normalize();

        transform.Translate(moveSpeed * Time.deltaTime * moveVec);
    }

    float rotY = 0;
    private void Rotate()
    {
        rotY += InputManager.GetInputMouse().x * rotSpeed;
        transform.eulerAngles = new Vector3(0, rotY);
    }

    protected override void Attack()
    {
        base.Attack();
        if (Input.GetMouseButton(0))
        {
            if (currentWeapon.GetCurrentBullet() > 0)
            {
                currentWeapon.SpawnBullet();
            }
            else
            {
                Reload();
            }
        }
    }
}