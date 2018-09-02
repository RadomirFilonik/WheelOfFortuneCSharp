using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheeleOfFortune.Models
{
    public class AvailableLetters
    {
        private readonly char[] letters = new char[26];

        public AvailableLetters()
        {
            //there is 26 letter in English alphabet
            for (int i = 0; i < 26; i++)
            {
                letters[i] = (char)('A' + i);
            }
        }

        public void CheckAndRemoveLetterFromAvailableLetters(char letter)
        {
            if(letter >= 'A' && letter <= 'Z')
            {
                //'A' char have value 65 in Ascii code.
                if (letters[letter - 65] == ' ')
                {
                    Console.WriteLine();
                    Console.WriteLine("Already used.");
                }
                else
                {
                    letters[letter - 65] = ' ';
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("You choose wrong charater");
            }
        }

        public char[] GetAvailableLetters()
        {
            return letters;
        }

    }
}
