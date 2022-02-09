using System;
using System.Collections.Generic;

using ProtoScript.Errors;
using ProtoScript.Objects;
using ProtoScript.Objects.Arguments;
using ProtoScript.Objects.Lines;
using ProtoScript.Objects.Types;
using ProtoScript.VM.Instructions;

namespace ProtoScript.VM;

class Executor
{
    public static Source Code = null;
    public static int ExecutionLine = 0;

    public static Dictionary<string, Variable> Variables = new();

    public static Random RandomGenerator = new();

    public static void BeginCycle()
    {
        for (; ExecutionLine < Code.Lines.Length; ExecutionLine++) {
            LineBase ln = Code.Lines[ExecutionLine];

            if (ln.RealType == typeof(ConstantLine))
                _handleConstantLine(ln as ConstantLine);
            else {
                _handleExecutableLine(ln as ExecutableLine);
            }
        }
    }

    private static void _handleConstantLine(ConstantLine ln)
    {
        if (ln.ReturnVariable is null)
            return;

        if (ln.Constant.RealType == typeof(VariableArgument)) {
            string var_name = (ln.Constant as VariableArgument).Value;

            if (!Variables.TryGetValue(var_name, out Variable result))
                throw new UndefinedReferenceError("Undefined variable referenced", Code.Name, ln.OriginLine);

            _setVariable(ln.ReturnVariable, result.Value, ln);
        }
        else
            _setVariable(ln.ReturnVariable, (ln.Constant as TypeArgument).Value, ln);
    }

    private static void _handleExecutableLine(ExecutableLine ln)
    {
        int index = Array.IndexOf(Constants.Instructions, ln.Instruction);
        InstructionInfo info = Constants.InstructionData[index];

        if (info is null)
            return;

        List<ArgumentBase> new_args = new(ln.Arguments);
        for (int i = 0; i < new_args.Count; i++) {
            ArgumentBase arg = new_args[i];
            if (arg.RealType != typeof(VariableArgument))
                continue;

            string var_name = (arg as VariableArgument).Value;
            new_args[i] = Variables.TryGetValue(var_name, out Variable result)
                ? new TypeArgument(result.Value)
                : throw new UndefinedReferenceError("Unknown variable referenced", Code.Name, ln.OriginLine);
        }

        int start = info.AllowedParameters.Start.Value;
        int end = info.AllowedParameters.End.Value;
        if (new_args.Count < start || new_args.Count > end)
            throw new ArgumentError(string.Format("Instruction requires between {0} and {1] arguments", start, end), Code.Name, ln.OriginLine);

        TypeBase ret_value = info.Runner(new ExecutableLine(
            ln.Instruction,
            new_args.ToArray(),
            ln.ReturnVariable,
            ln.OriginLine
        ));

        if (ln.ReturnVariable is not null) {
            if (ret_value is null)
                throw new InvalidSetPositionError("Instruction returns no value", Code.Name, ln.OriginLine);

            _setVariable(ln.ReturnVariable, ret_value, ln);
        }
    }

    private static void _setVariable(VariableSet name, TypeBase value, LineBase ln)
    {
        if (Variables.TryGetValue(name.Name, out Variable result) && result.Const)
            throw new InvalidVariableError("Cannot set value to constant variable", Code.Name, ln.OriginLine);

        Variables[name.Name] = new Variable(value, name.Const);
    }
}
