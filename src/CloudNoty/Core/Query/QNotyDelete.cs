using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudNoty.Core.Query
{
    public class QNotyDelete
    {
        public async Task<bool> Delete(string id, MySql.Data.MySqlClient.MySqlConnection conn)
        {
            try
            {

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "delete from noty where id=@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;
                await conn.OpenAsync();
                MySqlDataReader uid = cmd.ExecuteReader();

                if (await uid.ReadAsync())
                    return true;
                else
                {
                    return false;
                }

            }

            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            finally
            {
                conn.Close();
            }
        }
    }
}
