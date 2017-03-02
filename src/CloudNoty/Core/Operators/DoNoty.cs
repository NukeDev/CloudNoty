using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudNoty.Core.Operators
{
    public class DoNoty
    {
        public async Task<List<Config.Noty>> Notys(int ID, MySql.Data.MySqlClient.MySqlConnection conn)
        {
            Query.QNoty qnoty = new Query.QNoty();

            return await qnoty.Notys(ID, conn);
        }
    }
}
