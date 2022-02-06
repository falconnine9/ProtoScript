using System;

using ProtoScript.Errors;
using ProtoScript.Objects.Arguments;
using ProtoScript.Objects.Lines;
using ProtoScript.Objects.Types;

namespace ProtoScript.VM.Instructions
{
    class StringIO
    {
        public static TypeBase Concat(ExecutableLine ln)
        {
            string a;
            string b;
            try {
                a = ((ln.Arguments[0] as TypeArgument).Value as StringType).Value;
                b = ((ln.Arguments[1] as TypeArgument).Value as StringType).Value;
            } catch (NullReferenceException) {
                throw new ArgumentError("Concat instruction takes (string, string) arguments", Executor.Code.Name, ln.OriginLine);
            }

            return new StringType(a + b);
        }

        public static TypeBase Insert(ExecutableLine ln)
        {
            string original;
            string insert_value;
            int index;
            try {
                original = ((ln.Arguments[0] as TypeArgument).Value as StringType).Value;
                insert_value = ((ln.Arguments[1] as TypeArgument).Value as StringType).Value;
                index = (int)((ln.Arguments[2] as TypeArgument).Value as NumberType).Value;
            } catch (NullReferenceException) {
                throw new ArgumentError("Insert instruction takes (string, string, number) arguments", Executor.Code.Name, ln.OriginLine);
            }

            return index < 0 || index >= original.Length
                ? throw new ArgumentError("Index is outside the bounds of the string", Executor.Code.Name, ln.OriginLine)
                : new StringType(original.Insert(index, insert_value));
        }

        public static TypeBase Len(ExecutableLine ln)
        {
            string str_value;
            try {
                str_value = ((ln.Arguments[0] as TypeArgument).Value as StringType).Value;
            }
            catch (NullReferenceException) {
                throw new ArgumentError("Len instruction takes (string) arguments", Executor.Code.Name, ln.OriginLine);
            }

            return new NumberType(str_value.Length);
        }

        public static TypeBase Substring(ExecutableLine ln)
        {
            string str_value;
            int start;
            int length;
            try {
                str_value = ((ln.Arguments[0] as TypeArgument).Value as StringType).Value;
                start = (int)((ln.Arguments[1] as TypeArgument).Value as NumberType).Value;
                length = (int)((ln.Arguments[2] as TypeArgument).Value as NumberType).Value;
            } catch (NullReferenceException) {
                throw new ArgumentError("Insert instruction takes (string, number, number) arguments", Executor.Code.Name, ln.OriginLine);
            }

            return start < 0 || start >= str_value.Length || length + start > str_value.Length
                ? throw new ArgumentError("Substring left the bounds of the string", Executor.Code.Name, ln.OriginLine)
                : new StringType(str_value.Substring(start, length));
        }
    }
}
