using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NRIcontact
{
    public partial class NRIcontactLoginForm : Form
    {
        public NRIcontactLoginForm()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if(txtUserName.Text=="Sanjay" && txtpassword.Text=="sanjay638")
            {
                new NRIcontact().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("The User Name or Password you entered is incorrect,try again");
                txtUserName.Clear();
                txtpassword.Clear();
                txtUserName.Focus();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            txtUserName.Clear();
            txtpassword.Clear();
            txtUserName.Focus();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void NRIcontactLoginForm_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
