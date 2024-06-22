using System.Linq;

namespace is_lab_8
{
    public class RC4
    {
        byte[] S = new byte[256]; // Массив-перестановка, содержащий все байты от 0x00 до 0xFF
        int x = 0; // Переменные-счетчики
        int y = 0;

        public RC4(byte[] key)
        {
            // Алгоритм ключевого расписания (Key-Scheduling Algorithm) для инициализации S ключом
            for (int i = 0; i < 256; i++)
            {
                S[i] = (byte)i;
            }

            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % key.Length]) % 256;
                S.Swap(i, j); // Поменять местами        
            }
        }

        // Для каждого байта массива исходных данных запрашиваем байт ключа и объединяем их при помощи XOR (^)
        public byte[] Encode(byte[] dataB, int size)
        {
            byte[] data = dataB.Take(size).ToArray();
            byte[] cipher = new byte[data.Length];

            for (int m = 0; m < data.Length; m++)
            {
                cipher[m] = (byte)(data[m] ^ keyItem());
            }
            return cipher;
        }

        // При каждом вызове отдает следующий байт ключевого потока, который будем объединять XOR'ом с байтом исходных данных
        private byte keyItem()
        {
            x = (x + 1) % 256;
            y = (y + S[x]) % 256;

            S.Swap(x, y);

            return S[(S[x] + S[y]) % 256];
        }
    }

    static class SwapExt
    {
        public static void Swap<T>(this T[] array, int index1, int index2)
        {
            T temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
    }
}