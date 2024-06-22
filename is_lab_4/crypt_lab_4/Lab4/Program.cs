using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Lab2;

namespace crypt_lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string polishAlph = "aąbcćdeęfghijklłmnńoópqrsśtuvwxyzźż";
            string keyWord = "german";
            const string fileName = "Lab4-1.xls";
            int a = 17, b = 23;
            EntropyChecker polishChecker = new EntropyChecker(polishAlph, 0, "Польский");
            string polishText = polishChecker.OpenDocument("polish.txt").ReadToEnd().ToLower();
            Regex regex = new Regex(@"\W");
            polishText = regex.Replace(polishText, "");
            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));

            int c = 0;
            while (true)
            {
                Console.WriteLine("Введите номер задания:");
                Console.WriteLine("1) На основе аффинной системы подстановок Цезаря; a = 5, b = 7 ");
                Console.WriteLine("2) Шифр Порты");
                Console.WriteLine("3) Выход");

                if (!int.TryParse(Console.ReadLine(), out c))
                {
                    c = -1;
                }
                switch (c)
                {
                    case 1:
                        {
                            EncoderK encoderK = new EncoderK(polishAlph, a, b);

                            Stopwatch first = new Stopwatch();

                            Console.WriteLine($"Алфавит языка и алфавит для шифрования:\n{polishAlph}\n{encoderK.editedAlphabet}\n");

                            Console.WriteLine($"Текст для шифрования:\n{polishText}");

                            Dictionary<char, int> alphCounts = polishChecker.alphabetListToDictionary();
                            polishChecker.getSymbolsCounts(polishText, alphCounts);

                            Dictionary<char, double> chances = polishChecker.getSymbolsChances(polishText, alphCounts);
                            polishChecker.printChances(chances);

                            excel.createWorksheet("first");
                            excel.addValuesFromDict(chances, "first", 0);

                            first.Start();
                            string encodedText = encoderK.encode(polishText);
                            first.Stop();
                            Console.WriteLine($"Время шифрования: {first.Elapsed.TotalMilliseconds} мс \n"); ;

                            Dictionary<char, int> alphCountsEnc = polishChecker.alphabetListToDictionary();
                            polishChecker.getSymbolsCounts(encodedText, alphCountsEnc);

                            Console.WriteLine($"Зашифрованный текст:\n{encodedText}");

                            chances = polishChecker.getSymbolsChances(encodedText, alphCountsEnc);
                            polishChecker.printChances(chances);

                            excel.createWorksheet("first");
                            excel.addValuesFromDict(chances, "first", 3);
                            excel.pack.Save();

                            string decodedText = encoderK.decode(encodedText);
                            Console.WriteLine($"Расшифрованный текст:\n{decodedText}");

                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                    case 2:
                        {
                            EncoderPorta encoderPorta = new EncoderPorta();

                            Stopwatch second = new Stopwatch();

                            Console.WriteLine($"Алфавит языка и ключевое слово для шифрования:\n{polishAlph}\n{keyWord}\n");

                            string polishTextFromFile = System.IO.File.ReadAllText("polish.txt").ToLower();

                            Console.WriteLine($"Текст для шифрования:\n{polishTextFromFile}");

                            Dictionary<char, int> alphCounts = polishChecker.alphabetListToDictionary();
                            polishChecker.getSymbolsCounts(polishTextFromFile, alphCounts);

                            Dictionary<char, double> chances = polishChecker.getSymbolsChances(polishTextFromFile, alphCounts);
                            polishChecker.printChances(chances);

                            excel.createWorksheet("second");
                            excel.addValuesFromDict(chances, "second", 0);

                            second.Start();
                            string encryptedText = encoderPorta.Encrypt(polishTextFromFile);
                            second.Stop();
                            Console.WriteLine($"Время шифрования: {second.Elapsed.TotalMilliseconds} мс \n");

                            Dictionary<char, int> alphCountsEnc = polishChecker.alphabetListToDictionary();
                            polishChecker.getSymbolsCounts(encryptedText, alphCountsEnc);

                            Console.WriteLine($"Зашифрованный текст:\n{encryptedText}");

                            chances = polishChecker.getSymbolsChances(encryptedText, alphCountsEnc);
                            polishChecker.printChances(chances);

                            excel.createWorksheet("second");
                            excel.addValuesFromDict(chances, "second", 3);
                            excel.pack.Save();

                            string decryptedText = encoderPorta.Decrypt(encryptedText);
                            Console.WriteLine($"Расшифрованный текст:\n{decryptedText}");

                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                    case 3:
                        {
                            return;
                        }
                    case -1:
                        {
                            Console.Clear();
                            break;
                        }
                    default:
                        {
                            Console.Clear();
                            break;
                        }
                }
            }
        }
    }
}
