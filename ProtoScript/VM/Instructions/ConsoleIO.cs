using System;

using ProtoScript.Errors;
using ProtoScript.Objects.Arguments;
using ProtoScript.Objects.Lines;
using ProtoScript.Objects.Types;

namespace ProtoScript.VM.Instructions
{
    class ConsoleIO
    {
        public static TypeBase BackColor(ExecutableLine ln)
        {
            if (ln.Arguments.Length == 1) {
                string color_str;
                try {
                    color_str = ((ln.Arguments[0] as TypeArgument).Value as StringType).Value;
                } catch (NullReferenceException) {
                    throw new ArgumentError("Backcolor instruction takes (string) arguments", Executor.Code.Name, ln.OriginLine);
                }
                Console.BackgroundColor = GetColorFromName(color_str);
            }

            return new StringType(GetNameFromColor(Console.BackgroundColor));
        }

        public static TypeBase ForeColor(ExecutableLine ln)
        {
            if (ln.Arguments.Length == 1) {
                string color_str;
                try {
                    color_str = ((ln.Arguments[0] as TypeArgument).Value as StringType).Value;
                } catch (NullReferenceException) {
                    throw new ArgumentError("Forecolor instruction takes (string) arguments", Executor.Code.Name, ln.OriginLine);
                }
                Console.ForegroundColor = GetColorFromName(color_str);
            }

            return new StringType(GetNameFromColor(Console.ForegroundColor));
        }

        public static TypeBase GetKey(ExecutableLine _)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            return new StringType(key.KeyChar.ToString());
        }

        public static TypeBase Input(ExecutableLine ln)
        {
            if (ln.Arguments.Length >= 1) {
                string arg;
                try {
                    arg = ((ln.Arguments[0] as TypeArgument).Value as StringType).Value;
                } catch (NullReferenceException) {
                    throw new ArgumentError("Input instruction takes (string) arguments", Executor.Code.Name, ln.OriginLine);
                }
                Console.Write(arg);
            }

            string user_input = Console.ReadLine();
            return new StringType(user_input);
        }

        public static TypeBase Print(ExecutableLine ln)
        {
            string arg;
            try {
                arg = ((ln.Arguments[0] as TypeArgument).Value as StringType).Value;
            } catch (NullReferenceException) {
                throw new ArgumentError("Print instruction takes (string) arguments", Executor.Code.Name, ln.OriginLine);
            }

            Console.Write(arg);
            return null;
        }

        public static TypeBase PrintLn(ExecutableLine ln)
        {
            string arg;
            try {
                arg = ((ln.Arguments[0] as TypeArgument).Value as StringType).Value;
            }
            catch (NullReferenceException) {
                throw new ArgumentError("Println instruction takes (string) arguments", Executor.Code.Name, ln.OriginLine);
            }

            Console.WriteLine(arg);
            return null;
        }

        private static ConsoleColor GetColorFromName(string name) => name switch
        {
            "black" => ConsoleColor.Black,
            "blue" => ConsoleColor.Blue,
            "cyan" => ConsoleColor.Cyan,
            "dark_blue" => ConsoleColor.DarkBlue,
            "dark_cyan" => ConsoleColor.DarkCyan,
            "dark_gray" => ConsoleColor.DarkGray,
            "dark_green" => ConsoleColor.DarkGreen,
            "dark_magenta" => ConsoleColor.DarkMagenta,
            "dark_red" => ConsoleColor.DarkRed,
            "dark_yellow" => ConsoleColor.DarkYellow,
            "gray" => ConsoleColor.Gray,
            "green" => ConsoleColor.Green,
            "magenta" => ConsoleColor.Magenta,
            "red" => ConsoleColor.Red,
            "white" => ConsoleColor.White,
            "yellow" => ConsoleColor.Yellow,
            _ => ConsoleColor.Black
        };

        private static string GetNameFromColor(ConsoleColor color) => color switch
        {
            ConsoleColor.Black => "black",
            ConsoleColor.Blue => "blue",
            ConsoleColor.Cyan => "cyan",
            ConsoleColor.DarkBlue => "dark_blue",
            ConsoleColor.DarkCyan => "dark_cyan",
            ConsoleColor.DarkGray => "dark_gray",
            ConsoleColor.DarkGreen => "dark_green",
            ConsoleColor.DarkMagenta => "dark_magenta",
            ConsoleColor.DarkRed => "dark_red",
            ConsoleColor.DarkYellow => "dark_yellow",
            ConsoleColor.Gray => "gray",
            ConsoleColor.Green => "green",
            ConsoleColor.Magenta => "magenta",
            ConsoleColor.Red => "red",
            ConsoleColor.White => "white",
            ConsoleColor.Yellow => "yellow",
            _ => "black"
        };
    }
}