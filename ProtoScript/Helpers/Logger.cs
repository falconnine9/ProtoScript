using System;

namespace ProtoScript.Helpers;

class Logger
{
    public static void LogFatal(string message) => _logMessage(message, ConsoleColor.Red);

    private static void _logMessage(string message, ConsoleColor col)
    {
        ConsoleColor original_col = Console.ForegroundColor;
        Console.ForegroundColor = col;
        Console.Error.WriteLine(message);
        Console.ForegroundColor = original_col;
    }
}
