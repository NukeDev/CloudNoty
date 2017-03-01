using CloudNoty.Config;
using CloudNoty.Core.Encryption;
using CloudNoty.Core.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudNoty.Core.Operators
{
    public class DoRegister
    {
        public async Task<Cfg.RegisterStatus> Register(string user, string password, string email, MySql.Data.MySqlClient.MySqlConnection msq)
        {
            QRegister reg = new QRegister();
            PasswordEncryption enc = new PasswordEncryption();

            var psw = enc.Encrypt(password);

            Cfg.RegisterStatus regStatus = new Cfg.RegisterStatus();

            regStatus = await reg.SET(user, psw, email, msq);

            return regStatus;
        }
    }
}
