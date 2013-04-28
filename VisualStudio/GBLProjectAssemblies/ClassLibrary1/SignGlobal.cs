using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GBLProjectAssemblies
{
    public class SignGlobal : MonoBehaviour
    {
        public Transform Space;
        public Transform NumberZero;
        public Transform NumberOne;
        public Transform NumberTwo;
        public Transform NumberThree;
        public Transform NumberFour;
        public Transform NumberFive;
        public Transform NumberSix;
        public Transform NumberSeven;
        public Transform NumberEight;
        public Transform NumberNine;
        public Transform PlusSign;
        public Transform MinusSign;
        public Transform EqualSign;
        public Transform Questionmark;

         
        private Dictionary<char, Transform> transDict = new Dictionary<char, Transform>();

        void Start()
        {
            transDict.Clear();
            transDict[' '] = Space;
            transDict['0'] = NumberZero;
            transDict['1'] = NumberOne;
            transDict['2'] = NumberTwo;
            transDict['3'] = NumberThree;
            transDict['4'] = NumberFour;
            transDict['5'] = NumberFive;
            transDict['6'] = NumberSix;
            transDict['7'] = NumberSeven;
            transDict['8'] = NumberEight;
            transDict['9'] = NumberNine;
            transDict['+'] = PlusSign;
            transDict['-'] = MinusSign;

            GameController.Instance.RegisterSignGlobal(this);
        }

        public Transform TranslateTransform(String number, int position) {
            return transDict[number.ToCharArray()[position]];
        }   
    }
}