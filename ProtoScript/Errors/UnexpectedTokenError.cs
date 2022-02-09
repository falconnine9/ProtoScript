using System;

namespace ProtoScript.Errors;

class UnexpectedTokenError : Exception
{
    private readonly string _message;
    private readonly string _file;
    private readonly int _line;

    public UnexpectedTokenError(string message, string file = default, int line = -1)
    {
        this._message = message;
        this._file = file;
        this._line = line;
    }

    public override string ToString()
    {
        return string.Format(
            "UnexpectedTokenError: {0}\nAt file: {1}\nAt line: {2}",
            this._message,
            this._file,
            this._line
        );
    }
}
