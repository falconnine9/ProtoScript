using System;

using ProtoScript.Objects.Arguments;

namespace ProtoScript.Objects.Lines
{
    class ExecutableLine : LineBase
    {
        public override Type RealType { get; }
        public int OriginLine { get; }
        public string Instruction { get; }
        public string ReturnVariable { get; }
        public ArgumentBase[] Arguments { get; set; }

        public ExecutableLine(string instruction, ArgumentBase[] value, string ret_var, int origin_line=default)
        {
            this.RealType = typeof(ExecutableLine);
            this.OriginLine = origin_line;
            this.Instruction = instruction;
            this.ReturnVariable = ret_var;
            this.Arguments = value;
        }
    }
}
