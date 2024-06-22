using System;
using System.Collections.Generic;
using System.IO;

namespace is_lab_2
{
    class TextAnalyzer
    {
        private string text;

        public TextAnalyzer(string text)
        {
            this.text = text;
        }

        public Dictionary<char, int> Analyze()
        {
            Dictionary<char, int> charFrequency = new Dictionary<char, int>();
            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    if (charFrequency.ContainsKey(c))
                        charFrequency[c]++;
                    else
                        charFrequency[c] = 1;
                }
            }
            Console.WriteLine("Частоты букв в тексте:");
            foreach (var pair in charFrequency)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }

            int alphabetSize = charFrequency.Count;
            double hartleyEntropy = Math.Log2(64);

            double shannonEntropy = 0;
            foreach (var pair in charFrequency)
            {
                double probability = (double)pair.Value / text.Length;
                shannonEntropy -= probability * Math.Log2(probability);
            }

            double redundancy = ((hartleyEntropy - shannonEntropy)/hartleyEntropy) * 100;

            Console.WriteLine($"Энтропия Хартли: {hartleyEntropy}");
            Console.WriteLine($"Энтропия Шеннона: {shannonEntropy}");
            Console.WriteLine($"Избыточность алфавита: {redundancy} %");

            return charFrequency;
        }
    }
}