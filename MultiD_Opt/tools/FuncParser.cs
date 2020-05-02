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

        public string Function;
        private List<string> Tokens = new List<string>();
        private Stack<string> Stack = new Stack<string>();
        private Stack<string> Pol_Not = new Stack<string>();

        public bool Read(string Input)
        {
            Function = Input;
            Tokenize(Input);
            bool Result = Poliz();
            Result = Result == true ? Calculate(true) == 47.47474747 ? false : true : false;

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
            Stack.Clear();
            Pol_Not.Clear();
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

            Stack<string> Temp_Stack = new Stack<string>(Pol_Not);
            Pol_Not.Clear();
            while (Temp_Stack.Count != 0)
            {
                Pol_Not.Push(Temp_Stack.Pop());
            }

            return true;
        }

        public double Calculate(bool Check = false)
        {
            Dictionary<string, double> Unknowns = new Dictionary<string, double>();
            Stack<string> Temp_Stack = new Stack<string>(Pol_Not);
            Stack<double> Calc_Stack = new Stack<double>();
            int Temp_Cycle_Count = Temp_Stack.Count;

            for (int i = 0; i < Temp_Cycle_Count; i++)
            {
                if (Temp_Stack.Count == 0)
                {
                    return 47.47474747;
                }
                string Temp_Var = Temp_Stack.Pop();
                double Temp_A;
                double Temp_B;
                switch (Temp_Var)
                {
                    case "+":
                        if (Calc_Stack.Count == 0 || Calc_Stack.Count == 1)
                        {
                            return 47.47474747;
                        }
                        Temp_A = Calc_Stack.Pop();
                        Temp_B = Calc_Stack.Pop();
                        Calc_Stack.Push(Temp_B + Temp_A);
                        break;
                    case "-":
                        if (Calc_Stack.Count == 0 || Calc_Stack.Count == 1)
                        {
                            return 47.47474747;
                        }
                        Temp_A = Calc_Stack.Pop();
                        Temp_B = Calc_Stack.Pop();
                        Calc_Stack.Push(Temp_B - Temp_A);
                        break;
                    case "*":
                        if (Calc_Stack.Count == 0 || Calc_Stack.Count == 1)
                        {
                            return 47.47474747;
                        }
                        Temp_A = Calc_Stack.Pop();
                        Temp_B = Calc_Stack.Pop();
                        Calc_Stack.Push(Temp_B * Temp_A);
                        break;
                    case "/":
                        if (Calc_Stack.Count == 0 || Calc_Stack.Count == 1)
                        {
                            return 47.47474747;
                        }
                        Temp_A = Calc_Stack.Pop();
                        Temp_B = Calc_Stack.Pop();
                        if (Temp_A != 0)
                        {
                            Calc_Stack.Push(Temp_B / Temp_A);
                        }
                        else
                        {
                            return 47.47474747;
                        }
                        break;
                    case "^":
                        if (Calc_Stack.Count == 0 || Calc_Stack.Count == 1)
                        {
                            return 47.47474747;
                        }
                        Temp_A = Calc_Stack.Pop();
                        Temp_B = Calc_Stack.Pop();
                        Calc_Stack.Push(Math.Pow(Calc_Stack.Pop(), Calc_Stack.Pop()));
                        break;
                    default:
                        double Temp_Conv = 0;
                        try
                        {
                            Temp_Conv = Convert.ToDouble(Temp_Var);
                        }
                        catch (Exception e)
                        {
                            if (Check)
                            {
                                Temp_Conv = 0;
                            }
                            else
                            {
                                if (Unknowns.Count == 0)
                                {
                                    Unknowns.Add(Temp_Var, Unknown_Input(Temp_Var));
                                }
                                else
                                {
                                    for (int j = 0; j < Unknowns.Count + 1; j++)
                                    {
                                        if (Unknowns.Keys.Contains(Temp_Var))
                                        {
                                            Unknowns.TryGetValue(Temp_Var, out Temp_Conv);
                                            break;
                                        }
                                        if (j == Unknowns.Count)
                                        {
                                            Temp_Conv = Unknown_Input(Temp_Var);
                                            Unknowns.Add(Temp_Var, Temp_Conv);
                                        }
                                    }
                                }
                            }
                        }
                        Calc_Stack.Push(Temp_Conv);
                        break;
                }
            }

            if (Calc_Stack.Count != 1)
            {
                return 47.47474747;
            }

            return 0;
        }

        private double Unknown_Input(string Temp_Var)
        {
            Console.Write("Введите значение для переменной " + Temp_Var + ": ");
            double Temp_Input = 0;
            while (true)
            {
                try
                {
                    Temp_Input = Convert.ToDouble(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.Write("Вы ввели некорректное значение для переменной " + Temp_Var + ", попробуйте снова: ");
                }
            }
            return Temp_Input;
        }

    }

}
