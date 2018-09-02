using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheeleOfFortune.Helpers
{
    public class LetterInPassword
    {
        public int HowManyUserLetterInPassword(char key, string password)
        {
            int numberOfLetter = 0;
            // password.Count( x => x == key )
            for (int i = 0; i < password.Length; i++)
            {
                if(password[i] == key)
                {
                    numberOfLetter++;
                }
            }

            return numberOfLetter;
        }
    }
}
