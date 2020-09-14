using System;
using System.Collections.Generic;

namespace expr
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator compiler = new Generator();
            // compiler.Feed("123 + 345");
            // var ans = compiler.Evaluate();

            string input = RequestInput();
            while(!input.Equals("exit")) {
                try {
                    compiler.Feed(input);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("     {0}", compiler.Evaluate());
                    Console.ResetColor();
                } catch (Exception e) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }
                input = RequestInput();
                Console.ResetColor();
            }
        }

        static string RequestInput()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("⁡⁡⁡⍺ → ");
            return Console.ReadLine();
        }
    }
}
