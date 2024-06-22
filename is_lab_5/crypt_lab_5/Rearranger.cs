using System.Text;

namespace crypt_lab_5_rearranger
{
    internal class Rearranger
    {
        private List<KeyValuePair<int, char>> keyHorizontal;
        private List<KeyValuePair<int, char>> keyVertical;
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public void Initialize(string text, List<KeyValuePair<int, char>> keyVertical, List<KeyValuePair<int, char>> keyHorizontal)
        {
            if (keyVertical.Count * keyHorizontal.Count >= text.Length)
            {
                Text = text;
            }
            else
            {
                throw new Exception("Сообщение слишком длинное для данных ключей!");
            }
            this.keyVertical = keyVertical;
            this.keyHorizontal = keyHorizontal;
        }

        public void PrintKeys()
        {
            Console.WriteLine("Горизонтальные ключи:");
            foreach (var item in keyHorizontal)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }

            Console.WriteLine("Вертикальные ключи:");
            foreach (var item in keyVertical)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }

        public void PrintMatrix(char[,] input)
        {
            Console.WriteLine("Матрица:");
            int tableWidth = keyVertical.Count;
            int tableHeight = keyHorizontal.Count;
            for (int w = 0; w < tableWidth; w++)
            {
                for (int i = 0; i < tableHeight; i++)
                {
                    Console.Write($"{(input[w, i] == ' ' || input[w, i] == '\0' ? '*' : input[w, i]),5}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public char[,] CreateMatrix(string input)
        {
            int tableWidth = keyVertical.Count;
            int tableHeight = keyHorizontal.Count;
            char[,] table = new char[tableWidth, tableHeight];
            int l = 0;
            for (int w = 0; w < tableWidth; w++)
            {
                for (int i = 0; i < tableHeight; i++)
                {
                    if (l == input.Length) break;
                    table[w, i] = input[l++];
                }
                if (l == input.Length) break;
            }
            return table;
        }

        public string CreateString(char[] input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int w = 0; w < input.Length; w++)
            {
                stringBuilder.Append(input[w]);
            }
            return stringBuilder.ToString();
        }

        public string CreateString(char[,] input)
        {
            int tableWidth = keyVertical.Count;
            int tableHeight = keyHorizontal.Count;
            StringBuilder stringBuilder = new StringBuilder();
            for (int w = 0; w < tableWidth; w++)
            {
                for (int l = 0; l < tableHeight; l++)
                    stringBuilder.Append(input[w, l]);
            }
            return stringBuilder.ToString();
        }

        public string Encrypt()
        {
            char[,] table = CreateMatrix(Text);

            int tableWidth = keyVertical.Count;
            int tableHeight = keyHorizontal.Count;

            char[,] result = new char[tableWidth, tableHeight];
            int iteration = 0;
            while (iteration < tableHeight) 
            {
                int k = 0;
                for (int x = 0; x < tableWidth; x++) 
                {
                    k = keyHorizontal.IndexOf(keyHorizontal.Find(l => l.Key == iteration + 1)); 
                    if (iteration < result.GetLength(1) && x < result.GetLength(0))
                    {
                        
                        result[x, iteration] = table[x, k];
                    }
                    else
                    {
                        Console.WriteLine($"Выход за границы массива: result[{x}, {iteration}]");
                    }
                }
                iteration++;
            }

            Console.WriteLine("После горизонтальной перестановки:\n");
            PrintMatrix(result);

            char[,] resultV = new char[tableWidth, tableHeight];
            iteration = 0;
            while (iteration < tableWidth)
            {
                int k = 0;
                for (int y = 0; y < tableHeight; y++)
                {
                    k = keyVertical.IndexOf(keyVertical.Find(l => l.Key == iteration + 1));
                    if (iteration < resultV.GetLength(0) && y < resultV.GetLength(1))
                    {
                        resultV[iteration, y] = result[k, y];
                    }
                    else
                    {
                        Console.WriteLine($"Выход за границы массива: resultV[{y}, {iteration}]");
                    }
                }
                iteration++;
            }
            Console.WriteLine("После вертикальной перестановки:\n");
            PrintMatrix(resultV);

            return CreateString(resultV);
        }
    }
}