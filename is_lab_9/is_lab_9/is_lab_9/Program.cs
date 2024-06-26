﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace is_lab_9
{
    class Program
    {
        public static readonly int z = 10;
        public static readonly int a = 31;
        public static readonly int n = 420;
        public static readonly int[] d = { 103, 107, 211, 430, 863, 1716, 3449, 6907, 13807, 27610 };
        public static string M = "Herman";

        static void Main(string[] args)
        {
            // Генерация сверхвозр. послед
            var r = new BackPack();
            int[] d2 = r.Generate(8);
            Console.WriteLine($"Закрытый ключ d: {r.Str(d)}");

            // Вычисление норм. послед
            int[] e = r.getNorm(d, a, n, z);
            Console.WriteLine($"Открытый ключ e: {r.Str(e)}\n");

            //Зашифрование ФИО
            long OldTicks = DateTime.Now.Ticks;
            int[] C = r.getcipher(e, M, z);
            Console.WriteLine($"\nЗашифров сооб C: {r.Str(C)}");
            Console.WriteLine($"Время: {(DateTime.Now.Ticks - OldTicks) / 1000} мс\n");


            //Расшифрование ФИО
            int a_1 = r.a_1(a, n);

            int[] S = new int[C.Length];
            string M2 = "";

            for (int i = 0; i < C.Length; i++)
            {
                S[i] = (C[i] * a_1) % n;
            }
            Console.WriteLine($"Вектор  весов S: {r.Str(S)}      a^(-1) = {a_1}");

            OldTicks = DateTime.Now.Ticks;
            foreach (int Si in S)
            {
                string M2i = r.decipher(d, Si, z);//110000
                M2 += M2i + " ";
            }
            Console.WriteLine($"Расшифр сообщ  : {M2}");
            Console.WriteLine($"Время: {(DateTime.Now.Ticks - OldTicks) / 1000} мс\n");

            M2 = M2.Replace(" ", "");
            var stringArray = Enumerable.Range(0, M2.Length / 8).Select(i => Convert.ToByte(M2.Substring(i * 8, 8), 2)).ToArray();
            var str = Encoding.UTF8.GetString(stringArray);
            Console.WriteLine(str);

            Console.ReadKey();
        }
    }
}
