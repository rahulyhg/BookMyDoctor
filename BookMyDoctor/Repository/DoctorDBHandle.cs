using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BookMyDoctor.Models;

namespace BookMyDoctor.Repository
{
    public class DoctorDBHandle
    {
        // **************** ADD NEW STUDENT *********************
        public bool AddDoctor(DoctorModel dmodel)
        {

            SqlConnection db = new SqlConnection();
            db.ExecuteQuery("INSERT INTO tbl_doctor (doc_first_name) VALUES ('Hi Ram')");
            return true;
            
        }
    }
}