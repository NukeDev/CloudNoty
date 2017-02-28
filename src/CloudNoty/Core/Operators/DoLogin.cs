using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudNoty.Config;
using CloudNoty.Core.Query;
using CloudNoty.Core.Encryption;

namespace CloudNoty.Core.Operators
{
    public class DoLogin
    {
        public async Task<Config.Config.LoginStatus> PasswordCheck(string user, string password, MySql.Data.MySqlClient.MySqlConnection msq)
        {
            QLogin login = new QLogin();
            PasswordEncryption Crypt = new PasswordEncryption();
            string x = await login.GetPasswordHash(user, msq);

            if (x == null)
            {
                return Config.Config.LoginStatus.InvalidAccount;
            }

            if (Crypt.Verify(password, x) == true)
            {
                return Config.Config.LoginStatus.LoggedIn;
            }
            else
            {
                return Config.Config.LoginStatus.WrongPassword;
            }
        }
    }
}
