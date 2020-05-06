using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* --------------------------------------
    Operation priorities:
    1. ^
    2. * /
    3. + -

    Rules:
    1. '$' - End of line
    2. (double)47.47474747 - Error code for calc func
-------------------------------------- */

namespace MultiD_Opt.tools
{

    // @Tool //
    class FuncParser
    {

        private string Function;
        private List<string> Tokens;
        private Stack<string> Stack;
        private Stack<string> PolNot;
        private List<string> Variables;

        private void Init()
        {
            Tokens = new List<string>();
            Stack = new Stack<string>();
            PolNot = new Stack<string>();
            Variables = new List<string>();
        }

        public bool Read(string Input)
        {
            Init();
            Function = Input;
            Tokenize(Input);
            bool result = Poliz();
            result = result == true ? Calculate(true) == 47.47474747 ? false : true : false;

            return result;
        }

        private bool Tokenize(string input)
        {
            string[] splitedString = input.Split(' ');
            for (int i = 0; i < splitedString.Length; i++)
            {
                if (splitedString[i] != "")
                {
                    Tokens.Add(splitedString[i]);
                }
            }
            Tokens.Add("$");

            return true;
        }

        private bool Poliz()
        {
            Stack.Clear();
            PolNot.Clear();
            for (int i = 0; i < Tokens.Count; i++)
            {
                switch (Tokens[i])
                {
                    case "$":
                        while (Stack.Count != 0)
                        {
                            PolNot.Push(Stack.Pop());
                        }
                        break;
                    case "(":
                        Stack.Push("(");
                        break;
                    case ")":
                        if (Stack.Count == 0)
                        {
                            return false;
                        }
                        string temp = Stack.Pop();
                        while (temp != "(")
                        {
                            PolNot.Push(temp);
                            if (Stack.Count == 0)
                            {
                                return false;
                            }
                            temp = Stack.Pop();
                        }
                        break;
                    case "+":
                        if (Stack.Count != 0)
                        {
                            while (Stack.Peek() == "+" || Stack.Peek() == "-" || Stack.Peek() == "/" || Stack.Peek() == "*" || Stack.Peek() == "^")
                            {
                                PolNot.Push(Stack.Pop());
                                if (Stack.Count == 0)
                                {
                                    break;
                                }
                            }
                        }
                        Stack.Push("+");
                        break;
                    case "*":
                        if (Stack.Count != 0)
                        {
                            while (Stack.Peek() == "/" || Stack.Peek() == "*" || Stack.Peek() == "^")
                            {
                                PolNot.Push(Stack.Pop());
                                if (Stack.Count == 0)
                                {
                                    break;
                                }
                            }
                        }
                        Stack.Push("*");
                        break;
                    case "-":
                        if (Stack.Count != 0)
                        {
                            while (Stack.Peek() == "+" || Stack.Peek() == "-" || Stack.Peek() == "/" || Stack.Peek() == "*" || Stack.Peek() == "^")
                            {
                                PolNot.Push(Stack.Pop());
                                if (Stack.Count == 0)
                                {
                                    break;
                                }
                            }
                        }
                        Stack.Push("-");
                        break;
                    case "/":
                        if (Stack.Count != 0)
                        {
                            while (Stack.Peek() == "/" || Stack.Peek() == "*" || Stack.Peek() == "^")
                            {
                                PolNot.Push(Stack.Pop());
                                if (Stack.Count == 0)
                                {
                                    break;
                                }
                            }
                        }
                        Stack.Push("/");
                        break;
                    case "^":
                        if (Stack.Count != 0)
                        {
                            while (Stack.Peek() == "^")
                            {
                                PolNot.Push(Stack.Pop());
                                if (Stack.Count == 0)
                                {
                                    break;
                                }
                            }
                        }
                        Stack.Push("^");
                        break;
                    default:
                        PolNot.Push(Tokens[i]);
                        break;
                }
            }

            Stack<string> tempStack = new Stack<string>(PolNot);
            PolNot.Clear();
            while (tempStack.Count != 0)
            {
                PolNot.Push(tempStack.Pop());
            }

            return true;
        }

        public double Calculate(bool check = false)
        {
            Stack<string> tempStack = new Stack<string>(PolNot);
            Stack<double> calcStack = new Stack<double>();
            Dictionary<string, double> unknowns = new Dictionary<string, double>();
            int tempCycleCount = tempStack.Count;

            for (int i = 0; i < tempCycleCount; i++)
            {
                if (tempStack.Count == 0)
                {
                    return 47.47474747;
                }
                string tempVar = tempStack.Pop();
                double tempA;
                double tempB;
                switch (tempVar)
                {
                    case "+":
                        if (calcStack.Count == 0 || calcStack.Count == 1)
                        {
                            return 47.47474747;
                        }
                        tempA = calcStack.Pop();
                        tempB = calcStack.Pop();
                        calcStack.Push(tempB + tempA);
                        break;
                    case "-":
                        if (calcStack.Count == 0 || calcStack.Count == 1)
                        {
                            return 47.47474747;
                        }
                        tempA = calcStack.Pop();
                        tempB = calcStack.Pop();
                        calcStack.Push(tempB - tempA);
                        break;
                    case "*":
                        if (calcStack.Count == 0 || calcStack.Count == 1)
                        {
                            return 47.47474747;
                        }
                        tempA = calcStack.Pop();
                        tempB = calcStack.Pop();
                        calcStack.Push(tempB * tempA);
                        break;
                    case "/":
                        if (calcStack.Count == 0 || calcStack.Count == 1)
                        {
                            return 47.47474747;
                        }
                        tempA = calcStack.Pop();
                        tempB = calcStack.Pop();
                        if (tempA != 0)
                        {
                            calcStack.Push(tempB / tempA);
                        }
                        else
                        {
                            return 47.47474747;
                        }
                        break;
                    case "^":
                        if (calcStack.Count == 0 || calcStack.Count == 1)
                        {
                            return 47.47474747;
                        }
                        tempA = calcStack.Pop();
                        tempB = calcStack.Pop();
                        calcStack.Push(Math.Pow(calcStack.Pop(), calcStack.Pop()));
                        break;
                    default:
                        double tempConv = 0;
                        try
                        {
                            tempConv = Convert.ToDouble(tempVar);
                        }
                        catch (Exception e)
                        {
                            if (check)
                            {
                                tempConv = 0;
                                if (!Variables.Contains(tempVar))
                                {
                                    Variables.Add(tempVar);
                                }
                            }
                            else
                            {
                                if (unknowns.Keys.Contains(tempVar))
                                {
                                    unknowns.TryGetValue(tempVar, out tempConv);
                                }
                                else
                                {
                                    unknowns.Add(tempVar, tempConv = UnknownInput(tempVar));
                                }
                            }
                        }
                        calcStack.Push(tempConv);
                        break;
                }
            }

            if (calcStack.Count != 1)
            {
                return 47.47474747;
            }

            return calcStack.Pop();
        }

        private double UnknownInput(string tempVar)
        {
            Console.Write("Введите значение для переменной " + tempVar + ": ");
            double tempInput = 0;
            while (true)
            {
                try
                {
                    tempInput = Convert.ToDouble(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.Write("Вы ввели некорректное значение для переменной " + tempVar + ", попробуйте снова: ");
                }
            }
            return tempInput;
        }

        public List<string> GetVariables()
        {
            return Variables;
        }

    }

}
