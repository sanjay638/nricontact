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
    public partial class Jobdetails : Form
    {
        public Jobdetails()
        {
            InitializeComponent();
        }
        jobdetails j = new jobdetails();

        public void clear()
        {
            txtboxJobID.Text = "";
            cmbDesignation.Text = "";
            txtboxCompanyName.Text = "";
            txtboxSalary.Text = "";
            txtboxStreetName.Text = "";
            txtboxCity.Text = "";
            txtboxState.Text = "";
            txtboxPincode.Text = "";
            txtboxPassportID.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            j.PassportID = txtboxPassportID.Text;
            j.JobID =txtboxJobID.Text;
            j.Designation = cmbDesignation.Text;
            j.CompanyName = txtboxCompanyName.Text;
            j.Salary = txtboxSalary.Text;
            j.StreetName = txtboxStreetName.Text;
            j.City = txtboxCity.Text;
            j.State = txtboxState.Text;
            j.Pincode =txtboxPincode.Text;
            j.State = txtboxState.Text;

            bool success = j.Insert(j);
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

            DataTable dt = j.Select();
            dgvJobList.DataSource = dt;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            j.PassportID = txtboxPassportID.Text;
            j.JobID = txtboxJobID.Text;
            j.Designation = cmbDesignation.Text;
            j.CompanyName = txtboxCompanyName.Text;
            j.Salary = txtboxSalary.Text;
            j.StreetName = txtboxStreetName.Text;
            j.City = txtboxCity.Text;
            j.State = txtboxState.Text;
            j.Pincode = txtboxPincode.Text;
            j.State = txtboxState.Text;
            bool success = j.Update(j);
            if (success == true)
            {
                MessageBox.Show("Job details has been successfully Updated.");
                DataTable dt = j.Select();
                dgvJobList.DataSource = dt;
                clear();
                txtboxPassportID.Focus();


            }
            else
            {
                MessageBox.Show("Failed to Update details.Try Again. ");

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            j.JobID = txtboxJobID.Text;
            bool success = j.Delete(j);
            if (success == true)
            {
                MessageBox.Show("Job details successfully Deleted.");
                DataTable dt = j.Select();
                dgvJobList.DataSource = dt;
                clear();
                txtboxPassportID.Focus();
            }
            else
            {
                MessageBox.Show("Failed to Delete Details.Try Again. ");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtboxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT *FROM tbl_jobdetails WHERE PassportID LIKE '%" + keyword + "%' OR JobID LIKE '%" + keyword + "%' OR Designation LIKE '%" + keyword + "%' OR CompanyName LIKE '%" + keyword + "%' OR Salary LIKE '%" + keyword + "%' OR StreetName LIKE'%" + keyword + "%' OR City LIKE'%" + keyword + "%'", conn);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvJobList.DataSource = dt;
        }

        private void dgvJobList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtboxPassportID.Text = dgvJobList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxJobID.Text = dgvJobList.Rows[rowIndex].Cells[1].Value.ToString();
            cmbDesignation.Text = dgvJobList.Rows[rowIndex].Cells[2].Value.ToString();
            txtboxCompanyName.Text = dgvJobList.Rows[rowIndex].Cells[3].Value.ToString();
            txtboxSalary.Text = dgvJobList.Rows[rowIndex].Cells[4].Value.ToString();
            txtboxStreetName.Text = dgvJobList.Rows[rowIndex].Cells[5].Value.ToString();
            txtboxCity.Text = dgvJobList.Rows[rowIndex].Cells[6].Value.ToString();
            txtboxState.Text = dgvJobList.Rows[rowIndex].Cells[7].Value.ToString();
            txtboxPincode.Text = dgvJobList.Rows[rowIndex].Cells[8].Value.ToString();

        }

        private void Jobdetails_Load(object sender, EventArgs e)
        {  
         
            DataTable dt = j.Select();
            dgvJobList.DataSource = dt;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new NRIcontact().Show();
            this.Hide();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            new ProofDetails().Show();
            this.Hide();
        }
    }
}
