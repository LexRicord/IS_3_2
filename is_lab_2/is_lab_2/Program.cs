using System;
using System.IO;
using is_lab_2;

class Program
{

    static void Main()
    {
        string inputFilePath = "D:\\repos\\IS_3_2\\is_lab_2\\is_lab_2\\input.txt";
        string textName = "Alexander";
        string textSurname = "Herman";
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Dictionary<char, int>  chancesInput, chancesOutput;
        const string fileName = "D:\\repos\\IS_3_2\\is_lab_2\\is_lab_2\\bin\\Debug\\net7.0\\Lab2.xlsx";
        try
        {
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("Файл не найден.");
                return;
            }

            string text = File.ReadAllText(inputFilePath);

            TextAnalyzer analyzer = new TextAnalyzer(text);
            chancesInput = analyzer.Analyze();

            string base64Text = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(text));
            string outputFilePath = Path.ChangeExtension(inputFilePath, ".base64.txt");
            File.WriteAllText(outputFilePath, base64Text);
            TextAnalyzer textAnalyzer = new TextAnalyzer(base64Text);
            chancesOutput = textAnalyzer.Analyze();

            ExcelDocumentCreator<char, int> excel = new ExcelDocumentCreator<char, int>(new System.IO.FileInfo(fileName));
            excel.createWorksheet("first");
            excel.addValuesFromDict(chancesInput, "first", 0);
            excel.addValuesFromDict(chancesOutput, "first", 3);
            excel.SaveAndClose();

            Console.WriteLine($"Файл успешно закодирован и сохранен по пути: {outputFilePath}\n\n");
            
            string textNameBase64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(textName));
            string textSurnameBase64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(textSurname));

            string xorResult3 = BufferProcessor.PadAndXOR(textSurname, textName, BufferProcessor.InputFormat.ASCII, BufferProcessor.InputFormat.ASCII);
            Console.WriteLine($"Результат операции a XOR b XOR b (в ASCII): {xorResult3}");
            string xorResult4 = BufferProcessor.PadAndXOR(textSurnameBase64, textNameBase64, BufferProcessor.InputFormat.Base64, BufferProcessor.InputFormat.Base64);
            Console.WriteLine($"Результат операции a XOR b XOR b (в ASCII): {xorResult4}");

            string xorResult = BufferProcessor.PadAndXOR(textSurname, BufferProcessor.PadAndXOR(textName, textName, BufferProcessor.InputFormat.ASCII, BufferProcessor.InputFormat.ASCII),
            BufferProcessor.InputFormat.ASCII, BufferProcessor.InputFormat.ASCII);
            Console.WriteLine($"Результат операции a XOR b XOR b (в ASCII): {xorResult}");
            string xorResult2 = BufferProcessor.PadAndXOR(textSurnameBase64, BufferProcessor.PadAndXOR(textNameBase64, textNameBase64, BufferProcessor.InputFormat.Base64, BufferProcessor.InputFormat.Base64), 
                BufferProcessor.InputFormat.Base64, BufferProcessor.InputFormat.Base64);
            Console.WriteLine($"Результат операции a XOR b XOR b (в base64): {xorResult2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}