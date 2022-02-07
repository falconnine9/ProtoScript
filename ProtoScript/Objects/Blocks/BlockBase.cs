using System;

namespace ProtoScript.Objects.Blocks
{
    abstract class BlockBase
    {
        public abstract Type RealType { get; }
        public abstract int Condition { get; }
        public abstract int End { get; }
    }
}
