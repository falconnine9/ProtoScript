using System.Collections.Generic;

using ProtoScript.Objects;
using ProtoScript.Objects.Blocks;
using ProtoScript.Objects.Lines;

namespace ProtoScript.Compiler;

class CompilerMain
{
    public static Source CompileSource(string filename, string source)
    {
        // Makes the source and summarizes it into lines
        FileParser fp = new(source, filename);
        AnonymousLine[] empty_lines = fp.ParseSource();
        Source initial_source = LineSummarizer.SummarizeLines(filename, empty_lines);

        // Indexing and stuff
        Dictionary<string, int> label_index = Indexers.IndexLabels(initial_source);
        Dictionary<int, BlockBase> block_index = Indexers.IndexBlocks(initial_source);

        return new Source(filename, initial_source.Lines, label_index, block_index);
    }
}
