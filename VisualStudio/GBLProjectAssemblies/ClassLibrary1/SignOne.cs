using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    public class SignOne : MonoBehaviour
    {

        public bool IsLeft;
        public int PositionInNumber;
        public int MathPieceNumber;

        private bool isRegistered = false;

        void Start()
        {
            isRegistered = false;
        }

        void Update()
        {
            if (!isRegistered)
            {
                GameController.Instance.RegisterSignOne(this);
                isRegistered = true;
            }

        }


        public void SpawnSign(Transform signTrans)
        {
            Vector3 spawnPos = transform.position;
            Instantiate(signTrans, spawnPos, Quaternion.identity);
            //Debug.Log("Set number for signOne: " + (IsLeft ? "Left" : "Right") + " " + " pos: " + PositionInNumber);
        }

    }

}
