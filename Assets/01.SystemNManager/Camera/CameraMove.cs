using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float cameraRotSpeed;
    public float clampYLow;
    public float clampYHigh;

    public bool isCanRot;
    private float rotX = 0;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        GameStateEventBus.Subscribe(GameState.Pause, CanRotFalse);
        GameStateEventBus.Subscribe(GameState.Play, CanRotTrue);
    }

    private void CanRotTrue()
    {
        isCanRot = true;
    }

    private void CanRotFalse()
    {
        isCanRot = false;
    }

    private void FixedUpdate()
    {
        if (!isCanRot) return;
        CameraRotation();
    }

    private void CameraRotation()
    {
        float rotHorizontal = -InputManager.GetInputMouse().y * cameraRotSpeed;

        rotX += rotHorizontal;

        float camreaRotX = Mathf.Clamp(rotX, clampYLow, clampYHigh);

        transform.eulerAngles = new Vector2(camreaRotX, transform.eulerAngles.y);
    }
}