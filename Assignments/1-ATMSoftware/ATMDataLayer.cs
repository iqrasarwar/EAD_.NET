using ATMBussinessObjects;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ATMDataAccessLayer
{
    public class ATMDataLayer
    {
        private static readonly SqlConnection connection = new(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public static bool AddUser(ATMUser user)
        {
            string query = @"INSERT INTO [User] (UserName,PinCode,isAdmin) Values(@username, @pincode, @isAdmin)";
            connection.Open();
            SqlCommand com = new(query, connection);
            com.Parameters.Add(new("@username", user.UserName));
            com.Parameters.Add(new("@pincode", user.PinCode));
            com.Parameters.Add(new("@isAdmin", user.IsAdmin));
            int status = com.ExecuteNonQuery();
            connection.Close();
            if (status > 0)
                return true;
            return false;
        }
        public static List<ATMUser> ReadUsers()
        {
            connection.Open();
            SqlCommand com = new(@"select * from [User]", connection);
            SqlDataReader data = com.ExecuteReader();
            List<ATMUser> list = new();
            if (data.HasRows)
            {
                while(data.Read())
                {
                    ATMUser user = new();
                    user.Id = (int)data[0];
                    user.UserName = (string)data[1];
                    user.PinCode = (string)data[2];
                    user.IsAdmin = (bool)data[3] ? 1 : 0;
                    list.Add(user);
                }
            }
            connection.Close();
            return list;
        }
        public static bool AddCustomer(Customer c)
        {
            string query = @"INSERT INTO [Customer] (HolderName,Type,Balance,status,userID) Values(@holder, @type, @balance,@status,@userId)";
            connection.Open();
            SqlCommand com = new(query, connection);
            com.Parameters.Add(new("@holder", c.HolderName));
            com.Parameters.Add(new("@type", c.Type));
            com.Parameters.Add(new("@balance", c.Balance));
            com.Parameters.Add(new("@status", c.Status));
            com.Parameters.Add(new("@userId", GetId(c.UserName, c.PinCode)));
            int status = com.ExecuteNonQuery();
            connection.Close();
            if (status > 0)
                return true;
            return false;
        }
        public static bool DeleteCustomer(Customer c)
        {
            string query = @"DELETE from [Customer] where AccountNum = @accNum ";
            connection.Open();
            SqlCommand com = new(query, connection);
            com.Parameters.Add(new("@accNum", c.AccountNum));
            int status = com.ExecuteNonQuery();
            connection.Close();
            if (status > 0)
                return true;
            return false;
        }
        public static bool UpdateCustomer(Customer c)
        {
            string query = @"UPDATE [User] SET UserName=@un, PinCode=@pc where Id = @uid;UPDATE [Customer] SET HolderName=@holder,Type=@type,Balance=@balance,status=@status where AccountNum = @accNum ";
            connection.Open();
            SqlCommand com = new(query, connection);
            com.Parameters.Add(new("@un", c.UserName));
            com.Parameters.Add(new("@pc", c.PinCode));
            com.Parameters.Add(new("@uid", c.UserID));
            com.Parameters.Add(new("@holder", c.HolderName));
            com.Parameters.Add(new("@type", c.Type));
            com.Parameters.Add(new("@balance", c.Balance));
            com.Parameters.Add(new("@status", c.Status));
            com.Parameters.Add(new("@accNum", c.AccountNum));
            int status = com.ExecuteNonQuery();
            connection.Close();
            if (status > 0)
                return true;
            return false;
        }
        public static List<Customer> SearchCustomers(Query query,Customer c)
        {
            connection.Open();
            List<Customer> list = new();
            SqlCommand com = new(query.QueryStr, connection);
            com.Parameters.Add(new("@uid", c.UserID));
            com.Parameters.Add(new("@holder", c.HolderName));
            com.Parameters.Add(new("@type", c.Type));
            com.Parameters.Add(new("@balance", c.Balance));
            com.Parameters.Add(new("@status", c.Status == 1));
            com.Parameters.Add(new("@accNum", c.AccountNum));
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
                    user.UserID = (int)data[5];
                    list.Add(user);
                }
            }
            connection.Close();
            return list;
        }
        public static List<Customer> ReadCustomers()
        {
            connection.Open();
            SqlCommand com = new(@"select * from [Customer]", connection);
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
                    user.Status = (bool)data[4] ? 1 : 0;
                    user.UserID = (int)data[5];
                    list.Add(user);
                }
            }
            connection.Close();
            return list;
        }
        public static List<Customer> ReadAccounts()
        {
            string query = @"select * from [User] LEFT JOIN [Customer] ON [Customer].userID=[User].Id";
            connection.Open();
            SqlCommand com = new(query, connection);
            SqlDataReader data = com.ExecuteReader();
            List<Customer> list = new();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    Customer user = new();
                    user.Id = (int)data[0];
                    user.UserName = (string)data[1];
                    user.PinCode = (string)data[2];
                    user.IsAdmin = (bool)data[3] ? 1 : 0;
                    user.AdminAccountStatus = (bool)data[4] ? 1 : 0;
                    //if user is not admin he/she will have following fields
                    if (user.IsAdmin == 0)
                    {
                        user.AccountNum = (int)data[5];
                        user.HolderName = (string)data[6];
                        user.Type = (string)data[7];
                        user.Balance = (int)data[8];
                        user.Status = (bool)data[9] ? 1 : 0;
                    }
                    else
                        user.Status = -1; //if user is admin is/her account status must be set to
                                          //non-zero(active) by default
                    list.Add(user);
                }
            }
            connection.Close();
            return list;
        }
        public static List<Customer> BalanceBasedReport(Query q)
        {
            connection.Open();
            List<Customer> list = new();
            SqlCommand com = new(q.QueryStr, connection);
            com.Parameters.Add(new("@minBalance",q.IntA)); 
            com.Parameters.Add(new("@maxBalance",q.IntB)); 
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
                    user.UserID = (int)data[5];
                    list.Add(user);
                }
            }
            connection.Close();
            return list;
        }
        public static List<Transaction> DateBasedReport(Query q)
        {
            connection.Open();
            List<Transaction> list = new();
            SqlCommand com = new(q.QueryStr, connection);
            com.Parameters.Add(new("@minDate", q.IntA));
            com.Parameters.Add(new("@maxDate", q.IntB));
            SqlDataReader data = com.ExecuteReader();
            if(data.HasRows)
            {
                while (data.Read())
                {
                    Transaction t = new();
                    t.Id = (int)data[0];
                    t.AccountNum = (int)data[1];
                    t.TransactionType = (string)data[2];
                    t.RecipientAccount = (int)data[3];
                    t.Amount = (int)data[4];
                    t.Date = (string)data[5];
                    DateTime dt,start,end;
                    try
                    {
                        dt = DateTime.Parse(t.Date);
                        start = DateTime.Parse(q.StrA);
                        end = DateTime.Parse(q.StrB);
                    }
                    catch (FormatException)
                    {
                        continue;
                    }
                    if(dt>=start && dt<=end)
                    list.Add(t);
                }
            }
            connection.Close();
            return list;
        }
        public static bool TransferCash(Customer c,Customer receipt,Transaction deposit)
        {
            string query = @"INSERT INTO [TransactionHistory] (AccountNum, TransactionType, [To], Amount, Date) Values(@accNum,@Type,@toAcc,@amount,@date); update [Customer] set Balance = @reduceBalance where AccountNum = @accNum; update [Customer] set Balance = @increseBalance where AccountNum = @toAcc";
            connection.Open();
            SqlCommand com = new(query, connection);
            com.Parameters.Add(new("@reduceBalance", c.Balance));
            com.Parameters.Add(new("@increseBalance", receipt.Balance));
            com.Parameters.Add(new("@accNum", deposit.AccountNum));
            com.Parameters.Add(new("@Type", deposit.TransactionType));
            com.Parameters.Add(new("@toAcc", deposit.RecipientAccount));
            com.Parameters.Add(new("@amount", deposit.Amount));
            com.Parameters.Add(new("@date", deposit.Date));
            int status = com.ExecuteNonQuery();
            connection.Close();
            if (status > 0)
                return true;
            return false;
        }
        public static bool DepositAmount(Customer c,Transaction deposit)
        {
            string query = @"INSERT INTO [TransactionHistory] (AccountNum, TransactionType, [To], Amount, Date) Values(@accNum,@Type,@toAcc,@amount,@date); update [Customer] set Balance = @balance where AccountNum = @accNum";
            connection.Open();
            SqlCommand com = new(query, connection);
            com.Parameters.Add(new("@balance", c.Balance));
            com.Parameters.Add(new("@accNum", deposit.AccountNum));
            com.Parameters.Add(new("@Type", deposit.TransactionType));
            com.Parameters.Add(new("@toAcc", deposit.RecipientAccount));
            com.Parameters.Add(new("@amount", deposit.Amount));
            com.Parameters.Add(new("@date", deposit.Date));
            int status = com.ExecuteNonQuery();
            connection.Close();
            if (status > 0)
                return true;
            return false;
        }
        public static bool WithDrawAmount(Customer c,Transaction WithDraw)
        {
            string query = @"INSERT INTO [TransactionHistory] (AccountNum, TransactionType, [To], Amount, Date) Values(@accNum,@Type,@toAcc,@amount,@date); update [Customer] set Balance = @reduceBalance where AccountNum = @accNum;";
            connection.Open();
            SqlCommand com = new(query, connection);
            com.Parameters.Add(new("@reduceBalance", c.Balance));
            com.Parameters.Add(new("@accNum", WithDraw.AccountNum));
            com.Parameters.Add(new("@Type", WithDraw.TransactionType));
            com.Parameters.Add(new("@toAcc", WithDraw.RecipientAccount));
            com.Parameters.Add(new("@amount", WithDraw.Amount));
            com.Parameters.Add(new("@date", WithDraw.Date));
            int status = com.ExecuteNonQuery();
            connection.Close();
            if (status > 0)
                return true;
            return false;
        }
        public static bool DisableAccount(Customer c)
        {   
            string query = c.IsAdmin == 0 ? @"UPDATE [Customer] SET status=@status where AccountNum = @accNum" : @"UPDATE [User] SET AdminAccountStatus=@status where Id = @id";
            connection.Open();
            SqlCommand com = new(query, connection);
            com.Parameters.Add(new("@id", c.Id));
            com.Parameters.Add(new("@status", false));
            com.Parameters.Add(new("@accNum", c.AccountNum));
            int status = com.ExecuteNonQuery();
            connection.Close();
            if (status > 0)
                return true;
            return false;
        }
        /// <summary>
        /// Calculate the sum of the total amount withdrwan on current date.
        /// </summary>
        /// <param name="c">Customer whoes withDraw is to be calculated</param>
        /// <returns>Sum of withDraw</returns>
        public static int AmountWithDrawnToday(Customer c)
        {
            connection.Open();
            string query = $"Select * from [TransactionHistory] where TransactionType = @type AND AccountNum = @accNum;";
            SqlCommand com = new(query, connection);
            com.Parameters.Add(new("@type", "widthDraw"));
            com.Parameters.Add(new("@accNum", c.AccountNum));
            SqlDataReader data = com.ExecuteReader();
            int amountDrawn = 0;
            if(data.HasRows)
            {
                while (data.Read())
                {
                    DateTime dt;
                    try
                    {
                        dt = DateTime.Parse((string)data[5]);
                    }
                    catch
                    {
                        continue;
                    }
                    if (dt == DateTime.Now.Date)
                        amountDrawn += (int)data[4];
                }
            }
            connection.Close();
            return amountDrawn;
        }
        /// <summary>
        /// Return Id of the User From [User] table that is used to set forigen Key in [Customer] table
        /// </summary>
        /// <param name="name">UserName Column value</param>
        /// <param name="code">PinCode Column Value</param>
        /// <returns>Id of the user i.e Primary Key of [User]</returns>
        private static int GetId(string name, string code)
        {
            List<ATMUser> list = ReadUsers();
            foreach (ATMUser u in list)
                if (u.UserName == name && u.PinCode == code)
                    return u.Id;
            return -1;
        }
    }
}
