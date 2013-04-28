using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    public class GameOverTrigger : MonoBehaviour
    {
        void OnTriggerEnter()
        {
            MainGameManager.SP.SetGameOver();
        }
    }
}
