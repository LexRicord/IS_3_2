using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_5.Interfaces
{
    internal interface IPrintEntropy
    {
        public void printAlphabet();
        public void printChances(Dictionary<char, double> chances);
        public void printAlhabetEntropy();
    }
}
