using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_lab_3
{
    internal class NumCrypt
    {
        public static int CalculateLeastCommonDivisor(int x, int y)
        {
            while (x != 0 && y != 0)
            {
                if (x > y)
                {
                    x -= y;
                }
                else
                {
                    y -= x;
                }
            }
            return Math.Max(x, y);

        }
        public static int CalculateLeastCommonDivisor(int a, int b, int c)
        {
            int lcd = Math.Min(a, Math.Min(b, c));
            for (; lcd > 1; lcd--)
            {
                if (a % lcd == 0 && b % lcd == 0 && c % lcd == 0)
                    break;
            }
            return lcd;
        }

        private static bool isPrime(int n)
        {
            if (n > 1)
            {
                for (int i = 2; i < n; i++)
                {
                    if (n % i == 0) return false;               // если n делится без остатка на i - возвращаем false (число не простое)
                }
                return true;                  // если программа дошла до данного оператора, то возвращаем true (число простое) - проверка пройдена
            }
            else return false;
        }

        public static void FindPrimeNumberInInterval(int m, int n)
        {
            int counter = 0;
            if (n < m)
            {
                Console.WriteLine("Неверный промежуток");
            }

            Console.Write($"Простые числа интервала [{m},{n}]: ");

            for (int i = m; i <= n; i++)
            {
                if (isPrime(i))
                {
                    Console.Write(i.ToString() + " ");
                    counter++;
                }
            }
            Console.WriteLine();
            Console.WriteLine($"Количество простых чисел: {counter}");

        }

        public static string PrimeFactorization(int n)
        {
            if (n < 2)
            {
                return "1";
            }

            StringBuilder result = new StringBuilder();
            result.Append($"{n} = ");

            int divisor = 2;
            while (n > 1)
            {
                int power = 0;
                while (n % divisor == 0)
                {
                    n /= divisor;
                    power++;
                }

                if (power > 0)
                {
                    result.Append($"{divisor}");

                    if (power > 1)
                    {
                        result.Append($"^{power}");
                    }

                    if (n > 1)
                    {
                        result.Append(" * ");
                    }
                }

                divisor++;
            }

            return result.ToString();
        }

        public static bool IsConcatenationPrime(int m, int n)
        {
            string concatenatedNumberStr = m.ToString() + n.ToString();
            int concatenatedNumber;

            if (int.TryParse(concatenatedNumberStr, out concatenatedNumber))
            {
                return isPrime(concatenatedNumber);
            }
            else
            {
                Console.WriteLine("Ошибка при преобразовании входных данных в число.");
                return false;
            }
        }

    }
}
