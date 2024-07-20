using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    private PlayerHUDController playerHUDController;
    private PlayerMagazineInventory playerMagazineInventory;
    public float rotSpeed;
    public float stamina;
    public float maxStamina = 100;

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

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        if (Input.GetKeyDown(KeyCode.Space) /*&& stamina >*/)
        {
            Jump();
        }

        Attack();
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

    private void Jump()
    {

    }

    protected override void Attack()
    {
        base.Attack();
        currentWeapon.currentAttackDelay += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            if (IsCanAttack())
            {
                currentWeapon.Shoot();
                currentWeapon.currentAttackDelay = 0;
            }
        }
    }
}