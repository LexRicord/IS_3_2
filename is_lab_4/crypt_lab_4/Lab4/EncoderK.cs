using System;
using System.Text;

namespace crypt_lab_4
{
    class EncoderK
    {
        public string alphabet;
        public int a, b;
        public string editedAlphabet;

        public EncoderK(string alphabet, int a, int b)
        {
            this.alphabet = alphabet;
            this.a = a;
            this.b = b;
            this.editedAlphabet = editAlphabet(alphabet, a, b);
        }

        public string editAlphabet(string alphabet, int a, int b)
        {
            StringBuilder newAlphabet = new StringBuilder();
            int m = alphabet.Length;
            for (int iter = 0; iter < alphabet.Length; iter++)
            {
                int newPos = (a * iter + b) % m;
                newAlphabet.Append(alphabet[newPos]);
            }
            return newAlphabet.ToString();
        }

        public string encode(string text)
        {
            StringBuilder encodedText = new StringBuilder();
            int m = alphabet.Length;
            for (int iter = 0; iter < text.Length; iter++)
            {
                int pos = this.alphabet.IndexOf(text[iter]);
                int newPos = (a * pos + b) % m;
                char encSymbol = this.editedAlphabet[newPos];
                encodedText.Append(encSymbol);
            }
            return encodedText.ToString();
        }

        public string decode(string text)
        {
            StringBuilder decodedText = new StringBuilder();
            int aInverse = ModInverse(a, alphabet.Length);

            int m = alphabet.Length;
            for (int iter = 0; iter < text.Length; iter++)
            {
                int pos = this.editedAlphabet.IndexOf(text[iter]);
                int newPos = (aInverse * (pos - b + m)) % m;
                char decSymbol = this.alphabet[newPos];
                decodedText.Append(decSymbol);
            }
            return decodedText.ToString();
        }

        private int ModInverse(int a, int m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                    return x;
            }
            return 1;
        }
    }
}
