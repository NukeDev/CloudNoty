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
        public string _Data { get; set; }
        public noteForm(string Data)
        {
            _Data = Data;
            InitializeComponent();
        }

        private void noteForm_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = _Data;
        }
    }
}
