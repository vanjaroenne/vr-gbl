using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    public class InputBinding : MonoBehaviour
    {
        public void Update()
        {
            GameController.Instance.PlayerRelativePosition = new Vector3(0,0,Input.GetAxis("Horizontal"));
        }

    }
}
