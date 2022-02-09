using System.Collections.Generic;

using ProtoScript.Objects.Blocks;
using ProtoScript.Objects.Lines;

namespace ProtoScript.Objects;

class Source
{
    public string Name { get; }
    public LineBase[] Lines { get; set; }
    public Dictionary<string, int> LabelIndex { get; }
    public Dictionary<int, BlockBase> BlockIndex { get; }

    public Source(string name, LineBase[] lines)
    {
        this.Name = name;
        this.Lines = lines;
        this.LabelIndex = new Dictionary<string, int>();
        this.BlockIndex = new Dictionary<int, BlockBase>();
    }

    public Source(string name, LineBase[] lines, Dictionary<string, int> labels, Dictionary<int, BlockBase> blocks)
    {
        this.Name = name;
        this.Lines = lines;
        this.LabelIndex = labels;
        this.BlockIndex = blocks;
    }
}
