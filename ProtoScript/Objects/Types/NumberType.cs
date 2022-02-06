using System;

namespace ProtoScript.Objects.Types
{
    class NumberType : TypeBase
    {
        public override Type RealType { get; }
        public decimal Value { get; set; }

        public NumberType(decimal value)
        {
            this.RealType = typeof(NumberType);
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
