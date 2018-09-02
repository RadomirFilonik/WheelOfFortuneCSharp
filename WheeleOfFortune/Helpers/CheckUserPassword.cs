using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheeleOfFortune.Helpers
{
    public class CheckUserPassword
    {
        public bool ComparePasswords(string userPassword, string gamePassword)
        {
            if(userPassword == gamePassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
