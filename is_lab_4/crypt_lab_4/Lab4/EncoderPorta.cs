using System;
using System.Collections.Generic;

namespace crypt_lab_4
{
    public class EncoderPorta
    {
        private readonly Dictionary<char, int> letterToNumber;
        private readonly Dictionary<int, char> numberToLetter;

        public EncoderPorta()
        {
            letterToNumber = new Dictionary<char, int>();
            numberToLetter = new Dictionary<int, char>();

            string polishAlphabet = "AĄBCĆDEĘFGHIJKLŁMNŃOÓPRSŚTUWYZŹŻ";

            for (int i = 0; i < polishAlphabet.Length; i++)
            {
                letterToNumber[polishAlphabet[i]] = i + 1;
                numberToLetter[i + 1] = polishAlphabet[i];
            }
        }

        public string Encrypt(string plaintext)
        {
            plaintext = plaintext.ToUpper();

            List<int> encryptedNumbers = new List<int>();

            foreach (char c in plaintext)
            {
                if (letterToNumber.ContainsKey(c))
                {
                    int number = letterToNumber[c];
                    encryptedNumbers.Add(number);
                }
            }

            return string.Join(" ", encryptedNumbers);
        }

        public string Decrypt(string ciphertext)
        {
            string[] numbers = ciphertext.Split(' ');
            List<char> decryptedLetters = new List<char>();

            foreach (string number in numbers)
            {
                if (int.TryParse(number, out int num))
                {
                    if (numberToLetter.ContainsKey(num))
                    {
                        char letter = numberToLetter[num];
                        decryptedLetters.Add(letter);
                    }
                }
            }

            return string.Join("", decryptedLetters);
        }
    }
}