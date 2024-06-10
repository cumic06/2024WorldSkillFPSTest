using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMove), typeof(PlayerAttack))]
public class Player : Unit
{
    private PlayerMove playerMove;
    private PlayerAttack playerAttack;

    protected override void Awake()
    {
        base.Awake();
        playerMove = GetComponent<PlayerMove>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }


    private void FixedUpdate()
    {
        playerMove.Move();
        playerMove.Rotation();
    }

    public override void OnDead()
    {

    }
}
