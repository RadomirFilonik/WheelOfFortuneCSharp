using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheeleOfFortune.Helpers
{
    public class DisplayPasswod
    {

        public string DisplayPasswordFromHide(string secretPassword, char charToReveal, string password)
        {
            string revealdPassword = "";
            char innerChar = char.ToUpper(charToReveal);

            for (int i = 0; i < password.Length; i++)
            {
                if (secretPassword[i] == '_')
                {
                    if (password[i] == innerChar)
                    {
                        revealdPassword += password[i];
                    }
                    else
                    {
                        revealdPassword += '_';
                    }
                }
                else
                {
                    if (secretPassword[i] == ' ')
                    {
                        revealdPassword += ' ';
                    }
                    else
                    {
                        revealdPassword += password[i];
                    }
                }
            }
            return revealdPassword;
        }

    }
}

