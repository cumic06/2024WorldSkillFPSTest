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

    public float startfield = 60;

    private Coroutine sprintCor;

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
        if (sprintCor != null)
        {
            StopCoroutine(sprintCor);
        }
        sprintCor = StartCoroutine(FieldOfViewCamera(value));

        IEnumerator FieldOfViewCamera(float value)
        {
            float t = 0;

            while (t < 1)
            {
                t += Time.deltaTime;
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, value, t * 3);
                yield return null;
            }
            Camera.main.fieldOfView = value;
        }
    }
}