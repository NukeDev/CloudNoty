using CloudNoty.Config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudNoty.Core.Query
{
    public class QRegister
    {
        public async Task<Cfg.RegisterStatus> SET(string user, string password, string email, MySql.Data.MySqlClient.MySqlConnection conn)
        {

            try
            {
                bool validate = await registration_user_validate(user, email, conn);
                if(validate == true)
                {
                    MessageBox.Show(
                       @"Error, we can not register your account! Account username/email already registered. " + Environment.NewLine + "For more informations please contact the developers.",
                       @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return Cfg.RegisterStatus.AlreadyExists;
                }

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "insert into users(user,password,email) values('" + user +
                                  "','" + password + "','" + email + "');";
                cmd.Connection = conn;
                await conn.OpenAsync();
                var result = Convert.ToString(await cmd.ExecuteNonQueryAsync());
                if (Convert.ToInt16(result) == 1)
                {
                    MessageBox.Show(@"Account " + user + " successfully registered!", @"Registration", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return Cfg.RegisterStatus.Registered;
                }
                else
                {
                    MessageBox.Show(
                        @"Error, we can not register your account! Try with another username. " + Environment.NewLine + "For more informations please contact the developers.",
                        @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return Cfg.RegisterStatus.Error;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Cfg.RegisterStatus.Error;
            }

            finally
            {
                conn.Close();
            }
        }

        private async Task<bool> registration_user_validate(string user, string email, MySql.Data.MySqlClient.MySqlConnection con)
        {
            MySqlConnection conn = con;

            try
            {

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * from users where user=@user or email=@email";
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Connection = conn;
                await conn.OpenAsync();
                MySqlDataReader login = cmd.ExecuteReader();
                if (await login.ReadAsync())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch (MySqlException ex)
            {
                MessageBox.Show(@"Error: " + ex, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            finally
            {
                conn.Close();
            }
        }
    }
}
