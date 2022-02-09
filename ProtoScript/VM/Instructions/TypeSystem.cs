using ProtoScript.Errors;
using ProtoScript.Objects.Arguments;
using ProtoScript.Objects.Lines;
using ProtoScript.Objects.Types;

namespace ProtoScript.VM.Instructions;

class TypeSystem
{
    public static TypeBase TypeOf(ExecutableLine ln)
    {
        var value = ln.Arguments[0] as TypeArgument;
        string str_value;

        if (value.RealType == typeof(StringType))
            str_value = "string";
        else if (value.RealType == typeof(NumberType))
            str_value = "number";
        else
            str_value = "boolean";

        return new StringType(str_value);
    }

    public static TypeBase CvtString(ExecutableLine ln)
    {
        var value = ln.Arguments[0] as TypeArgument;

        if (value.Value.RealType == typeof(StringType))
            return value.Value;
        else if (value.Value.RealType == typeof(NumberType))
            return new StringType((value.Value as NumberType).ToString());
        else
            return new StringType((value.Value as BooleanType).ToString());
    }

    public static TypeBase CvtNumber(ExecutableLine ln)
    {
        var value = ln.Arguments[0] as TypeArgument;

        if (value.Value.RealType == typeof(StringType)) {
            return decimal.TryParse((value.Value as StringType).Value, out decimal result)
                ? new NumberType(result)
                : throw new ValueError("Cannot convert every character to base 10 value", Executor.Code.Name, ln.OriginLine);
        }
        else if (value.Value.RealType == typeof(NumberType))
            return value.Value;
        else
            return new NumberType((value.Value as BooleanType).Value ? 1 : 0);
    }

    public static TypeBase CvtBoolean(ExecutableLine ln)
    {
        var value = ln.Arguments[0] as TypeArgument;

        if (value.Value.RealType == typeof(StringType)) {
            string str_value = (value.Value as StringType).Value;
            return str_value is "true" or "false"
                ? new BooleanType(str_value == "true")
                : throw new ValueError("Can only convert \"true\" and \"false\" string values to booleans", Executor.Code.Name, ln.OriginLine);
        }
        else if (value.Value.RealType == typeof(NumberType))
            return new BooleanType((value.Value as NumberType).Value >= 1);
        else
            return value.Value;
    }
}
