namespace ProtoScript.Objects.Types;

class VariableSet
{
    public string Name { get; set; }
    public bool Const { get; set; }

    public VariableSet(string name, bool is_const)
    {
        this.Name = name;
        this.Const = is_const;
    }
}
