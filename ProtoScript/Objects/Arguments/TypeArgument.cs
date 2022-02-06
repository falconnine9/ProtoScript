using System;

using ProtoScript.Objects.Types;

namespace ProtoScript.Objects.Arguments
{
    class TypeArgument : ArgumentBase
    {
        public override Type RealType { get; }
        public TypeBase Value { get;  }

        public TypeArgument(TypeBase value)
        {
            this.RealType = typeof(TypeArgument);
            this.Value = value;
        }
    }
}
