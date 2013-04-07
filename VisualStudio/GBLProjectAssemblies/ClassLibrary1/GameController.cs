using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace GBLProjectAssemblies
{
    public class GameController
    {

        private static GameController instance = new GameController();

        public static GameController Instance { get {return instance;}}

        private GameObject player;

        public void RegisterPlayer(GameObject player) {
            this.player = player;
        }




    }
}
