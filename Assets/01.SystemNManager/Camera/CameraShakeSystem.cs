using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeSystem : Sigleton<CameraShakeSystem>
{
    public void CameraShake(float shakePower, float shakeTime)
    {
        StartCoroutine(CameraShake());

        IEnumerator CameraShake()
        {
            float t = 0;
            while (t >= shakeTime)
            {
                t += Time.deltaTime;

                transform.localPosition = transform.position + Random.insideUnitSphere * shakePower;
                yield return null;
            }
        }
    }
}