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
    public partial class noteForm : Form
    {
        public Config.Noty noty { get; set; }
        public Config.Cfg.EncrAlg encrAlg { get; set; }
        public Config.Cfg StdConfig = new Config.Cfg();
        public bool _iscreatinganewnote { get; set; }

        public noteForm(Config.Noty _noty, bool iscreatinganewnote)
        {
            noty = _noty;
            _iscreatinganewnote = iscreatinganewnote;
            InitializeComponent();
            StdConfig.MYSQLCONNECTION = new MySql.Data.MySqlClient.MySqlConnection(Config.Cfg.MYSQLCONNECTIONSTRING);
        }

        private void noteForm_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = noty.Content;
            textBox1.Text = noty.Title;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string key = Config.Cfg.LOCALKEY + Convert.ToString(Config.Cfg.UID);
                richTextBox1.Text = Core.Encryption.FileEncryption.Encrypt(richTextBox1.Text, key);
            }
            catch(Exception ex)
            {
                MessageBox.Show("There was a problem while trying to Encrypt the data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
      
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if(_iscreatinganewnote)
            {
                noty.Content = richTextBox1.Text;
                noty.Title = textBox1.Text;
                noty.Autor = Config.Cfg.UID;
                noty.EncrAlg = "RijndaelManaged";
                Core.Query.QNotyCreate notyUp = new Query.QNotyCreate();

                await notyUp.SET(noty, StdConfig.MYSQLCONNECTION);
            }
            else
            {
                noty.Content = richTextBox1.Text;
                noty.Title = textBox1.Text;
                noty.LastEdit = DateTime.Now;
                noty.EncrAlg = "RijndaelManaged";
                Core.Query.QNotyUpdate notyUp = new Query.QNotyUpdate();

                await notyUp.POST(noty, StdConfig.MYSQLCONNECTION);
            }
           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string key = Config.Cfg.LOCALKEY + Convert.ToString(Config.Cfg.UID);
                richTextBox1.Text = Core.Encryption.FileEncryption.Decrypt(richTextBox1.Text, key);
            }
            catch(Exception ex)
            {
                MessageBox.Show("There was a problem while trying to decrypt the data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void noteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Core.Forms.landingForm lForm = new Core.Forms.landingForm();
            lForm.Show();
            this.Hide();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Core.Query.QNotyDelete del = new Query.QNotyDelete();
                bool deleted = await del.Delete(Convert.ToString(noty.ID), StdConfig.MYSQLCONNECTION);
                if (!deleted)
                {
                    MessageBox.Show("Noty Successfully deleted!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("There was a problem while trying to DELETE the noty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("There was a problem while trying to DELETE the noty! " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
