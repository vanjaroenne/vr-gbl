using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace GBLProjectAssemblies
{
    public class GameController
    {

        private static GameController instance = new GameController();

        public static GameController Instance { get { return instance; } }

        public GameObject Player {get; set; }
        public GameObject Goal { get; set; }
        public MathPiece TheMathPiece { get; set; }

        
        public Vector3 UserRelativePosition { get; set; }


        public void RestartGame()
        {
            UserRelativePosition = new Vector3();
            TheMathPiece = MathPiece.GetNextPiece();
        }




    }


}
