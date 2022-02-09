using System;

namespace ProtoScript.Errors;

class CustomError : Exception
{
    private readonly string _err_type;
    private readonly string _message;
    private readonly string _file;
    private readonly int _line;

    public CustomError(string err_type, string message, string file = default, int line = -1)
    {
        this._err_type = err_type;
        this._message = message;
        this._file = file;
        this._line = line;
    }

    public override string ToString()
    {
        return string.Format(
            "{0}: {1}\nAt file: {2}\nAt line: {3}",
            this._err_type,
            this._message,
            this._file,
            this._line
        ); ;
    }
}
