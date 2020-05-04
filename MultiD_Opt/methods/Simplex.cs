using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MultiD_Opt.tools;

namespace MultiD_Opt.methods
{

    // @Method //
    class Simplex
    {

        private string Function;
        private double Accuracy;
        private int Simplex_D;
        private double Simplex_Length;
        private Dictionary<string, double> Simplex_Start_Coords;
        private FuncParser Parser;

        public void Start()
        {
            Init();
            ReadUserData();
        }

        private void Init()
        {
            Simplex_Start_Coords = new Dictionary<string, double>();
            Parser = new FuncParser();
        }

        private void ReadUserData()
        {
            Console.Clear();
            Console.WriteLine("Оптимизация симплексный методом!\n");
            Console.Write("Введите целевую функцию:\nf = ");
            Function = Console.ReadLine();
            while (true)
            {
                if (!Parser.Read(Function))
                {
                    Console.Write("Введенная вами функция содержит ошибки, повторите ввод:\nf = ");
                    Function = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }
            List<string> Temp_Variables = new List<string>(Parser.Get_Variables());
            Simplex_D = Temp_Variables.Count;
            Console.WriteLine("Размерность n = " + Simplex_D);
            Console.Write("Введите точность e: ");
            while (true)
            {
                try
                {
                    Accuracy = Convert.ToDouble(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.Write("Ввод содержит ошибки, попробуйте ввести значение e снова: ");
                }
            }
            for (int i = 0; i < Simplex_D; i++)
            {
                Console.Write("Введите координату " + Temp_Variables[i] + " начальной точки симплекса: ");
                while (true)
                {
                    try
                    {
                        Simplex_Start_Coords.Add(Temp_Variables[i], Convert.ToDouble(Console.ReadLine()));
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.Write("Ввод содержит ошибки, попробуйте ввести значение " + Temp_Variables[i]  + " снова: ");
                    }
                }
            }
            Console.Write("Введите длину ребра симплекса m: ");
            while (true)
            {
                try
                {
                    Simplex_Length = Convert.ToDouble(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.Write("Ввод содержит ошибки, попробуйте ввести значение m снова: ");
                }
            }
        }

    }

}
