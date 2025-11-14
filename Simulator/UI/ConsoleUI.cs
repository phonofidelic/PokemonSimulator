using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.UI
{
    public static class ConsoleUI
    {
        public static ConsoleColor ForegroundColor {
            get => Console.ForegroundColor;
            internal set {
                Console.ForegroundColor = value; 
            }
        }

        public static void WriteLine(string? input) => Console.WriteLine(input);
        public static void Write(string? input) => Console.Write(input);
        public static ConsoleKeyInfo ReadKey(bool intercept) => Console.ReadKey(intercept);

        internal static void Clear() => Console.Clear();

        internal static void ResetColor() => Console.ResetColor();
    }
}
