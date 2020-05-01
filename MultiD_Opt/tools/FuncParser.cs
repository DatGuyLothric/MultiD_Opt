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
-------------------------------------- */

namespace MultiD_Opt.tools
{

    class FuncParser
    {

        private string String;
        private List<string> Tokens = new List<string>();
        private Stack<string> Stack = new Stack<string>();
        private Stack<string> Pol_Not = new Stack<string>();

        public bool Read(string Input)
        {
            Tokenize(Input);
            bool Result = Poliz();
            // TODO: Check on calc

            return Result;
        }

        private bool Tokenize(string Input)
        {
            string[] SplitedString = Input.Split(' ');
            for (int i = 0; i < SplitedString.Length; i++)
            {
                if (SplitedString[i] != "")
                {
                    Tokens.Add(SplitedString[i]);
                }
            }
            Tokens.Add("$");

            return true;
        }

        private bool Poliz()
        {
            for (int i = 0; i < Tokens.Count; i++)
            {
                switch (Tokens[i])
                {
                    case "$":
                        while (Stack.Count != 0)
                        {
                            Pol_Not.Push(Stack.Pop());
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
                        string Temp = Stack.Pop();
                        while (Temp != "(")
                        {
                            Pol_Not.Push(Temp);
                            if (Stack.Count == 0)
                            {
                                return false;
                            }
                            Temp = Stack.Pop();
                        }
                        break;
                    case "+":
                        if (Stack.Count != 0)
                        {
                            while (Stack.Peek() == "+" || Stack.Peek() == "-" || Stack.Peek() == "/" || Stack.Peek() == "*" || Stack.Peek() == "^")
                            {
                                Pol_Not.Push(Stack.Pop());
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
                                Pol_Not.Push(Stack.Pop());
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
                                Pol_Not.Push(Stack.Pop());
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
                                Pol_Not.Push(Stack.Pop());
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
                                Pol_Not.Push(Stack.Pop());
                                if (Stack.Count == 0)
                                {
                                    break;
                                }
                            }
                        }
                        Stack.Push("^");
                        break;
                    default:
                        Pol_Not.Push(Tokens[i]);
                        break;
                }
            }

            return true;
        }

    }

}
