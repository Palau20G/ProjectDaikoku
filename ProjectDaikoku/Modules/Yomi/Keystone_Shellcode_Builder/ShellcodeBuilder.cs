using Keystone;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaikoku.Modules
{
    public class ShellcodeBuilder
    {
        public byte[] AssembleX64(string asmCode)
        {
            using var ks = new Engine(Architecture.X86, Mode.X64);

            int size;
            int stmtCount;

            byte[] shellcode = ks.Assemble(asmCode, 0, out size, out stmtCount);

            return shellcode;
        }
    }
}
