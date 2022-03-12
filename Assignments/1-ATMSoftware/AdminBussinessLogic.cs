using ATMBussinessObjects;
using System;
using ATMDataAccessLayer;
using System.Collections.Generic;

namespace ATMBussinessLogicLayer
{
    public class AdminBussinessLogic
    {
        public static void validateAccountDeletion(int accountNum)
        {
            Customer c = validateAccountNum(accountNum);
            if (c != null)
            {
                ATMDataLayer.deleteNewCustomerAccount(c);
            }
        }
        public static Customer validAccountUpdate(int accountNum)
        {
	       return validateAccountNum(accountNum);
        }
        public static void UpdateAccount(Customer old,Customer c)
        {
            Customer NewCustomer = old;
            if(c.HolderName!="")
                old.HolderName=c.HolderName;
            if(c.Type!="")
                old.Type=c.Type;
            if(c.Status > -1)
                old.Status=c.Status;
            if(c.UserName != "")
                old.UserName=c.UserName;
            if (c.PinCode != "")
                old.PinCode = c.PinCode;
            ATMDataLayer.updateCustomerAccount(old);
        }
        public static List<Customer> ExecuteSearchQuery(Customer c)
        {
            bool fetch = false;
            string query="select * from [customer] where";
            if(c.userID>0)
                {query+=" UserID = @uid AND";
                fetch =true;}
            if(c.AccountNum>0)
                {query+= " AccountNum = @accNum AND ";
                fetch =true;}
            if(c.HolderName!="")
                {query += " HolderName = @holder AND ";
                fetch =true;}
            if(c.Type!="")
                {query += " Type = @type AND ";
                fetch =true;}
            if(c.Balance > 0)
                {query += " Balance = @balance AND ";
                fetch =true;}
            if(c.Status > -1)
                {query += " Status = @status AND ";
                fetch =true;}
            query+= fetch? " 1 = 1":" 1 = 0";
                Console.WriteLine(query);
            return ATMDataLayer.searchCustomers(query,c);
        }
        public static List<Customer> ExecuteReportQuery(int min,int max)
        {
            return ATMDataLayer.BalanceBasedReport(min,max);
        }
        public static void validateAccountInfo(Customer c)
        {
            ATMUser user = new();
            bool validInfo = true;
            user.UserName = c.UserName;
            user.PinCode = c.PinCode;
            user.isAdmin = 0;
            Console.ForegroundColor = ConsoleColor.Red;
            if (c.Balance < 0)
                Console.WriteLine("Invalid Balance!");
            if (!(c.Type.ToLower() == "saving" || c.Type.ToLower() == "current"))
            {
                Console.WriteLine("Inavlid Account type!");
                validInfo = false;
            }
            if (c.HolderName.Length <= 0)
                Console.WriteLine("HolderName Must Exist!");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            if (c.Balance < 0 || c.HolderName.Length <= 0)
                validInfo = false;
            if (validInfo)
            {
                if (ATMBussinessLogic.NewUserValidation(user))
                {
                    c.UserName = user.UserName;
                    c.PinCode = user.PinCode;
                    if (ATMDataLayer.addNewCustomerAccount(c))
                        Console.WriteLine("New Account Created!");
                }
            }
            else
                ATMBussinessLogic.loginCredentialValidation(user);
            Console.ResetColor();
        }
        public static Customer validateAccountNum(int accountNum)
        {
            List<Customer> list = ATMDataLayer.ReadCustomers();
            foreach (Customer u in list)
            {
                if (u.AccountNum == accountNum)
                {
                    return u;
                }
            }
            return null;
        }
    }
}
