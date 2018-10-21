using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Game1
{
    internal class ConsoleStreamWriter : TextWriter
    {
        string _output = null;

        public ConsoleStreamWriter(string output)
        {
            _output = output;
        }

        public override void Write(char value)
        {
            base.Write(value);
            _output = _output + value.ToString();
            GUI.Console = _output;
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
