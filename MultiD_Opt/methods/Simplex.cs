using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiD_Opt.methods
{

    class Simplex
    {

        private string Function;
        private float Accuracy;
        private int N;

        public void Start()
        {
            Init();
            ReadUserData();
        }

        private void Init()
        {
            // TODO
        }

        private void ReadUserData()
        {
            Console.Clear();
            Console.WriteLine("Оптимизация симплексный методом!\n");
            Console.Write("Введите целевую функцию:\nf = ");

        }

    }

}
