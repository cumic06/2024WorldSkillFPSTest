using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : Sigleton<WayPointManager>
{
    #region Parameter
    private class BFSWayPoint
    {
        public WayPoint wayPoint;
        public BFSWayPoint parentPoint;

        public BFSWayPoint(WayPoint wayPoint)
        {
            this.wayPoint = wayPoint;
        }
    }

    public class ConnectedWayPoint
    {
        public WayPoint a;
        public WayPoint b;
    }
    public List<ConnectedWayPoint> connectedWayPoints = new();
    private Dictionary<WayPoint, List<WayPoint>> connectWayBranchs = new();

    public class WayPointObject
    {
        public Transform transform;
        private WayPoint wayPoint;

        public WayPointObject(Transform transform)
        {
            this.transform = transform;
        }

        public void SetCurrentWayPoint(WayPoint wayPoint)
        {
            this.wayPoint = wayPoint;
        }

        public WayPoint GetCurrentWayPoint()
        {
            return wayPoint;
        }
    }
    private List<WayPointObject> wayPointObjects = new();

    public List<WayPoint> allWayPoints = new();
    #endregion

    #region Init
    protected override void Awake()
    {
        base.Awake();
        SetConnectedWayBranches();
    }

    private void SetConnectedWayBranches()
    {
        foreach (var wayPoint in allWayPoints)
        {
            if (!connectWayBranchs.ContainsKey(wayPoint))
            {
                connectWayBranchs.Add(wayPoint, new());
            }

            List<WayPoint> connectedPoints = new();

            connectedWayPoints.FindAll(data => data.a == wayPoint).ForEach(data => connectedPoints.Add(data.b));
            connectedWayPoints.FindAll(data => data.b == wayPoint).ForEach(data => connectedPoints.Add(data.a));

            foreach (var connectedPoint in connectedPoints)
            {
                if (!connectWayBranchs[wayPoint].Exists(data => data == connectedPoint))
                {
                    connectWayBranchs[wayPoint].Add(connectedPoint);
                }
            }
        }
    }

    #endregion

    #region WayPoint UseObject
    private WayPointObject GetWayPointObject(Transform transform)
    {
        WayPointObject wayPointObject = wayPointObjects.Find(data => data.transform == transform);

        if (wayPointObject == null)
        {
            wayPointObject = new WayPointObject(transform);
        }

        return wayPointObject;
    }
    #endregion

    #region Control WayPoint
    public WayPoint GetNearestWayPoint(Vector3 point)
    {
        return allWayPoints.OrderBy(data => Vector3.Distance(data.transform.position, point)).First();
    }

    public WayPoint GetNextWayPoint(WayPoint wayPoint)
    {
        int wayPointIndex = allWayPoints.IndexOf(wayPoint);

        if (wayPointIndex + 1 >= allWayPoints.Count) return null;

        return wayPoint.IsExistNextWayPoint ? allWayPoints[wayPointIndex + 1] : null;
    }

    public WayPoint GetPreviousWayPoint(WayPoint wayPoint)
    {
        int wayPointIndex = allWayPoints.IndexOf(wayPoint);

        if (wayPointIndex - 1 < 0) return null;

        return allWayPoints[wayPointIndex - 1].IsExistNextWayPoint ? allWayPoints[wayPointIndex - 1] : null;
    }
    #endregion

    #region PathFinding
    public List<WayPoint> PathFinding(Transform transform, WayPoint findWayPoint)
    {
        WayPointObject wayPointObject = GetWayPointObject(transform);
        List<WayPoint> path = new();
        BFSWayPoint endBFSWayPoint = BFSFindingBothWay(wayPointObject.GetCurrentWayPoint(), findWayPoint);

        if (endBFSWayPoint == null)
        {
            Debug.LogError("찾기 실패.");
        }
        else
        {
            BFSWayPoint currentBFSWayPoint = endBFSWayPoint;
            while (currentBFSWayPoint.wayPoint != wayPointObject.GetCurrentWayPoint())
            {
                path.Add(currentBFSWayPoint.wayPoint);
                currentBFSWayPoint = currentBFSWayPoint.parentPoint;
            }
            path.Reverse();
        }

        return path;
    }

    private delegate WayPoint GetWayPointDelegate(WayPoint wayPoint);

    private List<BFSWayPoint> GetWayPointAllBFSPath(GetWayPointDelegate getWayPointDelegate, BFSWayPoint currentPoint)
    {
        List<BFSWayPoint> bfsWayPoints = new();

        WayPoint wayPoint = getWayPointDelegate?.Invoke(currentPoint.wayPoint);

        if (wayPoint != null)
        {
            BFSWayPoint bfsWayPoint = new(wayPoint);
            bfsWayPoint.parentPoint = currentPoint;
            bfsWayPoints.Add(bfsWayPoint);

            foreach (WayPoint connected in connectWayBranchs[wayPoint])
            {
                BFSWayPoint branchBFSWayPoint = new(connected);
                branchBFSWayPoint.parentPoint = bfsWayPoint;
                bfsWayPoints.Add(branchBFSWayPoint);
            }
        }

        return bfsWayPoints;
    }

    private BFSWayPoint BFSFindingBothWay(WayPoint currentWayPoint, WayPoint findWayPoint)
    {
        List<BFSWayPoint> bfsWayPoints = new();
        bfsWayPoints.Add(new(currentWayPoint));

        int currentFindIndex = 0;
        while (currentFindIndex < bfsWayPoints.Count)
        {
            BFSWayPoint currentPoint = bfsWayPoints[currentFindIndex++];
            if (currentPoint == null) continue;

            WayPoint current = currentPoint.wayPoint;
            if (findWayPoint == current)
                return currentPoint;

            List<BFSWayPoint> addBFSWayPoints = new();
            addBFSWayPoints.AddRange(GetWayPointAllBFSPath(GetNextWayPoint, currentPoint));
            addBFSWayPoints.AddRange(GetWayPointAllBFSPath(GetPreviousWayPoint, currentPoint));

            foreach (BFSWayPoint addBFSWayPoint in addBFSWayPoints)
            {
                if (addBFSWayPoint != null && !bfsWayPoints.Exists(data => data.wayPoint == addBFSWayPoint.wayPoint))
                    bfsWayPoints.Add(addBFSWayPoint);
            }

            if (currentFindIndex >= 1000)
            {
                Debug.LogError("Limit 초과");
                break;
            }
        }
        return null;
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        for (int i = 0; i < allWayPoints.Count; i++)
        {
            WayPoint currentWayPoint = allWayPoints[i];

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(currentWayPoint.GetMinBound(), currentWayPoint.GetMaxBound());
        }
    }
    #endregion
}