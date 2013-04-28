using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    class AnswerTrigger : MonoBehaviour
    {
        public bool isLeft;
        void OnTriggerEnter()
        {
            if (isLeft != GameController.Instance.rightAnswerIsLeft)
            {

                MainGameManager.SP.SetGameOver();
            }
        }
    }
}
