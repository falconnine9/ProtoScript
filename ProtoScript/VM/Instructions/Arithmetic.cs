using System;

using ProtoScript.Errors;
using ProtoScript.Objects.Arguments;
using ProtoScript.Objects.Lines;
using ProtoScript.Objects.Types;

namespace ProtoScript.VM.Instructions;

class Arithmetic
{
    private delegate decimal _mathOperation(decimal a, decimal b);

    public static TypeBase AddOp(ExecutableLine ln) => _mathBase(ln, (decimal a, decimal b) => a + b);

    public static TypeBase SubOp(ExecutableLine ln) => _mathBase(ln, (decimal a, decimal b) => a - b);

    public static TypeBase MultiOp(ExecutableLine ln) => _mathBase(ln, (decimal a, decimal b) => a * b);

    public static TypeBase DivOp(ExecutableLine ln) => _mathBase(ln, (decimal a, decimal b) => a / b);

    public static TypeBase RemainderOp(ExecutableLine ln) => _mathBase(ln, (decimal a, decimal b) => a % b);

    public static TypeBase PowerOp(ExecutableLine ln) => _mathBase(ln, (decimal a, decimal b) => (decimal)Math.Pow((double)a, (double)b));

    private static NumberType _mathBase(ExecutableLine ln, _mathOperation op)
    {
        decimal a;
        decimal b;
        try {
            a = ((ln.Arguments[0] as TypeArgument).Value as NumberType).Value;
            b = ((ln.Arguments[1] as TypeArgument).Value as NumberType).Value;
        }
        catch (NullReferenceException) {
            throw new ArgumentError("All math instructions takes (number, number) arguments", Executor.Code.Name, ln.OriginLine);
        }

        return new NumberType(op(a, b));
    }
}
