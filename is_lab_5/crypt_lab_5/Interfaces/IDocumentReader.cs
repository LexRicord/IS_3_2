using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_5.Interfaces
{
    internal interface IDocumentReader
    {
        public StreamReader OpenDocument(string path);
        public string GetAllText(string text, StreamReader reader);
    }
}
