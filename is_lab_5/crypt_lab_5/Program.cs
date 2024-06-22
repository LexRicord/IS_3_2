using crypt_lab_5.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace crypt_lab_5
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char> russianAlphabet = new List<char>()
            {
                'а','б','в','г','д','е','ё','ж','з','и','й',
                'к','л','м','н','о','п','р','с','т','у','ф',
                'х','ц','ч','ш','щ','ъ','ы','ь','э','ю','я'
            };

            int[] keyH = new int[] { 5, 3, 6, 4, 2, 7, 1, 9, 8 };
            char[] keyHword = new char[] { 'а', 'л', 'е', 'к', 'с', 'а', 'н', 'д', 'р', };
            int[] keyV = new int[] { 2, 6, 4, 3, 5, 1};
            char[] keyVword = new char[] { 'г', 'е', 'р', 'м', 'а', 'н'};

            List<KeyValuePair<int, char>> keyVertical = new List<KeyValuePair<int, char>>();
            List<KeyValuePair<int, char>> keyHorizontal = new List<KeyValuePair<int, char>>();

            for (int i = 0; i < keyV.Length; i++)
            {
                keyVertical.Add(new KeyValuePair<int, char>(keyV[i], keyVword[i]));
            }
            for (int i = 0; i < keyH.Length; i++)
            {
                keyHorizontal.Add(new KeyValuePair<int, char>(keyH[i], keyHword[i]));
            }

            try
            {
                Console.WriteLine("------------MakeSpiral----------");
                SpiralService.MakeSpiral(russianAlphabet);
                Console.WriteLine("------------MakeReplace----------");
                ReplacementService.MakeReplace(russianAlphabet, keyVertical, keyHorizontal);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");
            }
        }
    }
}
