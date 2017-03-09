using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudNoty.Core.Query
{
    public class QNotyCreate
    {

        public async Task SET(Config.Noty noty, MySql.Data.MySqlClient.MySqlConnection conn)
        {
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "insert into noty(title,content,autor,encr_alg) values('" + noty.Title +
                                               "','" + noty.Content + "','" + noty.Autor + "','" + noty.EncrAlg + "');";
                cmd.Connection = conn;
                await conn.OpenAsync();
                var result = Convert.ToString(await cmd.ExecuteNonQueryAsync());
                if (Convert.ToInt16(result) == 1)
                {
                    MessageBox.Show(@"Noty Created!", @"Saved", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                   
                }
                else
                {
                    MessageBox.Show(
                        @"Error, we can not Create the Noty! " + Environment.NewLine + "For more informations please contact the developers.",
                        @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                conn.Close();
            }
        }
    }
}
