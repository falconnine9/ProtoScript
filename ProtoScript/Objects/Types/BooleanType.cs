using System;

namespace ProtoScript.Objects.Types
{
    class BooleanType : TypeBase
    {
        public override Type RealType { get; }
        public bool Value { get; set; }

        public BooleanType(bool value)
        {
            this.RealType = typeof(BooleanType);
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Value ? "true" : "false";
        }
    }
}
