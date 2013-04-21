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

        public void OnGUI()
        {
            var guiOptions = new GUILayoutOption[] { GUILayout.Height(15) };
            GUILayout.Label(GameController.Instance.TheMathPiece.LeftOperand + " " + GameController.Instance.TheMathPiece.Operator 
                + " " + GameController.Instance.TheMathPiece.RightOperand + " = " + GameController.Instance.TheMathPiece.RightAnswer 
                + "  NOT " + GameController.Instance.TheMathPiece.WrongAnswer);


        }

    }
}
