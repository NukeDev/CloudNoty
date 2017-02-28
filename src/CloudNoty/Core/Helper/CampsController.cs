using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudNoty.Core.Helper
{
    public class CampsController
    {
        public bool Registration(TextBox Username,TextBox Password, TextBox Email)
        {
            if (Username.Text == string.Empty || Password.Text == string.Empty || Email.Text == string.Empty)
            {
                MessageBox.Show(@"Please compile all texts!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            if (Username.Text.Length < 5 || Username.Text.Length > 32)
            {
                MessageBox.Show(@"Username must be of 5 or more letters! (max 32)", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return true;
            }

            if (Username.Text.Contains(".") || Username.Text.Contains("_") || Username.Text.Contains("-") ||
                Username.Text.Contains(";") || Username.Text.Contains(",") || Username.Text.Contains(":"))
            {
                MessageBox.Show(@"Username must not contains these chars (. _ - ; : ,)", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return true;
            }

            if (Username.Text.Contains(" ") || Email.Text.Contains(" "))
            {
                MessageBox.Show(@"Please remove all the blank spaces from the texts!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return true;
            }

            if (!Email.Text.Contains("@"))
            {
                MessageBox.Show(@"Please insert a valid E-Mail address!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return true;
            }


            return false;

        }
        public bool Login(TextBox Username, TextBox Password)
        {
            if (Username.Text == string.Empty || Password.Text == string.Empty)
            {
                MessageBox.Show(@"Please compile all texts!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            if (Username.Text.Length < 5 || Username.Text.Length > 32)
            {
                MessageBox.Show(@"Username must be of 5 or more letters! (max 32)", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return true;
            }

            if (Username.Text.Contains(".") || Username.Text.Contains("_") || Username.Text.Contains("-") ||
                Username.Text.Contains(";") || Username.Text.Contains(",") || Username.Text.Contains(":"))
            {
                MessageBox.Show(@"Username must not contains these chars (. _ - ; : ,)", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return true;
            }

            if (Username.Text.Contains(" "))
            {
                MessageBox.Show(@"Please remove all the blank spaces from the texts!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return true;
            }


            return false;

        }


    }
}
