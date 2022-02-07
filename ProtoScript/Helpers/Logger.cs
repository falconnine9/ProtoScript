using System;

namespace ProtoScript.Helpers
{
    class Logger
    {
        public static void LogFatal(string message) => LogMessage(message, ConsoleColor.Red);

        private static void LogMessage(string message, ConsoleColor col)
        {
            ConsoleColor original_col = Console.ForegroundColor;
            Console.ForegroundColor = col;
            Console.Error.WriteLine(message);
            Console.ForegroundColor = original_col;
        }
    }
}
