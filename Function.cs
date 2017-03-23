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

        public Function(List<string> Lines)
        {
            this.Lines = Lines;
            this.FullName = Lines[0];
            this.Name = this.FullName.Split('(')[0];
            this.FunctionString = String.Join("\n", Lines);
        }

        public override string ToString()
        {
            return FunctionString;
        }
    }
}
