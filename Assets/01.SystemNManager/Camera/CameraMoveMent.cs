using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveMent : Sigleton<CameraMoveMent>
{
    public float cameraRotSpeed;
    public float clampYLow;
    public float clampYHigh;

    public bool isCanRot;
    private float rotX = 0;

    public Transform playerPos;

    private float startfield = 60;

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

    public void SlideCamera()
    {
        transform.position = playerPos.position + new Vector3(0, 0.7f, 0);

        StartCoroutine(SlideUp());

        IEnumerator SlideUp()
        {
            yield return new WaitForSeconds(1);

            transform.position = playerPos.position + new Vector3(0, 1.7f, 0);
        }
    }

    public void SprintCamera(float value)
    {
        while (Camera.main.fieldOfView >= value)
        {
            Camera.main.fieldOfView = startfield + Time.deltaTime;
        }
        Camera.main.fieldOfView = startfield;
    }
}