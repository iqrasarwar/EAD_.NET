using ATMDataAccessLayer;
using ATMBussinessObjects;
using System;
using System.Collections.Generic;

namespace ATMBussinessLogicLayer
{
    public class CustomerBussinessLogic
    {
        public static bool updateBalance(Customer c,Transaction deposit)
        {
            deposit.AccountNum = c.AccountNum;
            deposit.Date = DateTime.Now.ToString("dd/MM/yyyy");
            deposit.TransactionType = "deposit";
            deposit.ToAccount = -1;
            return ATMDataLayer.depositTransaction(c, deposit);
        }
        public static ATMUser getUser(ATMUser user)
        {
            List<ATMUser> list = ATMDataLayer.ReadUser();
            foreach(ATMUser u in list)
            {
                if(u.UserName==user.UserName && u.PinCode == user.PinCode)
                {
                    return u;
                }
            }
            return null;
        }
        public static Customer getCustomer(ATMUser user)
        {
            List<Customer> list = ATMDataLayer.ReadCustomers();
            foreach(Customer c in list)
            {
                if(c.userID == user.id)
                {
                    return c;
                }
            }
            return null;
        }
    }
}
