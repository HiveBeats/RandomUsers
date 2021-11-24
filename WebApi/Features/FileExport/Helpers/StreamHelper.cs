using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Features.FileExport.Helpers
{
    public class StreamHelper
    {
        public static Stream GetStream(string s)
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            sw.Write(s);
            sw.Flush();
            ms.Position = 0;

            return ms;
        }
    }
}
