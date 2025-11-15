
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
        public static void Write(string? input) => Console.Write(input);
        public static ConsoleKeyInfo ReadKey(bool intercept) => Console.ReadKey(intercept);

        public static void Clear() => Console.Clear();

        public static void ResetColor() => Console.ResetColor();

        public static void Debug(string message)
        {
            ConsoleUI.ForegroundColor = ConsoleColor.Cyan;
            ConsoleUI.WriteLine(message);
            ConsoleUI.ResetColor();
        }
    }
}
