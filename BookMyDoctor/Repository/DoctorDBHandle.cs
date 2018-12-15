using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BookMyDoctor.Models;

namespace BookMyDoctor.Repository
{
    public class DoctorDBHandle
    {


        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["BMD_dBConn"].ToString();
            con = new SqlConnection(constring);
        }

        // **************** ADD NEW STUDENT *********************
        public bool AddDoctor(DoctorModel dmodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("INSERT INTO Table_Doctor VALUES ('1','"+ dmodel.FirstName + "','" + dmodel.Contact_Number + "','" + dmodel.Address + "')", con);
           

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;

        }
    }
}