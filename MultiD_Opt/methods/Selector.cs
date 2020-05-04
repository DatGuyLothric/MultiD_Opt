using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiD_Opt.methods
{

    class Selector
    {

        private enum EOptions
        {
            SIMPLEX = 0,
            EXIT = 1,
        }

        private Dictionary<EOptions, string> Options;

        public void Start()
        {
            Init();
            Select();
        }

        private void Select()
        {
            int selected = 0;
            bool Running = true;

            while (Running)
            {
                Console.WriteLine("Выберете необходимую опцию:\n");
                for (int i = 0; i < Options.Count; i++)
                {
                    string Opt_Out = selected == i ? "> " : "";
                    string Val_Out = "";
                    Options.TryGetValue((EOptions)Enum.GetValues(typeof(EOptions)).GetValue(i), out Val_Out);
                    Console.WriteLine(Opt_Out + Val_Out);
                }
                
                ConsoleKey pressed = Console.ReadKey().Key;
                // Waiting cycle
                while (pressed != ConsoleKey.DownArrow && pressed != ConsoleKey.UpArrow && pressed != ConsoleKey.Enter) { pressed = Console.ReadKey().Key; }

                Console.Clear();
                switch (pressed)
                {
                    case ConsoleKey.DownArrow:
                        selected = selected != 1 ? selected + 1 : 0;
                        break;
                    case ConsoleKey.UpArrow:
                        selected = selected != 0 ? selected - 1 : 1;
                        break;
                    case ConsoleKey.Enter:
                        switch (selected) {
                            case 0:
                                Simplex Simplex_Inst = new Simplex();
                                Simplex_Inst.Start();
                                break;
                            case 1:
                                Running = false;
                                break;
                        }
                        break;
                }
            }
        }

        private void Init()
        {
            Options = new Dictionary<EOptions, string>();
            SetOptions();
        }

        private void SetOptions()
        {
            Options.Add(EOptions.SIMPLEX, "Минимизация симплексным методом");
            Options.Add(EOptions.EXIT, "Выход");
        }

    }

}
