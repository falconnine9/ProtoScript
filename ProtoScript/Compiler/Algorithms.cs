using System;
using System.Linq;

namespace ProtoScript.Compiler
{
    class Algorithms
    {
        public static bool IsValidVariable(string name)
        {
            return !(Constants.Instructions.Contains(name) || Constants.Keywords.Contains(name));
        }

        public static bool IsValidInstruction(string name)
        {
            return Constants.Instructions.Contains(name);
        }
    }
}
