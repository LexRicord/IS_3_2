using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace is_lab_8
{
    class Program
    {
        public static readonly int a = 421; // multi
        public static readonly int c = 1663; // incr
        public static readonly int n = 7875; // mod
        public static readonly int x = 5; // start value (seed)
        public static readonly int length = 13; // seq length

        public static int LCGnext(int prev, int index)
        {
            int res = (a * prev + c) % n;
            Console.WriteLine($"x{index} = ({a}*{prev} + {c}) mod {n} = {res}");
            return res;
        }

        static void Main(string[] args)
        {
            //---------- LCG ----------------

            int[] seq = new int[length];

            Console.WriteLine($"n = {n} (модуль)");
            Console.WriteLine($"x = {x} (начальное значение)\n");
            int buf = x;

            long OldTicks = DateTime.Now.Ticks;
            for (int i = 0; i < length; i++)
            {
                buf = LCGnext(buf, i);
                seq[i] = buf;
            }
            Console.Write("\nСгенерированная последовательность: ");
            foreach (int item in seq)
            {
                Console.Write($"{item}; ");
            }
            Console.WriteLine($"\nВремя генерации: {(DateTime.Now.Ticks - OldTicks) / 1000} мс");

            //----------- RC4 ---------------

            Console.WriteLine("\n\n\n ----------- RC4 ---------------\n");

            int[] ikey = { 123, 125, 41, 84, 203 };
            byte[] key = new byte[ikey.Length];

            for (int i = 0; i < ikey.Length; i++)
            {
                key[i] = Convert.ToByte(ikey[i]);
            }

            RC4 rc = new RC4(key);
            RC4 rc2 = new RC4(key);
            byte[] testBytes = ASCIIEncoding.ASCII.GetBytes("Herman Alexander");

            long OldTicksRC4 = DateTime.Now.Ticks;

            byte[] encrypted = rc.Encode(testBytes, testBytes.Length);
            Console.WriteLine($"Зашифрованнная строка: {ASCIIEncoding.ASCII.GetString(encrypted)}");

            byte[] decrypted = rc2.Encode(encrypted, encrypted.Length);
            Console.WriteLine($"Расшифрованнная строка: {ASCIIEncoding.ASCII.GetString(decrypted)}");

            Console.WriteLine($"\nВремя зашифрования и расшифрования: {(DateTime.Now.Ticks - OldTicksRC4) / 1000} мс");

            Console.ReadKey();
        }
    }
}