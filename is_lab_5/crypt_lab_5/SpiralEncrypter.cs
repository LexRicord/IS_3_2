using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using crypt_lab_5_matrix;
using Microsoft.Office.Interop.Excel;

namespace crypt_lab_5_spiral
{
    internal class SpiralEncrypter
    {
        private int tableHeight;
        private int tableWidth;
        private string text;

        public int TableHeight
        {
            get { 
                return tableHeight; 
            }
            set { 
                tableHeight = value; 
            }
        }

        public int TableWidth
        {
            get {
                return tableWidth; 
            }
            set {
                tableWidth = value; 
            }
        }

        public string Text
        {
            get { 
                return text;
            }
            set {
                text = value; 
            }
        }

        public SpiralEncrypter(int tableWidth, int tableHeight, string text)
        {
            TableWidth = tableWidth;
            TableHeight = tableHeight;
            Text = text;
        }

        public char[,] createMatrix(string input)
        {
            char[,] table = new char[tableWidth, tableHeight];
            int l = 0;
            for (int w = 0; w < tableWidth; w++)
            {
                for (int i = 0; i < tableHeight; i++)
                {
                    if (l == input.Length) break;
                    table[w, i] = input[l++];
                }
                if (l == input.Length) break;
            }
            return table;
        }

        public string createString(char[] input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int w = 0; w < input.Length; w++)
            {
                stringBuilder.Append(input[w]);
            }
            return stringBuilder.ToString();
        }

        public string createString(char[,] input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int w = 0; w < tableWidth; w++)
            {
                for (int l = 0; l < tableHeight; l++)
                    stringBuilder.Append(input[w, l]);
            }
            return stringBuilder.ToString();
        }

        public void printMatrix(char[,] input)
        {
            for (int w = 0; w < tableWidth; w++)
            {
                for (int i = 0; i < tableHeight; i++)
                {
                    Console.Write($"{(input[w, i] == ' ' ? '*' : input[w, i])} ");
                }
                Console.Write($"\n");

            }
            Console.Write($"\n");
        }

        public string Encrypt()
        {
            char[,] table = createMatrix(text);
            char[] oResult = new char[text.Length];

            int x = 0, y = 0, l = 0, start = 0, step = 1;
            bool errorOccured = false;

            Console.WriteLine("Spiral Matrix\n");
            try
            {
                Console.WriteLine("Enter size of the matrix:");
                Console.Write("Row (x): " + tableWidth);
                x = tableWidth;
                Console.Write("Column (y): " + TableHeight);
                y = tableHeight;

                if (x < 0 || y < 0 || start < 0 || step < 0) throw new FormatException();
            }
            catch (FormatException e)
            {
                Console.WriteLine("Wrong input. [Details: {0}]", e.Message);
                Console.WriteLine("Program will now exit...");
                errorOccured = true;
            }

            if (!errorOccured)
            {
                List<char> ans = new List<char>();
                char[,] mat = new char[TableWidth, TableHeight];
                mat = SpiralMatrix.initMatrix(TableWidth, TableHeight, table);

                Console.WriteLine("\nIntial matrix generated is:");
                SpiralMatrix.displayMatrix(mat, x, y);

                Console.WriteLine("\nSpiral Matrix generated is:");
                ans = SpiralMatrix.spiralOrder(mat);

                SpiralMatrix.displayMatrix(mat, x, y);
                oResult = ans.ToArray();
            }
            Console.Write("\nPress enter to continue...");
            Console.Read();

            Console.WriteLine("Spiral: Encrypted Version");
            Console.WriteLine(oResult);
            return createString(oResult);
        }

        public char[,] Decrypt(string input)
        {
            char[,] table = new char[tableWidth, tableHeight];
            char[] oReverse = new char[tableWidth * tableHeight];
            char[] output = new char[tableWidth * tableHeight];
            List<char> ans = new List<char>();

            int i, k = 0, l = 0, m = tableWidth, n = tableHeight;

            oReverse = input.ToCharArray();
            for (int c = 0; c < oReverse.Length / 2; c++)
            {
                char tmp = oReverse[c];
                oReverse[c] = oReverse[oReverse.Length - c - 1];
                oReverse[oReverse.Length - c - 1] = tmp;
            }

            for (i = 0; i < tableWidth; i++)
            {
                for (int j = 0; j < tableHeight; j++)
                {
                    if (l >= oReverse.Length)
                        break;
                    table[i, j] = oReverse[l];
                    l++;
                }
                if (l >= oReverse.Length)
                    break;
            }

            Console.WriteLine("\n-----Input-----\n");
            Console.WriteLine(input);

            Console.WriteLine("\n-----oReverse-----\n");
            Console.WriteLine(oReverse);

            Console.WriteLine("\n-----table-----\n");
            table = SpiralMatrix.initMatrix(tableHeight, tableWidth, oReverse);
            printMatrix(table);

            Console.WriteLine("\nSpiral Matrix generated is:");

            table = SpiralMatrix.ReverseSpiralInput(tableHeight, tableWidth, oReverse);
            printMatrix(table);

            Console.WriteLine("\n-----Spiral: Decrypted Version-----\n");
           
            return table;
        }
    }
}
