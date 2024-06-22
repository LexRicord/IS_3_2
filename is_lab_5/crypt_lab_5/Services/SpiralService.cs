using crypt_lab_5_excel;
using crypt_lab_5_spiral;
using crypt_lab_5_EntropyChecker;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace crypt_lab_5.Services
{
    static class SpiralService
    {
        public static void MakeSpiral(List<char> russianAlphabet)
        {
            EntropyChecker russianChecker = new EntropyChecker(russianAlphabet, 0, "Русский");
            string russianText = russianChecker.OpenDocument("D:\\repos\\IS_3_2\\is_lab_5\\crypt_lab_5\\bin\\Debug\\net7.0\\russian.txt").ReadToEnd().ToLower();
            Regex regex = new Regex(@"\W");
            russianText = regex.Replace(russianText, "");
            Dictionary<char, int> russianDict = russianChecker.alphabetListToDictionary();

            SpiralEncrypter spiralEncrypter = new SpiralEncrypter(Convert.ToInt32(Math.Sqrt(russianText.Length)), Convert.ToInt32(Math.Sqrt(russianText.Length)), russianText);
            russianChecker.getSymbolsCounts(russianText, russianDict);
            Dictionary<char, double> russianChances = russianChecker.getSymbolsChances(russianText, russianDict);

            russianChecker.printAlphabet();
            russianChecker.printChances(russianChances);

            spiralEncrypter.printMatrix(spiralEncrypter.createMatrix(spiralEncrypter.Text));
            Stopwatch first = new Stopwatch();

            first.Start();
            string resultEnc = spiralEncrypter.Encrypt();
            first.Stop();
            Console.WriteLine($"Время шифрования: {first.ElapsedMilliseconds} мс \n"); ;
            spiralEncrypter.printMatrix(spiralEncrypter.createMatrix(resultEnc));

            Console.WriteLine(resultEnc);
            char[,] resultDecr = spiralEncrypter.Decrypt(resultEnc);
            string resultDecrStr = spiralEncrypter.createString(resultDecr);

            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo("Lab3.xlsx"));
            excel.createWorksheet("first");
            excel.addValuesFromDict(russianChances, "first", 0);
            excel.pack.Save();
            Console.ReadKey();

            Console.WriteLine("\n" + resultDecrStr);
        }
    }
}
