using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Honeybee
{
    class Program
    {
        static string READ_SCRIPTS = "Icarus_scripts.txt";

        static string[] _ReadLines = null;
        static List<Function> Functions;

        static void Main(string[] args)
        {
            Functions = ParseFunctions();

            Console.WriteLine("Found functions: " + Functions.Count + "\n");
            
            int found = 0;

            foreach (Function func in Functions)
            {

                if (func.ToString().Contains("\"Login\", \"socket\""))
                {

                    Console.WriteLine("Login handler found: " + func.Name);
                    func.GetHeader();
                    found++;
                }
                
            }

            Console.WriteLine("\nFunctions found: " + found);

            Console.Read();
        }

        static List<Function> ParseFunctions()
        {
            List<Function> temp = new List<Function>();

            string[] Lines = ReadLines();

            for (int i = 0; i < Lines.Length; i++)
            {
                string Line = Lines[i];

                if (IsLineFunction(Line))
                {

                    List<string> FunctionLines = new List<string>();
                    FunctionLines.Add(Line);

                    int count = 1;

                    while ((i + count) < Lines.Length && !IsLineFunction(Lines[i + count]))
                    {
                        string NextLine = Lines[i + count];
                        FunctionLines.Add(NextLine);
                        count++;
                    }


                    temp.Add(new Function(FunctionLines));
                }
            }

            return temp;
        }

        public static bool IsLineFunction(string Line)
        {
            return Line.Contains("function ") && !Line.Contains(", function ") && !Line.Contains("(function ")
                    && (Line.Contains("():") || (Line.Contains("(k:")));
        }

        public static string[] ReadLines()
        {
            if (_ReadLines == null)
            {
                string[] Lines = File.ReadAllLines(READ_SCRIPTS);
                _ReadLines = new string[Lines.Length];

                for (int i = 0; i < Lines.Length; i++)
                {
                    _ReadLines[i] = Lines[i].Replace("    ", "");
                }
            }

            return _ReadLines;
        }
    }
}
