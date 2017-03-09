using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace CloudNoty.Core.Forms
{
    public partial class landingForm : Form
    {
        public static Config.Cfg StdConfig = new Config.Cfg();
        public List<Config.Noty> notyList = new List<Config.Noty>();
        public landingForm()
        {
            InitializeComponent();
            StdConfig.MYSQLCONNECTION = new MySqlConnection(Config.Cfg.MYSQLCONNECTIONSTRING);

        }

        private void landingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private async void landingForm_Load(object sender, EventArgs e)
        {
            listNoty.FullRowSelect = true;

            LoadNotys();
        }

        private async void LoadNotys()
        {
            try
            {
                if (Config.Cfg.LoggedIn == false)
                {
                    MessageBox.Show("You are NOT Logged in!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }


                Core.Operators.DoNoty doNoty = new Operators.DoNoty();

                notyList = await doNoty.Notys(Config.Cfg.UID, StdConfig.MYSQLCONNECTION);

                listNoty.Items.Clear();

                foreach (Config.Noty noty in notyList)
                {
                    var lvi = new ListViewItem(new[] { noty.ID.ToString(), noty.Title.ToString(), noty.LastEdit.ToString(), noty.CreationDate.ToString() });
                    listNoty.Items.Add(lvi);
                }

            }
            catch (Exception ex)
            {

            }


        }

        private async void listNoty_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           foreach(Config.Noty noty in notyList)
            {
                if(listNoty.SelectedItems[0].Text != string.Empty)
                {
                    if (noty.ID.ToString() == listNoty.SelectedItems[0].Text)
                    {
                        Query.QNotyContent getContent = new Query.QNotyContent();
                        string data = await getContent.Notys(Convert.ToInt16(listNoty.SelectedItems[0].Text), StdConfig.MYSQLCONNECTION);
                        noty.Content = data;
                        noteForm noteF = new noteForm(noty, false);
                        this.Hide();
                        noteF.ShowDialog();
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Config.Noty noty = new Config.Noty();
            noty.Autor = Config.Cfg.UID;
            noty.EncrAlg = "RijndaelManaged";
            noteForm noteF = new noteForm(noty, true);
            this.Hide();
            noteF.ShowDialog();
        }
    }
}
