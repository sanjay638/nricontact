using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRIcontact.nricontactClasses
{
    class jobdetails
    {
        //Getter Setter Properties
        //Acts as Data Carrier in Application
        public string PassportID { get; set; }
        public string JobID { get; set; }
        public string Designation { get; set; }
        public string CompanyName { get; set; }
        public string Salary { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }



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
                string sql = "SELECT * FROM tbl_jobdetails";
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
        public bool Insert(jobdetails j)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "INSERT INTO tbl_jobdetails(PassportID,JobID,Designation,CompanyName,Salary,StreetName,City,State,Pincode) VALUES(@PassportID,@JobID,@Designation,@CompanyName,@Salary,@StreetName,@City,@State,@Pincode)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PassportID", j.PassportID);
                cmd.Parameters.AddWithValue("@JobID", j.JobID);
                cmd.Parameters.AddWithValue("@Designation", j.Designation);
                cmd.Parameters.AddWithValue("@CompanyName", j.CompanyName);
                cmd.Parameters.AddWithValue("@Salary", j.Salary);
                cmd.Parameters.AddWithValue("@StreetName", j.StreetName);
                cmd.Parameters.AddWithValue("@City", j.City);
                cmd.Parameters.AddWithValue("@State", j.State);
                cmd.Parameters.AddWithValue("@Pincode", j.Pincode);

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
        public bool Update(jobdetails j)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "UPDATE tbl_jobdetails SET JobID=@JobID,Designation=@Designation,CompanyName=@CompanyName,Salary=@Salary,StreetName=@StreetName,City=@City,State=@State,Pincode=@Pincode WHERE PassportID=@PassportID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@JobID", j.JobID);
                cmd.Parameters.AddWithValue("@Designation", j.Designation);
                cmd.Parameters.AddWithValue("@CompanyName", j.CompanyName);
                cmd.Parameters.AddWithValue("@Salary", j.Salary);
                cmd.Parameters.AddWithValue("@StreetName", j.StreetName);
                cmd.Parameters.AddWithValue("@City", j.City);
                cmd.Parameters.AddWithValue("@State", j.State);
                cmd.Parameters.AddWithValue("@Pincode", j.Pincode);
                cmd.Parameters.AddWithValue("PassportID", j.PassportID);

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

        //delete data from database
        public bool Delete(jobdetails j)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "DELETE FROM tbl_jobdetails WHERE PassportID=@PassportID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PassportID", j.PassportID);
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
