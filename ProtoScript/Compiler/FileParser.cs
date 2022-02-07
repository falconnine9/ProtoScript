using System;
using System.Collections.Generic;
using System.Text;

using ProtoScript.Errors;
using ProtoScript.Objects.Lines;

namespace ProtoScript.Compiler
{
    class FileParser
    {
        public readonly string FileName;
        public readonly string Source;

        private readonly List<AnonymousLine> _lines;
        private readonly List<string> _lineBuffer;
        private StringBuilder _buffer;

        private bool _quotationActive;

        private int _realLine;

        public FileParser(string source, string filename)
        {
            this.FileName = filename;
            this.Source = source.Trim().Replace("\r", string.Empty).Replace("\t", string.Empty);

            this._lines = new List<AnonymousLine>();
            this._lineBuffer = new List<string>();
            this._buffer = new StringBuilder();

            this._quotationActive = false;

            this._realLine = 1;
        }

        public AnonymousLine[] ParseSource()
        {
            for (int i = 0; i < this.Source.Length; i++) {
                char c = this.Source[i];

                switch (c) {
                    case '\n':
                        this.HandleNewline();
                        break;

                    case ';':
                        this.HandleLineBreak();
                        break;

                    case ' ':
                        this.HandleTokenBreak(c);
                        break;

                    case ',':
                        this.HandleTokenBreak(c);
                        break;

                    case '"':
                        this.HandleQuotation();
                        break;

                    default:
                        _ = this._buffer.Append(c);
                        break;
                }
            }

            if (this._buffer.Length > 0)
                this.PushBuffer();

            if (this._lineBuffer.Count > 0)
                this.PushLineBuffer();

            return this._lines.ToArray();
        }

        private void PushLineBuffer()
        {
            this._lines.Add(new AnonymousLine(this._lineBuffer.ToArray(), this.FileName, this._realLine));
            this._lineBuffer.Clear();
        }

        private void PushBuffer()
        {
            this._lineBuffer.Add(this._buffer.ToString());
            this._buffer = this._buffer.Clear();
        }

        private void HandleNewline()
        {
            this._realLine++;

            if (this._quotationActive)
                return;

            if (this._buffer.Length > 0)
                this.PushBuffer();
        }

        private void HandleLineBreak()
        {
            if (this._quotationActive) {
                _ = this._buffer.Append(';');
                return;
            }

            if (this._buffer.Length > 0)
                this.PushBuffer();

            if (this._lineBuffer.Count > 0)
                this.PushLineBuffer();
        }

        private void HandleTokenBreak(char c)
        {
            if (this._quotationActive) {
                _ = this._buffer.Append(c);
                return;
            }

            if (this._buffer.Length > 0)
                this.PushBuffer();
        }

        private void HandleQuotation()
        {
            _ = this._buffer.Append('"');
            this._quotationActive = !this._quotationActive;
        }
    }
}
