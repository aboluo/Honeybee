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
        static List<Function> Functions;

        static void Main(string[] args)
        {
            Functions = ParseFunctions();

            Console.WriteLine("Found functions: " + Functions.Count);

            int found = 0;

            foreach (Function func in Functions)
            {
                if (func.ToString().Contains("\"Login\", \"socket\""))
                {

                    Console.WriteLine("Login handler found: " + func.Name);
                    found++;
                }
            }

            Console.WriteLine("Functions found: " + found);

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

        static string[] ReadLines()
        {
            string[] Lines = File.ReadAllLines(READ_SCRIPTS);
            string[] FormattedLines = new string[Lines.Length];

            for (int i = 0; i < Lines.Length; i++)
            {
                FormattedLines[i] = Lines[i].Replace("    ", "");
            }

            return Lines;
        }
    }
}
