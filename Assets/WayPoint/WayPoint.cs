using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public float width = 1.0f;

    public Vector3 GetMinBound()
    {
        return transform.position + transform.right * width * 0.5f;
    }

    public Vector3 GetMaxBound()
    {
        return transform.position - transform.right * width * 0.5f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(GetMinBound(), GetMaxBound());
    }
}