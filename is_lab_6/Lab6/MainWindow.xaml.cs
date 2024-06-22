using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace crypt_lab_6
{
    public partial class MainWindow : Window
    {
        string originalAlphabet = "abcdefghijklmnopqrstuvwxyz";
        Rotor L, M, R;
        Reflector reflector;
        public MainWindow()
        {
            originalAlphabet.ToCharArray();
            InitializeComponent();
            L = new Rotor
            {
                Alphabet = new Char[] { 'e', 'k', 'm', 'f', 'l', 'g', 'd', 'q', 'v',
                                'z', 'n', 't', 'o', 'w', 'y', 'h', 'x', 'u',
                                's', 'p', 'a', 'i', 'b', 'r', 'c', 'j'},
                Shift = 3
            };
            M = new Rotor
            {
                Alphabet = new Char[] { 'l', 'e', 'y', 'j', 'v', 'c', 'n', 'i', 'x',
                                'w', 'p', 'b', 'q', 'm', 'd', 'r', 't', 'a',
                                'k', 'z', 'g', 'f', 'u', 'h', 'o', 's' },
                Shift = 1,

            };
            R = new Rotor
            {
                Alphabet = new Char[] { 'f', 's', 'o', 'k', 'a', 'n', 'u', 'e', 'r',
                                'h', 'm', 'b', 't', 'i', 'y', 'c', 'w', 'l',
                                'q', 'p', 'z', 'x', 'v', 'g', 'j', 'd' },
                Shift = 3,
            };

            foreach (char item in L.Alphabet)
            {
                Start_position_Rot_1.Items.Add(item);
            }

            foreach (char item in M.Alphabet)
            {
                Start_position_Rot_2.Items.Add(item);
            }

            foreach (char item in R.Alphabet)
            {
                Start_position_Rot_3.Items.Add(item);
            }

            reflector = new Reflector();
            reflector.Alphabet = new Dictionary<char, char> { { 'a', 'y' }, { 'b', 'r' }, { 'c', 'u' },
            { 'd', 'h' }, { 'e', 'q' }, { 'f', 's' }, { 'g', 'l' }, { 'i', 'p' },
            { 'j', 'x' }, { 'k', 'n' }, { 'm', 'o' }, { 't', 'z' }, { 'v', 'w' }};

        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string originalMessage = Orig_message_input.Text.ToLower().Replace(" ","");
            char encryptedChar;
            string encryptedMessage = "";

            L.PickStartPosition(Start_position_Rot_1.Items[Start_position_Rot_1.SelectedIndex].ToString());
            M.PickStartPosition(Start_position_Rot_2.Items[Start_position_Rot_2.SelectedIndex].ToString());
            R.PickStartPosition(Start_position_Rot_3.Items[Start_position_Rot_3.SelectedIndex].ToString());

            for (int i = 0; i < originalMessage.Length; i++)
            {
                encryptedChar = R.Alphabet[originalAlphabet.IndexOf(originalMessage[i])];
                encryptedChar = M.Alphabet[originalAlphabet.IndexOf(encryptedChar)];
                encryptedChar = L.Alphabet[originalAlphabet.IndexOf(encryptedChar)];

                char reflectedChar;
                if (!reflector.Alphabet.TryGetValue(encryptedChar, out reflectedChar))
                    reflectedChar = reflector.Alphabet.First(key => key.Value == encryptedChar).Key;
                else
                    reflectedChar = reflectedChar;

                encryptedChar = originalAlphabet[Array.IndexOf(L.Alphabet, reflectedChar)];
                encryptedChar = originalAlphabet[Array.IndexOf(M.Alphabet, encryptedChar)];
                encryptedChar = originalAlphabet[Array.IndexOf(R.Alphabet, encryptedChar)];

                encryptedMessage += encryptedChar;

                L.DoShift(M.DoShift(R.DoShift(0)));
            }
            Encrypt_Output.Text = encryptedMessage;
        }

    }
}
