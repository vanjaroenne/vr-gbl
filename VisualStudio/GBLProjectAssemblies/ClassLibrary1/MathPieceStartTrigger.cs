using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    class MathPieceStartTrigger : MonoBehaviour
    {
        public int Nr = 0;

        void OnTriggerEnter()
        {
            GameController.Instance.SetCurrentMathPiece(Nr);
        }

    }
}
