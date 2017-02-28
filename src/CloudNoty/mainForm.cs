using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloudNoty.Config;
using CloudNoty.Core.Query;
using CloudNoty.Core.Operators;
using MySql.Data.MySqlClient;
using System.IO;
using Newtonsoft.Json;

namespace CloudNoty
{
    public partial class mainForm : Form
    {
        public static Config.Config StdConfig = new Config.Config();
        public static Config.Config.LoginStatus loginStatus;
        public static Config.Config.RegisterStatus registerStatus;
        public static Config.Config.Permissions permissions;

        public mainForm()
        {
            InitializeComponent();
            StdConfig.MYSQLCONNECTION = new MySqlConnection(Config.Config.MYSQLCONNECTIONSTRING);
        }

        private async void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                    Core.Helper.CampsController controller = new Core.Helper.CampsController();

                    bool response = controller.Login(txtUsr, txtPsw);

                    if (response)
                        return;

                    DoLogin login = new DoLogin();
                    DoPermissions DoPermissions = new DoPermissions();

                    try
                    {
                        loginStatus = await login.PasswordCheck(txtUsr.Text, txtPsw.Text, StdConfig.MYSQLCONNECTION);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (loginStatus == Config.Config.LoginStatus.LoggedIn)
                        {
                            btn_Login.Enabled = false;
                            txtUsr.Enabled = false;
                            txtPsw.Enabled = false;
                            btn_Login.Text = "Logged";

                            System.Windows.Forms.MessageBox.Show("Account " + txtUsr.Text + " successfully logged in!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //Check User Permissions
                            try
                            {
                                permissions = await DoPermissions.PermissionsCheck(txtUsr.Text, StdConfig.MYSQLCONNECTION);
                                this.Text = "CloudNoty - ID: " + Config.Config.UID.ToString() + " - Permissions: " + permissions.ToString();
                           
                                //await Task.Run(LoadNotys);
                            }
                            catch (Exception ex)
                            {
                                System.Windows.Forms.MessageBox.Show("Error: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else if (loginStatus == Config.Config.LoginStatus.WrongPassword)
                        {
                            System.Windows.Forms.MessageBox.Show("The password does not match for the account " + txtUsr.Text + "." + Environment.NewLine + "Contact an administrator for more informations!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (loginStatus == Config.Config.LoginStatus.InvalidAccount)
                        {
                            System.Windows.Forms.MessageBox.Show("Account " + txtUsr.Text + " does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
               
            }
            catch (Exception ex)
            { System.Windows.Forms.MessageBox.Show(ex.ToString()); }
        }

        private async void mainForm_Load(object sender, EventArgs e)
        {
            //CHECK LOCAL CONFIG
            LocalConfig cfg = new LocalConfig();

            string EncryptedconfigText = null;
            string DecryptedconfigText = null;

            if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Config.Config.LOCALCONFIGFILE))
            {
                var file = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Config.Config.LOCALCONFIGFILE;

                try
                {
                    EncryptedconfigText = File.ReadAllText(file).ToString();
                }
                catch(FileLoadException ex)
                {
                    MessageBox.Show("Can't load config file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    DecryptedconfigText = Core.Encryption.FileEncryption.Decrypt(EncryptedconfigText, Config.Config.LOCALKEY);

                    cfg = await JsonConvert.DeserializeObjectAsync<Config.LocalConfig>(DecryptedconfigText);


                    //Caricare la configurazione
                    if(!string.IsNullOrEmpty(cfg.Username) && !string.IsNullOrEmpty(cfg.Password)  && cfg.RememberMe == true)
                    {
                        txtUsr.Text = cfg.Username;
                        txtPsw.Text = cfg.Password;
                        checkBox1.Checked = true;
                    }
                    
                }
            }

        }

        private async void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                string file = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Config.Config.LOCALCONFIGFILE;

                if (checkBox1.Checked == true)
                {
                    //Save LOCAL CONFIG
                    LocalConfig cfg = new LocalConfig();

                    string EncryptedconfigText = null;
                    string DecryptedconfigText = null;

                    cfg.Username = txtUsr.Text;
                    cfg.Password = txtPsw.Text;
                    cfg.RememberMe = true;

                    DecryptedconfigText = JsonConvert.SerializeObject(cfg);
                    EncryptedconfigText = Core.Encryption.FileEncryption.Encrypt(DecryptedconfigText, Config.Config.LOCALKEY);
                    
                    File.WriteAllText(file, EncryptedconfigText);
                }
                else
                {
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
         
         
        }
    }

}
