using NRIcontact.nricontactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NRIcontact
{
    public partial class NRIcontact : Form
    {
        public NRIcontact()
        {
            InitializeComponent();
        }
        contactClass c=new contactClass();
        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            c.PassportID = txtboxPassportID.Text;
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo =txtboxContactNo.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = cmbGender.Text;
            c.CountryName = cmbCountryName.Text;

            bool success = c.Insert(c);
            if (success == true)
            {
                MessageBox.Show("New Contact successfully Inserted");
                clear();
                txtboxPassportID.Focus();
            }
            else
            {
                MessageBox.Show("Failed to add New Contact.Try Again");
            }

            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void NRIcontact_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }
        public void clear()
        {
            txtboxPassportID.Text = "";
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtboxContactNo.Text = "";
            txtboxAddress.Text = "";
            cmbGender.Text = "";
            cmbCountryName.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            c.PassportID = txtboxPassportID.Text;
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNo.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = cmbGender.Text;
            c.CountryName = cmbCountryName.Text;
            bool success = c.Update(c);
            if (success == true)
            {
                MessageBox.Show("Contact has been successfully Updated.");
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                clear();
                txtboxPassportID.Focus();


            }
            else
            {
                MessageBox.Show("Failed to Update contact.Try Again. ");

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            c.PassportID = txtboxPassportID.Text;
            bool success = c.Delete(c);
            if (success == true)
            {
                MessageBox.Show("Contact successfully Deleted.");
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                clear();
                txtboxPassportID.Focus();
            }
            else
            {
                MessageBox.Show("Failed to Delete Contact.Try Again. ");
            }
        }

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtboxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT *FROM tbl_nricontact WHERE PassportID LIKE '%"+ keyword +"%' OR FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%' OR ContactNo LIKE '%" + keyword + "%' OR Gender LIKE'%" +keyword + "%' OR CountryName LIKE'%"+keyword +"%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtboxPassportID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtboxContactNo.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtboxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
            cmbCountryName.Text = dgvContactList.Rows[rowIndex].Cells[6].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

       

        private void btnNext_Click(object sender, EventArgs e)
        {
            new Jobdetails().Show();
            this.Hide();
        }

      
    }
}
