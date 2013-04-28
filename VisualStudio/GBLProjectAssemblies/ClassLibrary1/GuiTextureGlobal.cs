using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    public class GuiTextureGlobal : MonoBehaviour
    {
        public Texture TextOne;
        public Texture TextTwo;
        public Texture TextThree;
        public Texture TextFour;
        public Texture TextFive;
        public Texture TextSix;
        public Texture TextSeven;
        public Texture TextEight;
        public Texture TextNine;
        public Texture TextEqual;
        public Texture TextMinus;
        public Texture TextPlus;
        public Texture TextQuestionmark;

        private Dictionary<char, Texture> charDict = new Dictionary<char, Texture>();

        void Start()
        {
            charDict['1'] = TextOne;
            charDict['2'] = TextTwo;
            charDict['3'] = TextThree;
            charDict['4'] = TextFour;
            charDict['5'] = TextFive;
            charDict['6'] = TextSix;
            charDict['7'] = TextSeven;
            charDict['8'] = TextEight;
            charDict['9'] = TextNine;
            charDict['='] = TextEqual;
            charDict['-'] = TextMinus;
            charDict['+'] = TextPlus;
            charDict['?'] = TextQuestionmark;
            GameController.Instance.RegisterGlobalTexture(this);
        }

        public Texture TranslateToTexture(char character)
        {
            return charDict[character];
        }

    }
}
