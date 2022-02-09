using System;

using ProtoScript.Errors;
using ProtoScript.Objects.Arguments;
using ProtoScript.Objects.Lines;
using ProtoScript.Objects.Types;

namespace ProtoScript.VM.Instructions;

class Logic
{
    private delegate bool _relationalOperator(decimal a, decimal b);
    private delegate bool _logicOperator(bool a, bool b);

    public static TypeBase NotOp(ExecutableLine ln)
    {
        bool value;
        try {
            value = ((ln.Arguments[0] as TypeArgument).Value as BooleanType).Value;
        }
        catch (NullReferenceException) {
            throw new ArgumentError("Not operator takes (bool) arguments", Executor.Code.Name, ln.OriginLine);
        }

        return new BooleanType(value);
    }

    public static TypeBase EqualOp(ExecutableLine ln)
    {
        TypeBase a = (ln.Arguments[0] as TypeArgument).Value;
        TypeBase b = (ln.Arguments[1] as TypeArgument).Value;

        if (a.RealType != b.RealType)
            throw new ArgumentError("Equality cannot be evaluated between 2 types", Executor.Code.Name, ln.OriginLine);

        if (a.RealType == typeof(StringType))
            return new BooleanType((a as StringType).Value == (b as StringType).Value);
        else if (a.RealType == typeof(NumberType))
            return new BooleanType((a as NumberType).Value == (b as NumberType).Value);
        else
            return new BooleanType((a as BooleanType).Value == (b as BooleanType).Value);
    }

    public static TypeBase NotEqualOp(ExecutableLine ln)
    {
        TypeBase a = (ln.Arguments[0] as TypeArgument).Value;
        TypeBase b = (ln.Arguments[1] as TypeArgument).Value;

        if (a.RealType == b.RealType)
            throw new ArgumentError("Equality cannot be evaluated between 2 types", Executor.Code.Name, ln.OriginLine);

        if (a.RealType == typeof(StringType))
            return new BooleanType((a as StringType).Value != (b as StringType).Value);
        else if (a.RealType == typeof(NumberType))
            return new BooleanType((a as NumberType).Value != (b as NumberType).Value);
        else
            return new BooleanType((a as BooleanType).Value != (b as BooleanType).Value);
    }

    public static TypeBase LessThan(ExecutableLine ln) => InequalityBase(ln, (decimal a, decimal b) => a < b);

    public static TypeBase GreaterThan(ExecutableLine ln) => InequalityBase(ln, (decimal a, decimal b) => a > b);

    public static TypeBase LessThanEqualTo(ExecutableLine ln) => InequalityBase(ln, (decimal a, decimal b) => a <= b);

    public static TypeBase GreaterThanEqualTo(ExecutableLine ln) => InequalityBase(ln, (decimal a, decimal b) => a >= b);

    public static TypeBase AndOp(ExecutableLine ln) => LogicBase(ln, (bool a, bool b) => a && b);

    public static TypeBase OrOp(ExecutableLine ln) => LogicBase(ln, (bool a, bool b) => a || b);

    private static BooleanType InequalityBase(ExecutableLine ln, _relationalOperator op)
    {
        decimal a;
        decimal b;
        try {
            a = ((ln.Arguments[0] as TypeArgument).Value as NumberType).Value;
            b = ((ln.Arguments[1] as TypeArgument).Value as NumberType).Value;
        }
        catch (NullReferenceException) {
            throw new ArgumentError("Relational operators takes (number, number) arguments", Executor.Code.Name, ln.OriginLine);
        }

        return new BooleanType(op(a, b));
    }

    private static BooleanType LogicBase(ExecutableLine ln, _logicOperator op)
    {
        bool a;
        bool b;
        try {
            a = ((ln.Arguments[0] as TypeArgument).Value as BooleanType).Value;
            b = ((ln.Arguments[1] as TypeArgument).Value as BooleanType).Value;
        }
        catch (NullReferenceException) {
            throw new ArgumentError("&& and || logic takes (bool, bool) arguments", Executor.Code.Name, ln.OriginLine);
        }

        return new BooleanType(op(a, b));
    }
}
