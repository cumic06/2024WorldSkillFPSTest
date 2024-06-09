using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Player player;
    [SerializeField] private float rotSpeed;
    [SerializeField] private int stamina;
    private const int maxStamina = 100;

    private bool isCanMove;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        GameStateEventBus.Subscribe(GameState.Pause, CanMoveFalse);
        GameStateEventBus.Subscribe(GameState.Play, CanMoveTrue);
    }

    private void CanMoveTrue()
    {
        isCanMove = true;
    }

    private void CanMoveFalse()
    {
        isCanMove = false;
    }

    public void Move()
    {
        if (!isCanMove) return;
        Vector3 moveVec = new(InputManager.GetInputHorizontal(), player.Rigid.velocity.y, InputManager.GetInputVertical());
        transform.Translate(player.GetUnitStat().currentMoveSpeed * Time.deltaTime * moveVec);
    }

    public void Rotation()
    {
        if (!isCanMove) return;
        transform.rotation = Quaternion.Euler(transform.rotation.x, InputManager.GetInputMouse().x * rotSpeed * Time.deltaTime, transform.rotation.z);
    }

    private void UseStamina(int value)
    {
        if (CanUseStamina(value))
        {
            stamina -= value;
        }
        else
        {

        }
    }

    private bool CanUseStamina(int value)
    {
        return stamina - value > 0;
    }

    public void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            UseStamina(5);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseStamina(10);
        }
    }

    public void Sliding()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            UseStamina(30);
        }
    }
}
