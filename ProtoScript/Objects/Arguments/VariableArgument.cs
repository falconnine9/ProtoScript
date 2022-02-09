using System;

namespace ProtoScript.Objects.Arguments;

class VariableArgument : ArgumentBase
{
    public override Type RealType { get; }
    public string Value { get; }

    public VariableArgument(string value)
    {
        this.RealType = typeof(VariableArgument);
        this.Value = value;
    }
}
