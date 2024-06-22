using crypt_2_lab.EntropyChecker;
using Lab2.DocumentReader;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            const string fileName = "D:\\repos\\IS_3_2\\is_lab_1\\crypt_2_lab\\crypt_2_lab\\bin\\Debug\\net7.0\\Lab_2.xlsx";
            double entropyRussian = 0.0, entropyGerman = 0.0, binaryEntropyGerman = 0.0, binaryEntropyRussian = 0.0; 

            List<char> germanAlphabet = new List<char>()
            {
                'a','b','c','d','e','f','g','h','i','j','k',
                'l','m','n','o','p','r','s','t','u','v','w',
                'ä','ö','ü','ß'
            };

            List<char> russianAlphabet = new List<char>()
            {
                'а','б','в','г',
                'д','е','ё','ж','з',
                'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'
            };
            while (choice != 5)
            {
                Console.Clear();

                Console.WriteLine("Выберите номер задания:\n- 1. a) - Alphabets entropy count;\n- 2. b) Binary alphabet entropy count;\n- 3. c) Count info on a) alph and b) ASCII;\n- 4. d) Error in data transportation 0.1, 0.5, 0,9;\n- 5. Exit");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            Console.Clear();
                            EntropyChecker germanChecker = new EntropyChecker(germanAlphabet, 0, "Немецкий(Германия)");
                            EntropyChecker russianChecker = new EntropyChecker(russianAlphabet, 0, "Русский");

                            string germanText = germanChecker.OpenDocument("german.txt").ReadToEnd().ToLower();
                            string russianText = russianChecker.OpenDocument("russian.txt").ReadToEnd().ToLower();

                            Regex regex = new Regex(@"\W");
                            germanText = regex.Replace(germanText, "");
                            russianText = regex.Replace(russianText, "");

                            Dictionary<char, int> germanDict = germanChecker.alphabetListToDictionary();
                            Dictionary<char, int> russianDict = russianChecker.alphabetListToDictionary();

                            germanChecker.getSymbolsCounts(germanText, germanDict);
                            russianChecker.getSymbolsCounts(russianText, russianDict);

                            Dictionary<char, double> chancesGerman = germanChecker.getSymbolsChances(germanText, germanDict);
                            Dictionary<char, double> chancesRussian = russianChecker.getSymbolsChances(russianText, russianDict);

                            germanChecker.computeTextEntropy(chancesGerman);
                            russianChecker.computeTextEntropy(chancesRussian);

                            entropyRussian = russianChecker.AlphabetEntropy;
                            entropyGerman = germanChecker.AlphabetEntropy;

                            germanChecker.printAlphabet();
                            germanChecker.printExampleOfBinaryChar();
                            germanChecker.printChances(chancesGerman);
                            germanChecker.printAlhabetEntropy();

                            russianChecker.printAlphabet();
                            russianChecker.printExampleOfBinaryChar();
                            russianChecker.printChances(chancesRussian);
                            russianChecker.printAlhabetEntropy();

                            double sumGerman = 0;
                            double sumRussian = 0;
                            foreach (KeyValuePair<char, double> x in chancesGerman)
                            {
                                sumGerman += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesRussian)
                            {
                                sumRussian += x.Value;
                            }

                            Console.WriteLine($"\n\n[Сумма шансов для русского языка: {sumRussian}]");
                            Console.WriteLine($"[Сумма шансов для немецкого языка: {sumGerman}]");

                            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));
                            excel.createWorksheet("first");
                            excel.addValuesFromDict(chancesGerman, "first", 0);
                            excel.addValuesFromDict(chancesRussian, "first", 3);
                            excel.SaveAndClose();
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();

                            EntropyChecker germanBinChecker = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код(Немецкий)");
                            EntropyChecker russianBinChecker = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код(Русский)");

                            string germanText = germanBinChecker.OpenDocument("german.txt").ReadToEnd().ToLower();
                            string russianText = russianBinChecker.OpenDocument("russian.txt").ReadToEnd().ToLower();

                            Regex regex = new Regex(@"\W");
                            germanText = regex.Replace(germanText, "");
                            russianText = regex.Replace(russianText, "");

                            string binTextGerman = "";
                            string binTextRussian = "";

                            var textChr = Encoding.UTF8.GetBytes(germanText);
                            foreach (int chr in textChr)
                            {
                                binTextGerman += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            textChr = Encoding.UTF8.GetBytes(russianText);
                            foreach (int chr in textChr)
                            {
                                binTextRussian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            Dictionary<char, int> germanDict = germanBinChecker.alphabetListToDictionary();
                            Dictionary<char, int> russianDict = russianBinChecker.alphabetListToDictionary();

                            germanBinChecker.getSymbolsCounts(binTextGerman, germanDict);
                            russianBinChecker.getSymbolsCounts(binTextRussian, russianDict);

                            Dictionary<char, double> chancesGerman = germanBinChecker.getSymbolsChances(binTextGerman, germanDict);
                            Dictionary<char, double> chancesRussian = russianBinChecker.getSymbolsChances(binTextRussian, russianDict);

                            germanBinChecker.computeTextEntropy(chancesGerman);
                            russianBinChecker.computeTextEntropy(chancesRussian);

                            binaryEntropyRussian = russianBinChecker.AlphabetEntropy;
                            binaryEntropyGerman = germanBinChecker.AlphabetEntropy;

                            germanBinChecker.printAlphabet();
                            germanBinChecker.printExampleOfBinaryChar();
                            germanBinChecker.printChances(chancesGerman);
                            germanBinChecker.printAlhabetEntropy();


                            russianBinChecker.printAlphabet();
                            russianBinChecker.printExampleOfBinaryChar();
                            russianBinChecker.printChances(chancesRussian);
                            russianBinChecker.printAlhabetEntropy();

                            double sumGerman = 0;
                            double sumRussian = 0;
                            foreach (KeyValuePair<char, double> x in chancesGerman)
                            {
                                sumGerman += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesRussian)
                            {
                                sumRussian += x.Value;
                            }

                            Console.WriteLine($"Сумма шансов для русского языка: {sumRussian}");
                            Console.WriteLine($"Сумма шансов для немецкого языка: {sumGerman}");

                            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));
                            excel.createWorksheet("second");
                            excel.addValuesFromDict(chancesGerman, "second", 0);
                            excel.addValuesFromDict(chancesRussian, "second", 3);
                            excel.pack.Save();

                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();

                            EntropyChecker germanChecker = new EntropyChecker(germanAlphabet, entropyGerman, "Немецкий(Германия)");
                            EntropyChecker russianChecker = new EntropyChecker(russianAlphabet, entropyRussian, "Русский");
                            EntropyChecker germanCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, binaryEntropyGerman, "Бинарный код (немецкий(Германия))");
                            EntropyChecker russianCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, binaryEntropyRussian, "Бинарный код (русский)");

                            string germanText = "germanaleksandrevgenievich";
                            string russianText = "германалександревгеньевич";

                            string binTextGerman = "";
                            string binTextRussian = "";

                            var textChr = Encoding.UTF8.GetBytes(germanText);
                            foreach (int chr in textChr)
                            {
                                binTextGerman += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            textChr = Encoding.UTF8.GetBytes(russianText);
                            foreach (int chr in textChr)
                            {
                                binTextRussian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            Dictionary<char, int> germanDict = germanChecker.alphabetListToDictionary();
                            Dictionary<char, int> russianDict = russianChecker.alphabetListToDictionary();
                            Dictionary<char, int> germanDictBin = germanCheckerBin.alphabetListToDictionary();
                            Dictionary<char, int> russianDictBin = russianCheckerBin.alphabetListToDictionary();

                            germanChecker.getSymbolsCounts(germanText, germanDict);
                            russianChecker.getSymbolsCounts(russianText, russianDict);
                            germanCheckerBin.getSymbolsCounts(binTextGerman, germanDictBin);
                            russianCheckerBin.getSymbolsCounts(binTextRussian, russianDictBin);

                            Dictionary<char, double> chancesGerman = germanChecker.getSymbolsChances(germanText, germanDict);
                            Dictionary<char, double> chancesRussian = russianChecker.getSymbolsChances(russianText, russianDict);
                            Dictionary<char, double> chancesGermanBin = germanCheckerBin.getSymbolsChances(binTextGerman, germanDictBin);
                            Dictionary<char, double> chancesRussianBin = russianCheckerBin.getSymbolsChances(binTextRussian, russianDictBin);

                            germanChecker.printAlphabet();
                            germanChecker.printExampleOfBinaryChar();
                            germanChecker.printChances(chancesGerman);
                            germanChecker.printAlhabetEntropy();

                            Console.WriteLine($"\n\nКоличество информации сообщения. Язык - {germanChecker.AlphabetName}: {entropyGerman * germanText.Length}");

                            russianChecker.printAlphabet();
                            russianChecker.printExampleOfBinaryChar();
                            russianChecker.printChances(chancesRussian);
                            russianChecker.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {russianChecker.AlphabetName}: {entropyRussian * russianText.Length}");

                            germanCheckerBin.printAlphabet();
                            germanCheckerBin.printExampleOfBinaryChar();
                            germanCheckerBin.printChances(chancesGermanBin);
                            germanCheckerBin.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {germanCheckerBin.AlphabetName}: {binaryEntropyGerman * binTextGerman.Length}");

                            russianCheckerBin.printAlphabet();
                            russianCheckerBin.printExampleOfBinaryChar();
                            russianCheckerBin.printChances(chancesRussianBin);
                            russianCheckerBin.printAlhabetEntropy();

                            Console.WriteLine($"Количество информации сообщения. Язык - {russianCheckerBin.AlphabetName}: {binaryEntropyRussian * binTextRussian.Length}");

                            double sumGerman = 0;
                            double sumRussian = 0;
                            double sumGermanBin = 0;
                            double sumRussianBin = 0;
                            foreach (KeyValuePair<char, double> x in chancesGerman)
                            {
                                sumGerman += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesRussian)
                            {
                                sumRussian += x.Value;
                            }

                            foreach (KeyValuePair<char, double> x in chancesGermanBin)
                            {
                                sumGermanBin += x.Value;
                            }
                            foreach (KeyValuePair<char, double> x in chancesRussianBin)
                            {
                                sumRussianBin += x.Value;
                            }

                            Console.WriteLine($"Сумма шансов для русского языка: {sumRussian}");
                            Console.WriteLine($"Сумма шансов для немецкого языка: {sumGerman}");
                            Console.WriteLine($"Сумма шансов для русского языка (бинарный): {sumRussianBin}");
                            Console.WriteLine($"Сумма шансов для немецкого языка (бинарный): {sumGermanBin}");

                            ExcelDocumentCreator<char, double> excel = new ExcelDocumentCreator<char, double>(new System.IO.FileInfo(fileName));
                            excel.createWorksheet("third");
                            excel.addValuesFromDict(chancesGerman, "third", 0);
                            excel.addValuesFromDict(chancesRussian, "third", 3);
                            excel.addValuesFromDict(chancesGermanBin, "third", 5);
                            excel.addValuesFromDict(chancesRussianBin, "third", 7);
                            excel.pack.Save();


                            Console.ReadKey();
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();
                            EntropyChecker germanChecker = new EntropyChecker(germanAlphabet, 0, "Немецкий(Германия)");
                            EntropyChecker russianChecker = new EntropyChecker(russianAlphabet, 0, "Русский");
                            EntropyChecker germanCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (немецкий)");
                            EntropyChecker russianCheckerBin = new EntropyChecker(new List<char>() { '0', '1' }, 0, "Бинарный код (русский)");

                            string germanText = "germanaleksandrevgenievich";
                            string russianText = "германалександревгеньевич";

                            string binTextGerman = "";
                            string binTextRussian = "";

                            var textChr = Encoding.UTF8.GetBytes(germanText);
                            foreach (int chr in textChr)
                            {
                                binTextGerman += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            textChr = Encoding.UTF8.GetBytes(russianText);
                            foreach (int chr in textChr)
                            {
                                binTextRussian += Convert.ToString(chr, 2).PadLeft(8, '0');
                            }

                            Dictionary<char, int> germanDict = germanChecker.alphabetListToDictionary();
                            Dictionary<char, int> russianDict = russianChecker.alphabetListToDictionary();
                            Dictionary<char, int> germanDictBin = germanCheckerBin.alphabetListToDictionary();
                            Dictionary<char, int> russianDictBin = russianCheckerBin.alphabetListToDictionary();

                            germanChecker.getSymbolsCounts(germanText, germanDict);
                            russianChecker.getSymbolsCounts(russianText, russianDict);
                            germanCheckerBin.getSymbolsCounts(binTextGerman, germanDictBin);
                            russianCheckerBin.getSymbolsCounts(binTextRussian, russianDictBin);

                            Dictionary<char, double> chancesGerman = germanChecker.getSymbolsChances(germanText, germanDict);
                            Dictionary<char, double> chancesRussian = russianChecker.getSymbolsChances(russianText, russianDict);
                            Dictionary<char, double> chancesGermanBin = germanCheckerBin.getSymbolsChances(binTextGerman, germanDictBin);
                            Dictionary<char, double> chancesRussianBin = russianCheckerBin.getSymbolsChances(binTextRussian, russianDictBin);

                            germanChecker.printAlphabet();
                            germanChecker.printExampleOfBinaryChar();
                            germanChecker.printChances(chancesGerman);
                            germanChecker.printAlhabetEntropy();

                            russianChecker.printAlphabet();
                            russianChecker.printExampleOfBinaryChar();
                            russianChecker.printChances(chancesRussian);
                            russianChecker.printAlhabetEntropy();

                            germanCheckerBin.printAlphabet();
                            germanCheckerBin.printExampleOfBinaryChar();
                            germanCheckerBin.printChances(chancesGermanBin);
                            germanCheckerBin.printAlhabetEntropy();

                            russianCheckerBin.printAlphabet();
                            russianCheckerBin.printExampleOfBinaryChar();
                            russianCheckerBin.printChances(chancesRussianBin);
                            russianCheckerBin.printAlhabetEntropy();

                            Console.WriteLine($"\n\nОшибка = 0.1. Количество информации сообщения. Язык - {germanChecker.AlphabetName}: {(entropyGerman - germanChecker.computeTextEntropyWithErrorV2(chancesGerman, 0.1)) * germanText.Length}");
                            Console.WriteLine($"Ошибка = 0.5. Количество информации сообщения. Язык - {germanChecker.AlphabetName}: {(entropyGerman - germanChecker.computeTextEntropyWithErrorV2(chancesGerman, 0.5)) * germanText.Length}");
                            Console.WriteLine($"Ошибка = 1. Количество информации сообщения. Язык - {germanChecker.AlphabetName}: {(entropyGerman - germanChecker.computeTextEntropyWithErrorV2(chancesGerman, 0.999)) * germanText.Length}\n");

                            Console.WriteLine($"Ошибка = 0.1. Количество информации сообщения. Язык - {russianChecker.AlphabetName}: {(entropyRussian - russianChecker.computeTextEntropyWithErrorV2(chancesRussian, 0.1)) * russianText.Length}");
                            Console.WriteLine($"Ошибка = 0.5. Количество информации сообщения. Язык - {russianChecker.AlphabetName}: {(entropyRussian - russianChecker.computeTextEntropyWithErrorV2(chancesRussian, 0.5)) * russianText.Length}");
                            Console.WriteLine($"Ошибка = 1. Количество информации сообщения. Язык - {russianChecker.AlphabetName}: {(entropyRussian - russianChecker.computeTextEntropyWithErrorV2(chancesRussian, 0.999)) * russianText.Length}\n");

                            Console.WriteLine($"Ошибка = 0.1. Количество информации сообщения. Язык - {germanCheckerBin.AlphabetName}: {germanCheckerBin.computeTextEntropyWithError(chancesGermanBin, 0.1) * binTextGerman.Length}");
                            Console.WriteLine($"Ошибка = 0.5. Количество информации сообщения. Язык - {germanCheckerBin.AlphabetName}: {germanCheckerBin.computeTextEntropyWithError(chancesGermanBin, 0.5) * binTextGerman.Length}");
                            Console.WriteLine($"Ошибка = 1. Количество информации сообщения. Язык - {germanCheckerBin.AlphabetName}: {germanCheckerBin.computeTextEntropyWithError(chancesGermanBin, 0.999) * binTextGerman.Length}\n");

                            Console.WriteLine($"Ошибка = 0.1. Количество информации сообщения. Язык - {russianCheckerBin.AlphabetName}: {russianCheckerBin.computeTextEntropyWithError(chancesRussianBin, 0.1) * binTextRussian.Length}");
                            Console.WriteLine($"Ошибка = 0.5. Количество информации сообщения. Язык - {russianCheckerBin.AlphabetName}: {russianCheckerBin.computeTextEntropyWithError(chancesRussianBin, 0.5) * binTextRussian.Length}");
                            Console.WriteLine($"Ошибка = 1. Количество информации сообщения. Язык - {russianCheckerBin.AlphabetName}: {(russianCheckerBin.computeTextEntropyWithError(chancesRussianBin, 0.999) * binaryEntropyRussian) * binTextRussian.Length}\n");


                            Console.ReadKey();

                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
    }
}
