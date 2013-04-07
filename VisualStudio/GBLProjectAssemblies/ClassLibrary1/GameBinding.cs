using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    public class GameBinding : MonoBehaviour
    {

        public void Start()
        {
            GameController.Instance.RestartGame();
        }

    }
}
