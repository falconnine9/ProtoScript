using System;
using System.Collections.Generic;

using ProtoScript.Errors;
using ProtoScript.Objects;
using ProtoScript.Objects.Arguments;
using ProtoScript.Objects.Lines;
using ProtoScript.Objects.Types;

namespace ProtoScript.Compiler
{
    class Indexers
    {
        public static Dictionary<string, int> IndexLabels(ref Source source)
        {
            Dictionary<string, int> index = new();

            for (int i = 0; i < source.Lines.Length; i++) {
                LineBase ln = source.Lines[i];
                if (ln.RealType != typeof(ExecutableLine))
                    continue;

                ExecutableLine real = ln as ExecutableLine;
                if (real.Instruction != "label")
                    continue;
                if (real.ReturnVariable is not null)
                    throw new InvalidVariableError("Cannot set variable at this location", source.Name, real.OriginLine);

                if (real.Arguments.Length != 1)
                    throw new ArgumentError("Label instruction requires 1 argument", source.Name, real.OriginLine);

                string label_name;
                try {
                    label_name = ((real.Arguments[0] as TypeArgument).Value as StringType).Value;
                } catch (NullReferenceException) {
                    throw new ArgumentError("Label instruction requires (string) arguments", source.Name, real.OriginLine);
                }

                index.Add(label_name, i);
            }

            return index;
        }
    }
}
