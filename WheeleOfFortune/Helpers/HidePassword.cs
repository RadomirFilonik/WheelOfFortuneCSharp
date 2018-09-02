using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheeleOfFortune.Helpers
{
    public class HidePassword
    {
        public string ChangeToSecretPassword(string password)
        {
            string secretPassword = "";

            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] == ' ')
                {
                    secretPassword += ' ';
                }
                else
                {
                    secretPassword += '_';
                }
            }
            return secretPassword;
        }
    }
}
