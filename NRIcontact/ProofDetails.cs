using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using NRIcontact.nricontactClasses;

namespace NRIcontact
{
    public partial class ProofDetails : Form
    {
        public ProofDetails()
        {
            InitializeComponent();
        }
        proofdetails p= new proofdetails();

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void btnBack_Click(object sender, EventArgs e)
        {
            new Jobdetails().Show();
            this.Hide();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            new ReportForm().Show();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            new NRIcontactLoginForm().Show();
            this.Hide();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image File(*.jpg;*.jpeg;*.bmp)|*.jpg;*.jpeg;*.bmp";
            if(openFileDialog.ShowDialog(this)==DialogResult.OK)
            {
                try
                {
                    if((myStream=openFileDialog.OpenFile())!=null)
                    {
                        string FileName = openFileDialog.FileName;
                        txtboxFileName.Text = FileName;
                        if(myStream.Length>512000)
                        {
                            MessageBox.Show("File Size Limit Exceeded");
                        }
                        else
                        {
                            pbImage.Load(FileName);
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(myconnstr);
            Image img = pbImage.Image;
            byte[] arr;
            ImageConverter converter = new ImageConverter();
            arr = (byte[])converter.ConvertTo(img, typeof(byte[]));

            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO tbl_picture(PassportID,AadharNumber,FileName,Image) VALUES(@PassportID,@AadharNumber,@FileName,@Image)", con);
            cmd.Parameters.AddWithValue("@PassportID", txtboxPassportID.Text);
            cmd.Parameters.AddWithValue("@AadharNumber", txtboxAadharNumber.Text);
            cmd.Parameters.AddWithValue("@FileName", txtboxFileName.Text);
            cmd.Parameters.AddWithValue("@Image", arr);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Proof Details Saved....");
            clear();
            DataTable dt = p.Select();
            dgvProofDetails.DataSource = dt;

        }

        public void clear()
        {
            txtboxPassportID.Text = "";            
            txtboxAadharNumber.Text = "";
            txtboxFileName.Text = "";
            pbImage.Image=null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

      

        private void dgvProofDetails_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtboxPassportID.Text = dgvProofDetails.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxAadharNumber.Text = dgvProofDetails.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxFileName.Text = dgvProofDetails.Rows[rowIndex].Cells[2].Value.ToString();
            byte[] imgdata=(byte[])dgvProofDetails.Rows[rowIndex].Cells[3].Value;
            MemoryStream ms = new MemoryStream(imgdata);
            pbImage.Image = Image.FromStream(ms);
            /*pbImage.Image = (Image)dgvProofDetails.Rows[rowIndex].Cells[3].Value;*/
        }

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtboxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT *FROM tbl_picture WHERE PassportID LIKE '%" + keyword + "%' OR AadharNumber LIKE '%" + keyword + "%'", conn);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvProofDetails.DataSource = dt;
        }

        private void ProofDetails_Load(object sender, EventArgs e)
        {
            DataTable dt = p.Select();
            dgvProofDetails.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            p.AadharNumber = txtboxAadharNumber.Text;
            bool success = p.Delete(p);
            if (success == true)
            {
                MessageBox.Show("Proof details successfully Deleted.");
                DataTable dt = p.Select();
                dgvProofDetails.DataSource = dt;
                clear();
                txtboxPassportID.Focus();
            }
            else
            {
                MessageBox.Show("Failed to Delete Details.Try Again. ");
            }
        }
    }
}
