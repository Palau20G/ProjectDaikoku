using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaikoku.Modules
{
    class YomiOptions
    {
        public int Port { get; set; }
        public string IP { get; set; } = Dns.GetHostName();
        public bool Verbose { get; set; }
        public string? OutputFile { get; set; }

        public string Format { get; set; } = "bin";

    }



}
