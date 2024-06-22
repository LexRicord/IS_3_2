using System;
using System.Reflection.Metadata.Ecma335;

namespace crypt_lab_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int c = 0;
            while (true)
            {
                Console.WriteLine("Введите номер задания:");
                Console.WriteLine("1: НОД (два числа)");
                Console.WriteLine("2: Найти простые числа в интервале");
                Console.WriteLine("3: НОД (три числа)");
                Console.WriteLine("4: Является ли конкатенация простым?");
                if (!int.TryParse(Console.ReadLine(), out c))
                {
                    c = -1;
                }
                switch (c)
                {
                    case 1:
                        {
                            int x = 0, y = 0;
                            Console.Write("Введите первое число: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.Write("Введите второе число: ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.WriteLine($"НОД двух чисел ({x},{y}) равен: {NumCrypt.CalculateLeastCommonDivisor(x, y)} ");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        }
                    case 2:
                        {
                            int x = 0, y = 0;
                            double y2;
                            Console.Write("Введите первое число: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.Write("Введите второе число, больше первого: ");
                            if (!int.TryParse(Console.ReadLine(), out y) || y <= x)
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }

                            Console.WriteLine($"Простые множители числа {x}: {NumCrypt.PrimeFactorization(x)}");
                            Console.WriteLine($"Простые множители числа {y}: {NumCrypt.PrimeFactorization(y)}");

                            NumCrypt.FindPrimeNumberInInterval(x, y);
                            y2 = Convert.ToDouble(y);
                            y2 = y2 / Math.Log2(y2);
                            Console.WriteLine($"y/ln(y) = " + y2);
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                    case 3:
                        {
                            int x = 0, y = 0, z = 0;
                            Console.Write("Введите первое число: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.Write("Введите второе число: ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.Write("Введите третье число: ");
                            if (!int.TryParse(Console.ReadLine(), out z))
                            {
                                Console.Write("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }
                            Console.WriteLine($"НОД трех чисел ({x},{y},{z}) равен: {NumCrypt.CalculateLeastCommonDivisor(x, y, z)} ");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        }
                    case 4:
                        {
                            int m = 0, n = 0;
                            Console.Write("Введите первое число (m): ");
                            if (!int.TryParse(Console.ReadLine(), out m))
                            {
                                Console.WriteLine("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }

                            Console.Write("Введите второе число (n): ");
                            if (!int.TryParse(Console.ReadLine(), out n))
                            {
                                Console.WriteLine("Ошибка!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            }

                            bool isConcatenationPrime = NumCrypt.IsConcatenationPrime(m, n);

                            Console.WriteLine($"Число, состоящее из конкатенации цифр {m} и {n}, " +
                                              $"{(isConcatenationPrime ? "является" : "не является")} простым.");
                            Console.ReadKey();
                            Console.Clear();
                            break;
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
