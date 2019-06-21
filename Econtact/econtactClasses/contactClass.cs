using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Econtact.econtactClasses
{
    class contactClass
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconnString = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnString);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tblContact";
                SqlCommand cmd = new SqlCommand(sql, conn);
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

        public bool Insert(contactClass c)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnString);
            try
            {
                string sql = "INSERT INTO tblContact(FirstName, LastName, ContactNo, Address, Gender) VALUES(@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName );
                cmd.Parameters.AddWithValue("@ContactNo",c.ContactNo );
                cmd.Parameters.AddWithValue("@Address",c.Address );
                cmd.Parameters.AddWithValue("@Gender", c.Gender );

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if(rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }


            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();

            }
            return isSuccess;
        }

        public bool Update(contactClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnString);
            try
            {
                string sql = "UPDATE tblContact SET FirstName =@FirstName, LastName=@LastName, ContactNo = @ContactNo, Address=@address, Gender= @Gender WHER ContactId = @ContactId";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactId", c.ContactId);

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
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();

            }
            return isSuccess;
        }

        public bool Delet(contactClass c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnString);
            try
            {
                string sql = "DELETE FROM tblContact WHERE ContactId=@ContactId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactId", c.ContactId);
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
            catch(Exception ex)
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
