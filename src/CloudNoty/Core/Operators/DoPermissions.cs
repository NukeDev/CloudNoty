using CloudNoty.Config;
using CloudNoty.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudNoty.Core.Operators
{
    public class DoPermissions
    {
        public async Task<Config.Cfg.Permissions> PermissionsCheck(string user, MySql.Data.MySqlClient.MySqlConnection msq)
        {
            Config.Cfg.Permissions permission = new Config.Cfg.Permissions();

            CloudNoty.Core.Query.QPermissions qPermissions = new CloudNoty.Core.Query.QPermissions();

            string x = await qPermissions.GetPermissions(user, msq);

            if(x == null)
            {
                MessageBox.Show("Error while getting Permissions!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                permission = Config.Cfg.Permissions.Null;
            }

            if(x.Contains(":"))
            {
                int uid = Convert.ToInt16(x.Substring(0, Convert.ToInt16(x.IndexOf(':'))));

                string y = x.Substring(x.IndexOf(":") + 1);

                Config.Cfg.UID = uid;
                
                switch (y)
                {
                    case "Administrator":
                        permission = Config.Cfg.Permissions.Administrator;
                        break;
                    case "Pro":
                        permission = Config.Cfg.Permissions.Pro;
                        break;
                    case "Medium":
                        permission = Config.Cfg.Permissions.Medium;
                        break;
                    case "Basic":
                        permission = Config.Cfg.Permissions.Basic;
                        break;
                }

            }

            return permission;

        }
    }
}
