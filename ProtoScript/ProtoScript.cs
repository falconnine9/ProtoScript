/* ProtoScript programming language.
 * Currently in major development and it doesn't have too many features
 * 
 * Licensed under the GNU General Public License v3.0.
 * Go to <gnu.org/licenses> for accessing and copying permissions
 */

using System;
using System.Collections.Generic;
using System.IO;

using ProtoScript.Compiler;
using ProtoScript.Objects;
using ProtoScript.Objects.Blocks;
using ProtoScript.Objects.Lines;
using ProtoScript.VM;

namespace ProtoScript
{
    class ProtoScript
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0) {
                Console.Error.WriteLine("No input file provided");
                return;
            }
            if (!File.Exists(args[0])) {
                Console.Error.WriteLine("Cannot find the input file");
                return;
            }

            string source = File.ReadAllText(args[0]);

            try {
                Executor.Code = CompileSource(args[0], ref source);
                Executor.BeginCycle();
            } catch (Exception e) {
                ConsoleColor original_col = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(e.ToString());
                Console.ForegroundColor = original_col;
            }
        }

        private static Source CompileSource(string filename, ref string source)
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
}
