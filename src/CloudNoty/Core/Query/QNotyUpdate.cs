using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudNoty.Core.Query
{
    public class QNotyUpdate
    {

        public async Task POST(Config.Noty noty, MySql.Data.MySqlClient.MySqlConnection conn)
        {
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE noty SET title=@title, content=@content, encr_alg=@encr_alg WHERE id=@id";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@title", noty.Title);
                cmd.Parameters.AddWithValue("@content", noty.Content);
                cmd.Parameters.AddWithValue("@encr_alg", noty.EncrAlg);
                cmd.Parameters.AddWithValue("@id", noty.ID);
                await conn.OpenAsync();
                var result = Convert.ToString(await cmd.ExecuteNonQueryAsync());
                if (Convert.ToInt16(result) == 1)
                {
                    MessageBox.Show(@"Noty Updated!", @"Saved", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                   
                }
                else
                {
                    MessageBox.Show(
                        @"Error, we can not update! Try with another username. " + Environment.NewLine + "For more informations please contact the developers.",
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
