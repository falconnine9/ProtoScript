/* ProtoScript programming language.
 * Currently in major development and it doesn't have too many features
 * 
 * Licensed under the GNU General Public License v3.0.
 * Go to <gnu.org/licenses> for accessing and copying permissions
 */

using System;
using System.IO;

using ProtoScript.Compiler;
using ProtoScript.Helpers;
using ProtoScript.VM;

namespace ProtoScript;

class ProtoScript
{
    public static void Main(string[] args)
    {
        if (args.Length == 0) {
            Logger.LogFatal("No input file provided");
            return;
        }
        if (!File.Exists(args[0])) {
            Logger.LogFatal("Cannot find the input file");
            return;
        }

        string source = File.ReadAllText(args[0]);

        try {
            Executor.Code = CompilerMain.CompileSource(args[0], source);
            VariableConstants.SetConstantVariables();
            Executor.BeginCycle();
        }
        catch (Exception e) {
            Logger.LogFatal(e.ToString());
        }
    }
}
