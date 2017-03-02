using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudNoty.Core.Query
{
    public class QNoty
    {
        public async Task<List<Config.Noty>> Notys(int ID, MySql.Data.MySqlClient.MySqlConnection conn)
        {
            try
            {

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * from noty where id=@user";
                cmd.Parameters.AddWithValue("@user", ID);
                cmd.Connection = conn;
                await conn.OpenAsync();
                MySqlDataReader uid = cmd.ExecuteReader();
                List<Config.Noty> notylist = new List<Config.Noty>();
                if (await uid.ReadAsync())
                {
                    Config.Noty noty = new Config.Noty();
                    noty.ID = Convert.ToInt16(uid["id"].ToString());
                    noty.Title = uid["title"].ToString();
                    noty.Autor = Convert.ToInt16(uid["autor"].ToString());
                    noty.Location = uid["location"].ToString();
                    noty.EncrAlg = uid["encr_alg"].ToString();
                    noty.LastEdit = Convert.ToDateTime(uid["last_edit"]);
                    noty.CreationDate = Convert.ToDateTime(uid["creation_date"]);
                    notylist.Add(noty);
                }
                else
                {
                    return null;
                }

                return notylist;

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

