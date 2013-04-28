using System;
using System.Collections.Generic;
using System.Text;

namespace GBLProjectAssemblies
{
    public class MathPiece
    {
        public int LeftOperand;
        public int RightOperand;
        public int WrongAnswer;
        public int RightAnswer;
        public char Operator;
        public String FormatRightAnswer()
        {
            return FormatAnswer(RightAnswer);
        }

        public String FormatWrongAnswer()
        {
            return FormatAnswer(WrongAnswer);
        }

        private static String FormatAnswer(int answer)
        {
            if (answer < 10)
                return " " + answer;
            else return answer.ToString();
        }

        private static Random rand = new Random();

        public static MathPiece GetNextPiece()
        {
            var returnee = new MathPiece();
            returnee.LeftOperand = rand.Next(1,10);
            returnee.RightOperand =  rand.Next(1, 10);
            var plusMinusFactor = 1;
            if (rand.Next(0, 2) == 1)
            {
                returnee.Operator = '+';
            }
            else
            {
                returnee.Operator = '-';
                plusMinusFactor = -1;
                if (returnee.RightOperand > returnee.LeftOperand)
                {
                    var temp = returnee.RightOperand;
                    returnee.RightOperand = returnee.LeftOperand;
                    returnee.LeftOperand = temp;
                }
            }

            returnee.RightAnswer = returnee.LeftOperand + returnee.RightOperand * plusMinusFactor;
            returnee.WrongAnswer = returnee.LeftOperand + returnee.RightOperand * plusMinusFactor + (rand.Next(0, 2) == 1 ? 1 : -1) * (rand.Next(1, 5));
            while (returnee.WrongAnswer < 0 || returnee.WrongAnswer == returnee.RightAnswer)
                returnee.WrongAnswer = 0 + rand.Next(1, 5);

            return returnee;
        }

        public String FormatPiece()
        {
            return LeftOperand + " " + Operator + " " + RightOperand + " = " + RightAnswer + " NOT " + WrongAnswer;
        }

    }
}
