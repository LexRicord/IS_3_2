using crypt_lab_5_excel;
using crypt_lab_5_EntropyChecker;
using crypt_lab_5_rearranger;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace crypt_lab_5.Services
{
    static class ReplacementService
    {
        public static void MakeReplace(List<char> russianAlphabet, List<KeyValuePair<int, char>> keyVertical, List<KeyValuePair<int, char>> keyHorizontal)
        {
            try
            {
                EntropyChecker russianChecker = new EntropyChecker(russianAlphabet, 0, "Русский");
                string russianText = russianChecker.OpenDocument("D:\\repos\\IS_3_2\\is_lab_5\\crypt_lab_5\\bin\\Debug\\net7.0\\russian1.txt").ReadToEnd().ToLower();
                List<char> russianTextTrimmedList = russianText.TakeLast(keyVertical.Count * keyHorizontal.Count).ToList();
                StringBuilder russianTextTrimmedBuilder = new StringBuilder();
                foreach (char x in russianTextTrimmedList)
                {
                    russianTextTrimmedBuilder.Append(x);
                }
                string russianTextTrimmed = russianTextTrimmedBuilder.ToString();

                Regex regex = new Regex(@"\W");
                russianTextTrimmed = regex.Replace(russianTextTrimmed, "");
                Dictionary<char, int> deuthDict = russianChecker.alphabetListToDictionary();

                Rearranger rearranger = new Rearranger();
                rearranger.Initialize(russianTextTrimmed, keyVertical, keyHorizontal);

                russianChecker.getSymbolsCounts(russianTextTrimmed, deuthDict);
                Dictionary<char, double> russianChances = russianChecker.getSymbolsChances(russianTextTrimmed, deuthDict);

                russianChecker.printAlphabet();
                russianChecker.printChances(russianChances);
                rearranger.PrintKeys();

                rearranger.PrintMatrix(rearranger.CreateMatrix(rearranger.Text));
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                string resultEnc = rearranger.Encrypt();
                stopwatch.Stop();
                Console.WriteLine($"Время шифрования: {stopwatch.ElapsedMilliseconds} мс");

                rearranger.PrintMatrix(rearranger.CreateMatrix(resultEnc));
                Console.WriteLine(resultEnc);

                ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo("Lab3.xlsx"));
                excel.createWorksheet("second");
                excel.addValuesFromDict(russianChances, "second", 3);
                excel.pack.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");
            }
        }
    }
}
