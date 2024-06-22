using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_5.Interfaces
{
    internal interface IEntropyCheck
    {
        public void getSymbolsCounts(string text, Dictionary<char, int> alphabet);
        public Dictionary<char, double> getSymbolsChances(string text, Dictionary<char, int> counts);
        public void computeTextEntropy(Dictionary<char, double> chances);
    }
}
