using System;

namespace is_lab_2
{
    class BufferProcessor
    {
        public static string PadAndXOR(string bufferA, string bufferB, InputFormat formatA, InputFormat formatB)
        {
            byte[] bytesA = ConvertInputToBytes(bufferA, formatA);
            byte[] bytesB = ConvertInputToBytes(bufferB, formatB);

            int maxLength = Math.Max(bytesA.Length, bytesB.Length);

            bytesA = PadBuffer(bytesA, maxLength);
            bytesB = PadBuffer(bytesB, maxLength);

            byte[] resultBytes = new byte[maxLength];
            for (int i = 0; i < maxLength; i++)
            {
                resultBytes[i] = (byte)(bytesA[i] ^ bytesB[i]);
            }

            string result = ConvertBytesToString(resultBytes, formatA);

            return result;
        }

        private static byte[] ConvertInputToBytes(string input, InputFormat format)
        {
            return format switch
            {
                InputFormat.ASCII => System.Text.Encoding.ASCII.GetBytes(input),
                InputFormat.Base64 => Convert.FromBase64String(input),
                _ => throw new ArgumentException("Unsupported input format"),
            };
        }

        private static byte[] PadBuffer(byte[] buffer, int length)
        {
            Array.Resize(ref buffer, length);
            return buffer;
        }

        private static string ConvertBytesToString(byte[] bytes, InputFormat format)
        {
            return format switch
            {
                InputFormat.ASCII => System.Text.Encoding.ASCII.GetString(bytes),
                InputFormat.Base64 => Convert.ToBase64String(bytes),
                _ => throw new ArgumentException("Unsupported output format"),
            };
        }

        public enum InputFormat
        {
            ASCII,
            Base64
        }
    }
}
