using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BookMyDoctor.Models;

namespace BookMyDoctor.Repository
{
    public class DoctorDBHandle
    {

        MySQL_DB_Connect db = new MySQL_DB_Connect();
        
        // **************** ADD NEW STUDENT *********************
        public bool AddDoctor(DoctorModel dmodel)
        {

            bool result = db.ExecuteQuery("INSERT INTO tbl_doctor (doc_first_name,doc_last_name,doc_qualification,doc_phone,doc_phone_alternate,doc_email,doc_status,doc_image) VALUES('" + dmodel.FirstName + "','" + dmodel.LastName + "','" + dmodel.Qualification + "','" + dmodel.Contact_Number + "','" + dmodel.Alternative_Number + "','" + dmodel.Email_id + "','1','" + dmodel.Image + "');");
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}