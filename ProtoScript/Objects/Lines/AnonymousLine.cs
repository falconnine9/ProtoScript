using System;

namespace ProtoScript.Objects.Lines;

class AnonymousLine : LineBase
{
    public override Type RealType { get; }
    public override int OriginLine { get; }
    public string OriginFile { get; }
    public string[] Arguments { get; set; }

    public AnonymousLine(string[] args, string origin_file = default, int origin_line = default)
    {
        this.RealType = typeof(AnonymousLine);
        this.OriginFile = origin_file;
        this.OriginLine = origin_line;
        this.Arguments = args;
    }
}
