using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraMoveMent : Sigleton<UICameraMoveMent>
{
    public float startfield = 60;

    private Coroutine sprintCor;

    public Camera currentCamera;

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
                currentCamera.fieldOfView = Mathf.Lerp(currentCamera.fieldOfView, value, t * 3);
                yield return null;
            }
            currentCamera.fieldOfView = value;
        }
    }
}
