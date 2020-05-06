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
        private int SimplexD;
        private double SimplexLength;
        private Dictionary<string, double> SimplexStartCoords;
        private FuncParser Parser;

        public void Start()
        {
            Init();
            ReadUserData();


            // TODO: Move to end func
            Console.Clear();
        }

        private void Init()
        {
            SimplexStartCoords = new Dictionary<string, double>();
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
            List<string> tempVariables = new List<string>(Parser.GetVariables());
            SimplexD = tempVariables.Count;
            Console.WriteLine("Размерность n = " + SimplexD);
            Accuracy = ReadDoubleValue("e", "точность e");
            for (int i = 0; i < SimplexD; i++)
            {
                SimplexStartCoords.Add(tempVariables[i], ReadDoubleValue(tempVariables[i], "координату " + tempVariables[i] + " начальной точки симплекса"));
            }
            SimplexLength = ReadDoubleValue("m", "длину ребра симплекса m");
        }

        private double ReadDoubleValue(string name, string text)
        {
            double value;
            Console.Write("Введите " + text + ": ");
            while (true)
            {
                try
                {
                    value = Convert.ToDouble(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.Write("Ввод содержит ошибки, попробуйте ввести значение " + name + " снова: ");
                }
            }
            return value;
        }

    }

}
