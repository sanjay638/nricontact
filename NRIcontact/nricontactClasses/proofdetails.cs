using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRIcontact.nricontactClasses
{
    class proofdetails
    {
        //Getter Setter Properties
        //Acts as Data Carrier in Application
        public string PassportID { get; set; }
        public string AadharNumber { get; set; }
        public string FileName { get; set; }
        public byte[] Image { get; set; }
      
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //Selecting Data from Database
        public DataTable Select()
        {
            //step 1:Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //step 2:writing sql query
                string sql = "SELECT * FROM tbl_picture";
                //creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //creating sql dataadapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        //inserting data into database
        public bool Insert(proofdetails p)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "INSERT INTO tbl_picture(PassportID,AadharNumber,FileName,Image) VALUES(@PassportID,@AadharNumber,@FileName,@Image)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PassportID", p.PassportID);
                cmd.Parameters.AddWithValue("@AadharNumber", p.AadharNumber);
                cmd.Parameters.AddWithValue("@FileName", p.FileName);
                cmd.Parameters.AddWithValue("@Image", p.Image);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //update data in database from our application
        
        //delete data from database
        public bool Delete(proofdetails p)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "DELETE FROM tbl_picture WHERE PassportID=@PassportID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PassportID", p.PassportID);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;

        }
    }
}
