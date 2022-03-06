using ATMBussinessObjects;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace ATMDataAccessLayer
{
    public class ATMDataLayer
    {
        public bool StoreNewUser(ATMUser user)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"INSERT INTO [User] (UserName,PinCode) Values(@username, @pincode)";
            con.Open();
            SqlParameter paramName = new SqlParameter("@username", user.UserName);
            SqlParameter paramPinCode = new SqlParameter("@pincode", user.PinCode);
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.Add(paramName);
            com.Parameters.Add(paramPinCode);
            int status = com.ExecuteNonQuery();
            con.Close();
            if (status > 0)
                return true;
            return false;
        }
        public List<ATMUser> ReadUser()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"select * from [User])";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader data = com.ExecuteReader(); 
            List<ATMUser> list = new List<ATMUser>();
            if (data.HasRows)
            {
                while(data.Read())
                {
                    ATMUser user = new ATMUser();
                    user.UserName = (string)data[1];
                    user.PinCode = (string)data[2];
                    list.Add(user);
                }
            }
            con.Close();
            return list;
        }
    }
}
