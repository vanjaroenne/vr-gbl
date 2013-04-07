using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace GBLProjectAssemblies
{
    public class GameController
    {

        private static GameController instance = new GameController();

        public static GameController Instance { get {return instance;}}

        private GameObject player;
        private SortedList<MonoBehaviour,WayPointBinding> waypoints = new SortedList<MonoBehaviour,WayPointBinding>(new PositionCompararer());
        public Vector3 PlayerRelativePosition { get; set; }

        public void RegisterPlayer(GameObject player) {
            this.player = player;
        }

        public void RegisterWayPoint(WayPointBinding waypoint)
        {
            waypoints.Add(waypoint, waypoint);
        }

        public void RestartGame()
        {
            waypoints.Clear();
        }

        public WayPointBinding NextWayPoint
        {
            get
            {
                if (waypoints.Keys.Count > 0)
                    return waypoints.Values[0];
                return null;
            }
        }

        public void RemoveCurrentWayPoint()
        {
            if (waypoints.Count > 0)
                waypoints.Remove(waypoints.Keys[0]);
        }

        private class PositionCompararer : IComparer<MonoBehaviour>
        {
            public int Compare(MonoBehaviour m1, MonoBehaviour m2)
            {
                if (m1 == m2) return 0;
                return -(int)(m1.transform.position.x - m2.transform.position.x) * 1000;
            }
        }


    }


}
