using System;
using System.Collections.Generic;

using ProtoScript.Errors;
using ProtoScript.Objects;
using ProtoScript.Objects.Arguments;
using ProtoScript.Objects.Lines;
using ProtoScript.Objects.Types;

namespace ProtoScript.Compiler
{
    class LineSummarizer
    {
        public static Source SummarizeLines(string file, AnonymousLine[] source)
        {
            List<LineBase> lines = new();

            foreach (AnonymousLine line in source) {
                string variable = null;
                int start_pos = 0;

                if (line.Arguments.Length == 0)
                    continue;
                
                if (line.Arguments[0] == "set") {
                    if (line.Arguments.Length == 1)
                        throw new ExpectedTokenError("Expected variable name after set keyword", line.OriginFile, line.OriginLine);

                    if (!Algorithms.IsValidVariable(line.Arguments[1]))
                        throw new InvalidVariableError("Cannot set variable to instruction or keyword", line.OriginFile, line.OriginLine);

                    variable = line.Arguments[1];
                    start_pos = 2;
                }

                List<ArgumentBase> args = new();
                for (int i = start_pos + 1; i < line.Arguments.Length; i++) {
                    ArgumentBase result = TokenizeArgument(line.Arguments[i]);
                    if (result is null)
                        throw new UndefinedReferenceError("Undefined reference \"" + line.Arguments[i] + "\" in argument list", line.OriginFile, line.OriginLine);

                    args.Add(result);
                }

                ArgumentBase initial_result = TokenizeArgument(line.Arguments[start_pos]);
                if (initial_result is null) {
                    if (!Algorithms.IsValidInstruction(line.Arguments[start_pos]))
                        throw new UndefinedReferenceError("Undefined instruction referenced", line.OriginFile, line.OriginLine);

                    lines.Add(new ExecutableLine(
                        line.Arguments[start_pos],
                        args.ToArray(),
                        variable,
                        line.OriginLine
                    ));
                }
                else {
                    if (args.Count > 0)
                        throw new ArgumentError("Too many arguments given in constant line", line.OriginFile, line.OriginLine);

                    lines.Add(new ConstantLine(
                        initial_result,
                        variable,
                        line.OriginLine
                    ));
                }
            }

            return new Source(file, lines.ToArray());
        }

        public static ArgumentBase TokenizeArgument(string value)
        {
            if (value.StartsWith('"') && value.EndsWith('"'))
                return new TypeArgument(new StringType(value[1..^1]));

            else if (decimal.TryParse(value, out decimal result))
                return new TypeArgument(new NumberType(result));

            else if (value is "true" or "parse")
                return new TypeArgument(new BooleanType(value == "true"));

            else if (value.StartsWith('$'))
                return new VariableArgument(value[1..]);

            else
                return null;
        }
    }
}
