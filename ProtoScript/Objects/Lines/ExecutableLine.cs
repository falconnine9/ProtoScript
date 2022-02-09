using System;

using ProtoScript.Objects.Arguments;
using ProtoScript.Objects.Types;

namespace ProtoScript.Objects.Lines;

class ExecutableLine : LineBase
{
    public override Type RealType { get; }
    public override int OriginLine { get; }
    public string Instruction { get; }
    public VariableSet ReturnVariable { get; }
    public ArgumentBase[] Arguments { get; set; }

    public ExecutableLine(string instruction, ArgumentBase[] value, VariableSet ret_var, int origin_line=default)
    {
        this.RealType = typeof(ExecutableLine);
        this.OriginLine = origin_line;
        this.Instruction = instruction;
        this.ReturnVariable = ret_var;
        this.Arguments = value;
    }
}
