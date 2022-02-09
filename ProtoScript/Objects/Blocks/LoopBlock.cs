using System;

namespace ProtoScript.Objects.Blocks;

class LoopBlock : BlockBase
{
    public override Type RealType { get; }
    public override int Condition { get; }
    public override int End { get; }
    public int MaxLoops { get; set; }
    public string IterationVariable { get; set; }
    public bool ToBeSet { get; set; }

    public LoopBlock(int condition_pos, int end_pos)
    {
        this.RealType = typeof(LoopBlock);
        this.Condition = condition_pos;
        this.End = end_pos;
        this.ToBeSet = true;
    }
}
