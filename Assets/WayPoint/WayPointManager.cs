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

    #endregion
}