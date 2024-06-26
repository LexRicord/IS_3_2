﻿using Lab2.DocumentReader;
using Lab2.Interfaces;
using System;
using System.Security.Cryptography;

namespace crypt_2_lab.EntropyChecker
{
    class EntropyChecker : IDocumentReader, IEntropyCheck, IPrintEntropy
    {
        private string alphabetName;
        private List<char> alphabet;
        private double alphabetEntropy = 0;
        string patternRussian = @"A";
        string patternGerman = @"R";

        public EntropyChecker()
        {
        }

        public EntropyChecker(List<char> alphabet, double alphabetEntropy, string alphabetName)
        {
            Alphabet = alphabet;
            AlphabetEntropy = alphabetEntropy;
            AlphabetName = alphabetName;
        }

        private int myVar;

        public List<char> Alphabet
        {
            get { return alphabet; }
            set { alphabet = value; }
        }


        public string AlphabetName
        {
            get { return alphabetName; }
            set { alphabetName = value; }
        }


        public double AlphabetEntropy
        {
            get { return alphabetEntropy; }
            set { alphabetEntropy = value; }
        }


        public Dictionary<char, int> alphabetListToDictionary()
        {
            Dictionary<char, int> dict = new Dictionary<char, int>(Alphabet.Count());
            foreach (char x in alphabet)
            {
                dict.Add(x, 0);
            }
            return dict;
        }

        public string GetAllText(string text, StreamReader reader)
        {
            if (reader == null)
            {
                throw new Exception("Document isn't open");
            }

            string fullText = reader.ReadToEnd();
            string alphabeticText = new string(fullText.Where(char.IsLetter).ToArray());

            return alphabeticText;
        }

        public Dictionary<char, double> getSymbolsChances(string text, Dictionary<char, int> counts)
        {
            Dictionary<char, double> chances = new Dictionary<char, double>(alphabet.Count);

            for (int i = 0; i < counts.Count(); i++)
            {
                chances.Add(alphabet[i], (double)counts[alphabet[i]] / text.Length);
            }

            return chances;
        }

        public void getSymbolsCounts(string text, Dictionary<char, int> alphabet)
        {
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < this.alphabet.Count(); j++)
                {
                    if (text[i] == this.alphabet[j])
                    {
                        alphabet[this.alphabet[j]]++;
                    }
                }
            }
        }

        public void computeTextEntropy(Dictionary<char, double> chances)
        {
            for (int i = 0; i < alphabet.Count; i++)
            {
                if (chances[alphabet[i]] != 0)
                {
                    AlphabetEntropy += chances[alphabet[i]] * Math.Log(chances[alphabet[i]], 2);
                }
            }
            AlphabetEntropy = -AlphabetEntropy;
        }

        public double computeTextEntropyWithError(Dictionary<char, double> chances, double p)
        {
            double q = 1 - p;
            double effectiveEntropy = 1 - (-p * Math.Log(p, 2) - q * Math.Log(q, 2));
            Console.WriteLine($"Effective entropy(with error): {effectiveEntropy}");
            if (double.IsNaN(effectiveEntropy))
            {
                return 0;
            }
            return effectiveEntropy;
        }

        public double computeTextEntropyWithErrorV2(Dictionary<char, double> chances, double p)
        {
            double q = 1 - p;
            double effectiveEntropy = (-p * Math.Log(p, 2) - q * Math.Log(q, 2));
            Console.WriteLine($"Effective entropy(with error): {effectiveEntropy}");
            if (double.IsNaN(effectiveEntropy))
            {
                return 0;
            }
            return effectiveEntropy;
        }

        public StreamReader OpenDocument(string path)
        {
            return new StreamReader(path);
        }

        public void printAlphabet()
        {
            Console.WriteLine($"\nАлфавит {AlphabetName}:"); ;
            foreach (char x in alphabet)
            {
                if(x == 0) break;
                Console.Write(x); Console.Write(" ");
            }
        }

        public void printExampleOfBinaryChar()
        {
            var random = new Random();
            Console.WriteLine($"\nАлфавит {AlphabetName}:");
            var lowerBound = 0;
            var upperBound = 1;
            int rNum = random.Next(lowerBound, upperBound);
            Console.WriteLine(rNum.ToString());
            char firstChar = alphabet.ToArray()[rNum];
            string binary = Convert.ToString(firstChar, 2);
            Console.WriteLine("\tПример бинарного числа из алфавита (передние нули опущены): "); 
            Console.Write($"\t{binary}");
        }

        public void printChances(Dictionary<char, double> chances)
        {
            Console.WriteLine("\n\tШансы появления символа:");
            foreach (char x in Alphabet)
                Console.WriteLine($"\t{x} : {chances[x]}");
        }

        public void printAlhabetEntropy()
        {
            Console.WriteLine($"\nЭнтропия алфавита для языка '{AlphabetName}' равна {AlphabetEntropy}.");
        }
    }
}
