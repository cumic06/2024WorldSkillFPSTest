using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private float cameraRotSpeed;
    [SerializeField] private float clampYLow;
    [SerializeField] private float clampYHigh;

    private bool isCanRot;

    private void Start()
    {
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
        CameraRotation();
    }

    private void CameraRotation()
    {
        float rotHorizontal = -InputManager.GetInputMouse().y * cameraRotSpeed * Time.deltaTime;
        float rotVertical = InputManager.GetInputMouse().x * cameraRotSpeed * Time.deltaTime;

        float cameraRotY = rotVertical;

        float camreaRotX = Mathf.Clamp(rotHorizontal, clampYLow, clampYHigh);

        transform.eulerAngles = new Vector2(camreaRotX, cameraRotY);
    }
}
