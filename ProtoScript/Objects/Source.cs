using System;
using System.Collections.Generic;

using ProtoScript.Objects.Lines;

namespace ProtoScript.Objects
{
    class Source
    {
        public string Name { get; }
        public LineBase[] Lines { get; set; }
        public Dictionary<string, int> LabelIndex { get; }

        public Source(string name, LineBase[] lines)
        {
            this.Name = name;
            this.Lines = lines;
            this.LabelIndex = new Dictionary<string, int>();
        }

        public Source(string name, LineBase[] lines, Dictionary<string, int> labels)
        {
            this.Name = name;
            this.Lines = lines;
            this.LabelIndex = labels;
        }
    }
}
