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

        private MathPiece[] mathPieces;
        private Dictionary<int,SignOne[]> signOnes = new Dictionary<int,SignOne[]>();

        private GuiTextureGlobal globalTexture;
        private GuiTextureBinding[] guiTextureBindings = new GuiTextureBinding[4];
        

        public void RestartGame()
        {
            UserRelativePosition = new Vector3();
            signOnes.Clear();
            CreateMathPieces(10);
        }

        public void CreateMathPieces(int nrOfMathPieces)
        {
            mathPieces = new MathPiece[nrOfMathPieces];
            for (int i = 0; i < nrOfMathPieces; i++)
            {
                mathPieces[i] = MathPiece.GetNextPiece();
            }
        }

        public void SetCurrentMathPiece(int pieceNr)
        {
            TheMathPiece = mathPieces[pieceNr];
            var rand = new System.Random();
            rightAnswerIsLeft = rand.Next(2) == 0 ? true : false;
            leftAnswer = rightAnswerIsLeft ? TheMathPiece.FormatRightAnswer() : TheMathPiece.FormatWrongAnswer();
            rightAnswer = rightAnswerIsLeft ? TheMathPiece.FormatWrongAnswer() : TheMathPiece.FormatRightAnswer();

            Debug.Log("Set mathpiece to: " + TheMathPiece.FormatPiece());
            Debug.Log("Right answer is " + (rightAnswerIsLeft ? "Left" : "Right"));

            var signOnesToUpdate = signOnes[pieceNr];
            //Debug.Log("signsToUpdate is null? : " + (signOnesToUpdate == null));
            
            signOnesToUpdate[0].SpawnSign(global.TranslateTransform(leftAnswer,0));
            signOnesToUpdate[1].SpawnSign(global.TranslateTransform(leftAnswer,1));
            signOnesToUpdate[2].SpawnSign(global.TranslateTransform(rightAnswer,0));
            signOnesToUpdate[3].SpawnSign(global.TranslateTransform(rightAnswer,1));

            SetTexture(guiTextureBindings[0], TheMathPiece.LeftOperand.ToString()[0]);
            SetTexture(guiTextureBindings[1], TheMathPiece.Operator);
            SetTexture(guiTextureBindings[2], TheMathPiece.RightOperand.ToString()[0]);
            SetTexture(guiTextureBindings[3], '?');

        }


        private SignGlobal global;
        public void RegisterSignGlobal(SignGlobal global)
        {
            this.global = global;
        }

        private System.Object registerLock = new System.Object();

        public void RegisterSignOne(SignOne signOne)
        {
            lock (registerLock)
            {
                if (!signOnes.ContainsKey(signOne.MathPieceNumber))
                {
                    signOnes[signOne.MathPieceNumber] = new SignOne[4];
                }
                else Debug.Log("Allready contained key for: " + signOne.MathPieceNumber);
                int position = (signOne.IsLeft ? 0 : 2) | (signOne.PositionInNumber);
                signOnes[signOne.MathPieceNumber][position] = signOne;
                //Debug.Log("Registered sign one at pos : " + position + " with: isLeft: " + signOne.IsLeft + " pos: " + signOne.PositionInNumber);
                //Debug.Log("Is it null now?: at pos: ? " + position + " : " + (signOnes[signOne.MathPieceNumber][position] == null));
            }
        }

        private String leftAnswer;    // KIG HER  never assigned to
        private String rightAnswer;   // KIG HER  never assigned to
        public bool rightAnswerIsLeft;

        public void RegisterGlobalTexture(GuiTextureGlobal global)
        {
            this.globalTexture = global;
        }

        public void RegisterGuiTextureBinding(GuiTextureBinding binding)
        {
            guiTextureBindings[binding.TextureBindingNr] = binding;
        }

        private void SetTexture(GuiTextureBinding binding, char character)
        {
            var texture = globalTexture.TranslateToTexture(character);
            var guiObject = binding.GetComponent<GUITexture>();
            guiObject.texture = texture;
            Debug.Log("Updated texture for: " + binding.TextureBindingNr + " to: " + character);
        }
    }
}
