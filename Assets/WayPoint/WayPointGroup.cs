using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointGroup : MonoBehaviour
{
    private List<WayPoint> wayPoints = new();

    private void OnDrawGizmos()
    {
        wayPoints = GetComponentsInChildren<WayPoint>().ToList();
        wayPoints.Remove(wayPoints[0]);

        for (int i = 0; i < wayPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(wayPoints[i].transform.position * wayPoints[i].width, wayPoints[i + 1].transform.position * wayPoints[i].width);
        }
    }
}