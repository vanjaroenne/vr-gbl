using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    public enum MixedState { playing, won, lost };
    public class Mixed : MonoBehaviour
    {
        private int totalFish;
        public int foundFish;
        private MixedState gameState;
        public static Mixed SP;

        void Awake()
        {
            SP = this;
            foundFish = 0;
            gameState = MixedState.playing;
            totalFish = GameObject.FindGameObjectsWithTag("Pickup").Length;
            Time.timeScale = 1.0f;
        }

        void OnGUI()
        {
            GUILayout.Label(" Antal fundne fisk: " + foundFish + "/" + totalFish);

            if (gameState == MixedState.lost)
            {
                GUILayout.Label("Du tabte spillet!");
                if (GUILayout.Button("Prøv igen"))
                {
                    Application.LoadLevel(Application.loadedLevel);
                }

            }
            else if (gameState == MixedState.won)
            {
                GUILayout.Label("Du vandt!");
                if (GUILayout.Button("Spil igen"))
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
        }
    }
}