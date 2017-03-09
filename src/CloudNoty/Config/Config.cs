using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudNoty.Config
{
    public class Cfg
    {
        public const string MYSQLCONNECTIONSTRING = "Server=;" + "Port=;" + "Database=;" + "Uid=;" + "Password=;" + "AllowUserVariables= true;" + "Convert Zero Datetime=True";
        public const string LOCALCONFIGFILE = "cn.dat";
        public const string LOCALKEY = "";

        public MySqlConnection MYSQLCONNECTION { get; set; }

        public enum LoginStatus
        {
            WrongPassword,
            InvalidAccount,
            LoggedIn
        }
        public enum RegisterStatus
        {
            AlreadyExists,
            Registered,
            Error
        }
        public enum Permissions
        {
            Administrator,
            Pro,
            Medium,
            Basic,
            Null
        }
        public enum EncrAlg
        {
            RijndaelManaged
        }

        public static int UID = 0;
        public static bool LoggedIn = false;
    }
}
