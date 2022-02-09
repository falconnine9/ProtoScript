namespace ProtoScript.Objects.Types;

class Variable
{
    public TypeBase Value { get; set; }
    public bool Const { get; set; }

    public Variable(TypeBase value, bool is_const)
    {
        this.Value = value;
        this.Const = is_const;
    }
}
