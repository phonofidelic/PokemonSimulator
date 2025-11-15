
namespace UI
{
    public static class ConsoleUI
    {
        public static ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            set
            {
                Console.ForegroundColor = value;
            }
        }

        public static void WriteLine(string? input) => Console.WriteLine(input);
        public static void WriteLine() => Console.WriteLine();
        public static void Write(string? input) => Console.Write(input);
        public static ConsoleKeyInfo ReadKey(bool intercept) => Console.ReadKey(intercept);
        public static ConsoleKeyInfo ReadKey() => Console.ReadKey();

        public static void Clear() => Console.Clear();

        public static void ResetColor() => Console.ResetColor();

        public static void WriteError(string message)
        {
            ConsoleUI.ForegroundColor = ConsoleColor.Red;
            ConsoleUI.WriteLine(message);
            ConsoleUI.ResetColor();
        }

        public static void WriteWarning(string message)
        {
            ConsoleUI.ForegroundColor = ConsoleColor.Yellow;
            ConsoleUI.WriteLine(message);
            ConsoleUI.ResetColor();
        }
        public static void WriteInfo(string message)
        {
            ConsoleUI.ForegroundColor = ConsoleColor.Cyan;
            ConsoleUI.WriteLine(message);
            ConsoleUI.ResetColor();
        }

        public static void Debug(string message)
        {
            ConsoleUI.ForegroundColor = ConsoleColor.Cyan;
            ConsoleUI.WriteLine(message);
            ConsoleUI.ResetColor();
        }
    }
}
