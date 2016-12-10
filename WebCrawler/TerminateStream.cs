using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace WebCrawler
{
    public class TerminateStream
    {
        StreamWriter TextWriter;
        public TerminateStream(StreamWriter textWriter) {
            TextWriter = textWriter;
        }

        public void Terminate()
        {
            TextWriter.Close();
        }
    }
}
