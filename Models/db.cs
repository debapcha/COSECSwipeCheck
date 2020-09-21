using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using COSECSwipeCheck.Models;
using System.Data;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace COSECSwipeCheck.Models
{
    public class db
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-ILIIGRJ\\SQLEXPRESS;Initial Catalog=COSEC;User id=sa;Password=SQL@15@dc;");
        
        public int LoginCheck(Ad_login ad)
        {
            SqlCommand sqlCommand = new SqlCommand("sp_CheckUser", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@USERNAME", ad.USERNAME);
            sqlCommand.Parameters.AddWithValue("@PASSWORD", ad.PASSWORD);
            SqlParameter obLogin = new SqlParameter();
            obLogin.ParameterName = "@IsValid";
            obLogin.SqlDbType = SqlDbType.Int;
            obLogin.Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add(obLogin);
            con.Open();
            sqlCommand.ExecuteNonQuery();
            
            int res = Convert.ToInt32(obLogin.Value);
            return res;

        }

    }
}
