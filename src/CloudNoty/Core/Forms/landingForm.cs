using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudNoty.Core.Forms
{
    public partial class landingForm : Form
    {
        public static Config.Cfg StdConfig = new Config.Cfg();

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
            if(Config.Cfg.LoggedIn == false)
            {
                MessageBox.Show("You are NOT Logged in!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            List<Config.Noty> notyList = new List<Config.Noty>();
            Core.Operators.DoNoty doNoty = new Operators.DoNoty();

            notyList = await doNoty.Notys(Config.Cfg.UID, StdConfig.MYSQLCONNECTION);

            foreach(Config.Noty noty in notyList)
            {
                var lvi = new ListViewItem(new[] { noty.ID.ToString(), noty.Title.ToString(), noty.LastEdit.ToString(), noty.CreationDate.ToString() });
                listView1.Items.Add(lvi);
            }

        }
    }
}
