using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudNoty.Core.Query
{
    public class QLogin
    {
        public async Task<string> GetPasswordHash(string user, MySql.Data.MySqlClient.MySqlConnection conn)
        {
            try
            {

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select password from users where user=@user";
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Connection = conn;
                await conn.OpenAsync();
                MySqlDataReader uid = cmd.ExecuteReader();

                if (await uid.ReadAsync())
                    return uid["password"].ToString();
                else
                {
                    return null;
                }

            }

            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            finally
            {
                conn.Close();
            }
        }
    }
}
