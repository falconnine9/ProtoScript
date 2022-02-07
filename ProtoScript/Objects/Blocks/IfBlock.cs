using System;

namespace ProtoScript.Objects.Blocks
{
    class IfBlock : BlockBase
    {
        public override Type RealType { get; }
        public override int Condition { get; }
        public override int End { get; }

        public IfBlock(int condition_pos, int end_pos)
        {
            this.RealType = typeof(IfBlock);
            this.Condition = condition_pos;
            this.End = end_pos;
        }
    }
}
