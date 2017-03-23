using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honeybee
{
    class Function
    {
        public List<string> Lines;
        public string FullName;
        public string Name;

        private string FunctionString;
        private string ArrayRegistrar;

        public Function(List<string> Lines)
        {
            this.Lines = Lines;
            this.FullName = Lines[0];

            string temp = this.FullName.Split('(')[0];
            string[] spaces = temp.Split(' ');

            this.Name = spaces[spaces.Length - 1];
            this.FunctionString = String.Join("\n", Lines);
        }

        public void GetHeader()
        {
            foreach (string Line in Program.ReadLines())
            {
                if (Line.Contains(".addHabboConnectionMessageEvent(") &&
                    Line.Contains("this." + this.Name + ")"))
                {

                    ArrayRegistrar = Line.Split('(')[1].Split(' ')[1];
                }

            }

            foreach (string Line in Program.ReadLines())
            {
                if (Line.Contains("] = " + ArrayRegistrar + ";"))
                {

                    Console.WriteLine("Found header: " + Line);
                }

            }

        }

        public override string ToString()
        {
            return FunctionString;
        }
    }
}
