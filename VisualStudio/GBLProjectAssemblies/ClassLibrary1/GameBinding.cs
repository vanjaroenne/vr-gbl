using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    public class GameBinding : MonoBehaviour
    {
     /* public Texture Wood;
        public Texture Gunn;
        public Texture Nums; 
        public int LeftOperand;
        public int RightOperand;
        public char Operator; 
        private static System.Random rand = new System.Random(); */

        public void Start()
        {
            GameController.Instance.RestartGame();
        }

        public void OnGUI()
        {
          /*LeftOperand = rand.Next(1, 10);
            RightOperand = rand.Next(1, 10);
            Debug.Log("Left = " + LeftOperand + ", Right = " + RightOperand); */
            /**var guiOptions = new GUILayoutOption[] { GUILayout.Height(15) };
            GUILayout.Label(GameController.Instance.TheMathPiece.LeftOperand + " " + GameController.Instance.TheMathPiece.Operator 
                + " " + GameController.Instance.TheMathPiece.RightOperand + " = " + GameController.Instance.TheMathPiece.RightAnswer 
                + "  NOT " + GameController.Instance.TheMathPiece.WrongAnswer);

       /*   GUI.Button(new Rect(0, 0, 50, 100), new GUIContent(Wood));
            GUI.Button(new Rect(50, 0, 50, 100), new GUIContent(Gunn));
            GUI.Button(new Rect(100, 0, 50, 100), new GUIContent(Nums)); */
        }
        

    }
}
