using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    public class WayPointBinding : MonoBehaviour
    {
        public void Start()
        {
            GameController.Instance.RegisterWayPoint(this);

        }


    }
}
