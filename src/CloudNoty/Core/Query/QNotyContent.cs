using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudNoty.Core.Query
{
    public class QNotyContent
    {
        public async Task<string> Notys(int IDNOTY, MySql.Data.MySqlClient.MySqlConnection conn)
        {
            try
            {

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select content from noty where id=@user";
                cmd.Parameters.AddWithValue("@user", IDNOTY);
                cmd.Connection = conn;
                await conn.OpenAsync();
                MySqlDataReader uid = cmd.ExecuteReader();
               
                if(await uid.ReadAsync())
                {               
                    return uid["content"].ToString();
                }


                return null;

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
