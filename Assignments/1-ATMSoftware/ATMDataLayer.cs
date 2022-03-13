using ATMBussinessObjects;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ATMDataAccessLayer
{
    public class ATMDataLayer
    {
        public static bool StoreNewUser(ATMUser user)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"INSERT INTO [User] (UserName,PinCode,isAdmin) Values(@username, @pincode, @isAdmin)";
            con.Open();
            SqlParameter paramName = new SqlParameter("@username", user.UserName);
            SqlParameter paramPinCode = new SqlParameter("@pincode", user.PinCode);
            SqlParameter paramIsAdmin = new SqlParameter("@isAdmin", user.isAdmin);
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.Add(paramName);
            com.Parameters.Add(paramPinCode);
            com.Parameters.Add(paramIsAdmin);
            int status = com.ExecuteNonQuery();
            con.Close();
            if (status > 0)
                return true;
            return false;
        }
        public static List<ATMUser> ReadUser()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"select * from [User]";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader data = com.ExecuteReader();
            List<ATMUser> list = new List<ATMUser>();
            if (data.HasRows)
            {
                while(data.Read())
                {
                    ATMUser user = new ATMUser();
                    user.id = (int)data[0];
                    user.UserName = (string)data[1];
                    user.PinCode = (string)data[2];
                    user.isAdmin = Convert.ToInt32((bool)data[3]);
                    list.Add(user);
                }
            }
            con.Close();
            return list;
        }
        public static bool addNewCustomerAccount(Customer c)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"INSERT INTO [Customer] (HolderName,Type,Balance,status,userID) Values(@holder, @type, @balance,@status,@userId)";
            con.Open();
            Console.WriteLine(c.UserName);
            Console.WriteLine(c.PinCode);
            SqlParameter paramName = new SqlParameter("@holder", c.HolderName);
            SqlParameter paramType = new SqlParameter("@type", c.Type);
            SqlParameter paramBalance = new SqlParameter("@balance", c.Balance);
            SqlParameter paramStatus = new SqlParameter("@status", c.Status);
            SqlParameter paramUserID = new SqlParameter("@userId", getId(c.UserName,c.PinCode));
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.Add(paramName);
            com.Parameters.Add(paramType);
            com.Parameters.Add(paramBalance);
            com.Parameters.Add(paramStatus);
            com.Parameters.Add(paramUserID);
            int status = com.ExecuteNonQuery();
            con.Close();
            if (status > 0)
                return true;
            return false;
        }
        public static List<Customer> ReadCustomers()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"select * from [Customer]";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader data = com.ExecuteReader();
            List<Customer> list = new();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    Customer user = new();
                    user.AccountNum = (int)data[0];
                    user.HolderName = (string)data[1];
                    user.Type = (string)data[2];
                    user.Balance = (int)data[3];
                    user.Status = (bool)data[4] ? 1:0;
                    user.userID = (int)data[5];
                    list.Add(user);
                }
            }
            con.Close();
            return list;
        }
        public static List<Customer> ReadAccounts()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"select * from [User] LEFT JOIN [Customer] ON [Customer].userID=[User].Id";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader data = com.ExecuteReader();
            List<Customer> list = new();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    Customer user = new();
                    user.id = (int)data[0];
                    user.UserName = (string)data[1];
                    user.PinCode = (string)data[2];
                    user.isAdmin = (bool) data[3] ? 1:0;
                    if(user.isAdmin == 0)
                    {
                        user.AccountNum = (int)data[4];
                        user.HolderName = (string)data[5];
                        user.Type = (string)data[6];
                        user.Balance = (int)data[7];
                        user.Status = (bool)data[8] ? 1:0;
                    }
                    else
                        user.Status=-1;
                    list.Add(user);
                }
            }
            con.Close();
            return list;
        }
        public static bool deleteNewCustomerAccount(Customer c)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"DELETE from [Customer] where AccountNum = @accNum ";
            con.Open();
            SqlParameter paramName = new SqlParameter("@accNum", c.AccountNum);
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.Add(paramName);
            int status = com.ExecuteNonQuery();
            con.Close();
            if (status > 0)
                return true;
            return false;
        }
        public static bool updateCustomerAccount(Customer c)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"UPDATE [User] SET UserName=@un, PinCode=@pc where Id = @uid;UPDATE [Customer] SET HolderName=@holder,Type=@type,Balance=@balance,status=@status where AccountNum = @accNum ";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.Add(new SqlParameter("@un", c.UserName));
            com.Parameters.Add(new SqlParameter("@pc", c.PinCode));
            com.Parameters.Add(new SqlParameter("@uid", c.userID));
            com.Parameters.Add(new SqlParameter("@holder", c.HolderName));
            com.Parameters.Add(new SqlParameter("@type", c.Type));
            com.Parameters.Add(new SqlParameter("@balance", c.Balance));
            com.Parameters.Add(new SqlParameter("@status", c.Status));
            com.Parameters.Add(new SqlParameter("@accNum", c.AccountNum));
            int status = com.ExecuteNonQuery();
            con.Close();
            if (status > 0)
                return true;
            return false;
        }
        public static List<Customer> searchCustomers(Query query,Customer c)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            List<Customer> list = new();
            SqlCommand com = new SqlCommand(query.QueryStr, con);
            com.Parameters.Add(new SqlParameter("@uid", c.userID));
            com.Parameters.Add(new SqlParameter("@holder", c.HolderName));
            com.Parameters.Add(new SqlParameter("@type", c.Type));
            com.Parameters.Add(new SqlParameter("@balance", c.Balance));
            com.Parameters.Add(new SqlParameter("@status", c.Status == 1 ? true : false));
            com.Parameters.Add(new SqlParameter("@accNum", c.AccountNum));
            SqlDataReader data = com.ExecuteReader();
            if(data.HasRows)
            {
                while (data.Read())
                {
                    Customer user = new();
                    user.AccountNum = (int)data[0];
                    user.HolderName = (string)data[1];
                    user.Type = (string)data[2];
                    user.Balance = (int)data[3];
                    user.Status = (bool)data[4] ? 1 : 0;
                    user.userID = (int)data[5];
                    list.Add(user);
                }
            }
            con.Close();
            return list;
        }
        public static List<Customer> GenerateBalanceReport(Query q)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            List<Customer> list = new();
            SqlCommand com = new SqlCommand(q.QueryStr, con);
            com.Parameters.Add(new SqlParameter("@p1",q.IntA));
            com.Parameters.Add(new SqlParameter("@p2",q.IntB));
            SqlDataReader data = com.ExecuteReader();
            if(data.HasRows)
            {
                while (data.Read())
                {
                    Customer user = new();
                    user.AccountNum = (int)data[0];
                    user.HolderName = (string)data[1];
                    user.Type = (string)data[2];
                    user.Balance = (int)data[3];
                    user.Status = (bool)data[4] ? 1 : 0;
                    user.userID = (int)data[5];
                    list.Add(user);
                }
            }
            con.Close();
            return list;
        }
        public static List<Transaction> GenerateTransactionReport(Query q)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con.Open();
            List<Transaction> list = new();
            SqlCommand com = new SqlCommand(q.QueryStr, con);
            com.Parameters.Add(new SqlParameter("@p1",q.StrA));
            com.Parameters.Add(new SqlParameter("@p2",q.StrB));
            SqlDataReader data = com.ExecuteReader();
            if(data.HasRows)
            {
                while (data.Read())
                {
                    Transaction t = new Transaction();
                    t.Id = (int)data[0];
                    t.AccountNum = (int)data[1];
                    t.TransactionType = (string)data[2];
                    t.ToAccount = (int)data[3];
                    t.Amount = (int)data[4];
                    t.Date = (string)data[5];
                    DateTime dt = DateTime.Parse(t.Date);
                    DateTime start = DateTime.Parse(q.StrA);
                    DateTime end = DateTime.Parse(q.StrB);
                    if(dt>=start && dt<=end)
                    list.Add(t);
                }
            }
            con.Close();
            return list;
        }
        public static bool transferTransaction(Customer c,Customer receipt,Transaction deposit)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"INSERT INTO [TransactionHistory] (AccountNum, TransactionType, [To], Amount, Date) Values(@accNum,@Type,@toAcc,@amount,@date); update [Customer] set Balance = @reduceBalance where AccountNum = @accNum; update [Customer] set Balance = @increseBalance where AccountNum = @toAcc";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.Add(new SqlParameter("@reduceBalance", c.Balance));
            com.Parameters.Add(new SqlParameter("@increseBalance", receipt.Balance));
            com.Parameters.Add(new SqlParameter("@accNum", deposit.AccountNum));
            com.Parameters.Add(new SqlParameter("@Type", deposit.TransactionType));
            com.Parameters.Add(new SqlParameter("@toAcc", deposit.ToAccount));
            com.Parameters.Add(new SqlParameter("@amount", deposit.Amount));
            com.Parameters.Add(new SqlParameter("@date", deposit.Date));
            int status = com.ExecuteNonQuery();
            con.Close();
            if (status > 0)
                return true;
            return false;
        }
        public static bool depositTransaction(Customer c,Transaction deposit)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"INSERT INTO [TransactionHistory] (AccountNum, TransactionType, [To], Amount, Date) Values(@accNum,@Type,@toAcc,@amount,@date); update [Customer] set Balance = @balance where AccountNum = @accNum";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.Add(new SqlParameter("@balance", c.Balance+=deposit.Amount));
            com.Parameters.Add(new SqlParameter("@accNum", deposit.AccountNum));
            com.Parameters.Add(new SqlParameter("@Type", deposit.TransactionType));
            com.Parameters.Add(new SqlParameter("@toAcc", deposit.ToAccount));
            com.Parameters.Add(new SqlParameter("@amount", deposit.Amount));
            com.Parameters.Add(new SqlParameter("@date", deposit.Date));
            int status = com.ExecuteNonQuery();
            con.Close();
            if (status > 0)
                return true;
            return false;
        }
        public static bool widthDrawTransaction(Customer c,Transaction widthDraw)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"INSERT INTO [TransactionHistory] (AccountNum, TransactionType, [To], Amount, Date) Values(@accNum,@Type,@toAcc,@amount,@date); update [Customer] set Balance = @reduceBalance where AccountNum = @accNum;";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.Add(new SqlParameter("@reduceBalance", c.Balance));
            com.Parameters.Add(new SqlParameter("@accNum", widthDraw.AccountNum));
            com.Parameters.Add(new SqlParameter("@Type", widthDraw.TransactionType));
            com.Parameters.Add(new SqlParameter("@toAcc", widthDraw.ToAccount));
            com.Parameters.Add(new SqlParameter("@amount", widthDraw.Amount));
            com.Parameters.Add(new SqlParameter("@date", widthDraw.Date));
            int status = com.ExecuteNonQuery();
            con.Close();
            if (status > 0)
                return true;
            return false;
        }
        public static bool disableAccount(Customer c)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"UPDATE [Customer] SET status=@status where AccountNum = @accNum";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.Add(new SqlParameter("@status", false));
            com.Parameters.Add(new SqlParameter("@accNum", c.AccountNum));
            int status = com.ExecuteNonQuery();
            con.Close();
            if (status > 0)
                return true;
            return false;
        }
        private static int getId(string name,string code)
        {
            List<ATMUser> list = ReadUser();
            foreach(ATMUser u in list)
            {
                if (u.UserName == name && u.PinCode == code)
                    return u.id;
            }
            return -1;
        }

    }
}
