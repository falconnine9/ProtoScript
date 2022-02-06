using System;
using System.Text;

namespace ProtoScript.Objects.Types
{
    class StringType : TypeBase
    {
        public override Type RealType { get; }
        public string Value => this.ToString();

        private readonly StringBuilder _builder;
        private string _cache;
        private bool _updated;

        public StringType(string value)
        {
            this.RealType = typeof(StringType);
            this._builder = new StringBuilder(value);
            this._cache = value;
            this._updated = false;
        }

        public override string ToString()
        {
            if (this._updated) {
                this._cache = this._builder.ToString();
                this._updated = false;
            }
            return this._cache;
        }
    }
}
