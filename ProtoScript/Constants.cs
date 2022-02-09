using ProtoScript.Objects.Lines;
using ProtoScript.Objects.Types;
using ProtoScript.VM.Instructions;

namespace ProtoScript;

class Constants
{
    public delegate TypeBase InstructionExecutor(ExecutableLine ln);

    public static readonly string[] Instructions = {
        // Arithmetic
        "+",
        "-",
        "*",
        "/",
        "%",
        "**",

        // Blocks
        "end",
        "if",
        "loop",

        // Compilation Controllers
        "import",

        // ConsoleIO
        "backcolor",
        "concls",
        "forecolor",
        "getkey",
        "input",
        "print",
        "println",

        // Execution Controllers
        "exit",
        "jump",
        "label",
        "throw",
        "wait",

        // Logic
        "!",
        "=",
        "!=",
        "<",
        ">",
        "<=",
        ">=",
        "||",
        "&&",

        // Random
        "randint",

        // StringIO
        "concat",
        "getchar",
        "insert",
        "len",
        "substring",
        
        // Type System
        "tobool",
        "tonum",
        "tostr",
        "typeof"
    };

    public static readonly string[] Keywords = {
        "set",
        "const"
    };

    public static readonly string[] BlockInstructions = {
        "if",
        "loop"
    };

    public static readonly InstructionInfo[] InstructionData = {
        // Arithmetic
        new InstructionInfo(Arithmetic.AddOp, 2..2),
        new InstructionInfo(Arithmetic.SubOp, 2..2),
        new InstructionInfo(Arithmetic.MultiOp, 2..2),
        new InstructionInfo(Arithmetic.DivOp, 2..2),
        new InstructionInfo(Arithmetic.RemainderOp, 2..2),
        new InstructionInfo(Arithmetic.PowerOp, 2..2),

        // Blocks
        new InstructionInfo(BlockHandlers.HandleBlockEnd, 0..0),
        new InstructionInfo(BlockHandlers.HandleIf, 1..1),
        new InstructionInfo(BlockHandlers.HandleLoop, 1..1),

        // Compilation Controllers
        null, // Import

        // ConsoleIO
        new InstructionInfo(ConsoleIO.BackColor, 0..1),
        new InstructionInfo(ConsoleIO.ConCls, 0..0),
        new InstructionInfo(ConsoleIO.ForeColor, 0..1),
        new InstructionInfo(ConsoleIO.GetKey, 0..0),
        new InstructionInfo(ConsoleIO.Input, 0..1),
        new InstructionInfo(ConsoleIO.Print, 1..1),
        new InstructionInfo(ConsoleIO.PrintLn, 1..1),

        // Execution Controllers
        new InstructionInfo(ExecutionCtrl.Exit, 0..0),
        new InstructionInfo(ExecutionCtrl.Jump, 1..1),
        null, // Label
        new InstructionInfo(ExecutionCtrl.Throw, 2..2),
        new InstructionInfo(ExecutionCtrl.Wait, 1..1),

        // Logic
        new InstructionInfo(Logic.NotOp, 1..1),
        new InstructionInfo(Logic.EqualOp, 2..2),
        new InstructionInfo(Logic.NotEqualOp, 2..2),
        new InstructionInfo(Logic.LessThan, 2..2),
        new InstructionInfo(Logic.GreaterThan, 2..2),
        new InstructionInfo(Logic.LessThanEqualTo, 2..2),
        new InstructionInfo(Logic.GreaterThanEqualTo, 2..2),
        new InstructionInfo(Logic.OrOp, 2..2),
        new InstructionInfo(Logic.AndOp, 2..2),

        // Random
        new InstructionInfo(Rand.RandInt, 2..2),

        // StringIO
        new InstructionInfo(StringIO.Concat, 2..2),
        new InstructionInfo(StringIO.GetChar, 2..2),
        new InstructionInfo(StringIO.Insert, 3..3),
        new InstructionInfo(StringIO.Len, 1..1),
        new InstructionInfo(StringIO.Substring, 3..3),

        // Type System
        new InstructionInfo(TypeSystem.CvtBoolean, 1..1),
        new InstructionInfo(TypeSystem.CvtNumber, 1..1),
        new InstructionInfo(TypeSystem.CvtString, 1..1),
        new InstructionInfo(TypeSystem.TypeOf, 1..1),
    };
}
