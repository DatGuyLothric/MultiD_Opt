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
        private float Accuracy;
        private int N;
        private FuncParser Parser;

        public void Start()
        {
            Init();
            ReadUserData();
        }

        private void Init()
        {
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
        }

    }

}
