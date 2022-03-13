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
        public static bool transferAmount(Customer c,Customer receipt,Transaction transfer)
        {
            transfer.AccountNum = c.AccountNum;
            transfer.Date = DateTime.Now.ToString("dd/MM/yyyy");
            transfer.TransactionType = "transfer";
            receipt.Balance+=transfer.Amount;
            c.Balance-=transfer.Amount;
            return ATMDataLayer.transferTransaction(c,receipt, transfer);
        }
        public static bool WithdrawAmount(Customer c,Transaction widthDraw)
        {
            widthDraw.AccountNum = c.AccountNum;
            widthDraw.ToAccount = -1;
            widthDraw.Date = DateTime.Now.ToString("dd/MM/yyyy");
            widthDraw.TransactionType = "widthDraw";
            if(c.Balance >= widthDraw.Amount)
            c.Balance-=widthDraw.Amount;
            else
            {
                Console.WriteLine("Not Enough Credit!");
                return false;
            }
            return ATMDataLayer.widthDrawTransaction(c,widthDraw);
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
        public static Customer getCustomerByAccountNum(int accNum)
        {
            List<Customer> list = ATMDataLayer.ReadCustomers();
            foreach(Customer c in list)
            {
                if(c.AccountNum == accNum)
                {
                    return c;
                }
            }
            return null;
        }
    }
}
