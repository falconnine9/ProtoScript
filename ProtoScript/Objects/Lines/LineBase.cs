using System;

namespace ProtoScript.Objects.Lines;

abstract class LineBase
{
    public abstract Type RealType { get; }
    public abstract int OriginLine { get; }
}
