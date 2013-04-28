using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    public class MainGameManager : MonoBehaviour
    {
        public enum MainGameState { playing, won, lost };

        public static MainGameManager SP;

        private int totalFish;
        private int foundFish;
        private MainGameState gameState;
        public Texture GameOverPic;
        public int NrOfMathPieces;


        void Awake()
        {
            SP = this;
            foundFish = 0;
            gameState = MainGameState.playing;
            totalFish = GameObject.FindGameObjectsWithTag("Pickup").Length;
            Time.timeScale = 1.0f;
        }

        void OnGUI()
        {
            GUILayout.Label(" Found Fish: " + foundFish + "/" + totalFish);

            if (gameState == MainGameState.lost)
            {
                GUI.Box(new Rect(0, 0, 1600, 900), GameOverPic);
                //GUILayout.Label("You Lost!");
                if (GUILayout.Button("Try again"))
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
            else if (gameState == MainGameState.won)
            {
                GUILayout.Label("You won!");
                if (GUILayout.Button("Play again"))
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
        }

        public void FoundFish()
        {
            foundFish++;
            if (foundFish >= totalFish)
            {
                WonGame();
            }
        }

        public void WonGame()
        {
            Time.timeScale = 0.0f; //Pause game
            gameState = MainGameState.won;
        }

        public void SetGameOver()
        {
            Time.timeScale = 0.0f; //Pause game
            gameState = MainGameState.lost;
        }
    }
}