using System;

using ProtoScript.Objects.Arguments;

namespace ProtoScript.Objects.Lines
{
    class ConstantLine : LineBase
    {
        public override Type RealType { get; }
        public int OriginLine { get; }
        public string ReturnVariable { get; }
        public ArgumentBase Constant { get; }

        public ConstantLine(ArgumentBase value, string ret_var, int origin_line=default)
        {
            this.RealType = typeof(ConstantLine);
            this.OriginLine = origin_line;
            this.ReturnVariable = ret_var;
            this.Constant = value;
        }
    }
}
