using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    public class GoalBinding : MonoBehaviour
    {

        public void Start()
        {
            GameController.Instance.Goal = this.gameObject;

        }


    }
}
