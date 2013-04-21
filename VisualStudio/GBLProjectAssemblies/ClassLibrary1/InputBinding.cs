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
            var jumpSpeed = Input.GetAxis("Vertical");
            //GameController.Instance.PlayerRelativePosition = new Vector3(0,jumpSpeed,Input.GetAxis("Horizontal"));
            
            //var pos = GameController.Instance.PlayerRelativePosition;
           //debug.Log(pos.x + "," + pos.y + "," + pos.z + " YOs");
        }


    }



}
