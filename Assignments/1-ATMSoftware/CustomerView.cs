using System;
using ATMDataAccessLayer;
using ATMBussinessObjects;
using ATMBussinessLogicLayer;
namespace ATMPresentationLayer
{
    public class CustomerView
    {
        public static void displayCustomerMenu(ATMUser user)
        {
            Console.WriteLine("Press 1 to Withdraw Cash.");
            Console.WriteLine("Press 2 to Cash Transfer.");
            Console.WriteLine("Press 3 to Deposit Cash.");
            Console.WriteLine("Press 4 to Display Balance.");
            Console.WriteLine("Press 5 to Exit");

            string choice = Console.ReadLine();
            if(choice == "1")
            {
                Transaction withDraw = new();
                Console.WriteLine("Press 1 for Fast Cash\nPress 2 for Normal Cash\nPlease select a mode of withdrawal:");
                string mode = Console.ReadLine();
                user = CustomerBussinessLogic.getUser(user);
                Customer c = CustomerBussinessLogic.getCustomer(user);
                if(mode=="1")
                {
                    Console.WriteLine("1----500\n2----1000\n3----2000\n4----5000\n5----10000\n6----15000\n7----20000\nSelect one of the denominations of money:");
                    int amount = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Are you sure you want to withdraw "+amount+" (Y/N)?:");
                    String consent = Console.ReadLine();
                    if(consent.ToLower() == "y")
                    {
                        Transaction withdraw = new();
                        withDraw.Amount = amount;
                        CustomerBussinessLogic.WithdrawAmount(c,withDraw);
                    }
                    else
                        Console.WriteLine("Operation Cancelled!");
                }
                else if(mode=="2")
                {
                    Console.WriteLine("Enter the withdrawal amount:");
                    int amount = Convert.ToInt32(Console.ReadLine());
                    Transaction withdraw = new();
                    withDraw.Amount = amount;
                    CustomerBussinessLogic.WithdrawAmount(c,withDraw);
                }
                else
                Console.WriteLine("Invalid Choice!");
            }
            else if(choice == "2")
            {
                Transaction transfer = new();
                Console.WriteLine("Enter amount in multiples of 500: ");
                transfer.Amount = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the account number to which you want to transfer:: ");
                transfer.ToAccount = Convert.ToInt32(Console.ReadLine());
                user = CustomerBussinessLogic.getUser(user);
                Customer receipt = CustomerBussinessLogic.getCustomerByAccountNum(transfer.ToAccount);
                Customer c = CustomerBussinessLogic.getCustomer(user);
                if(CustomerBussinessLogic.transferAmount(c,receipt,transfer))
                {
                    Console.WriteLine("Transaction Recorded!");
                }
            }
            else if (choice == "3")
            {
                Console.WriteLine("Enter amount to deposite");
                int amount = Convert.ToInt32(Console.ReadLine());
                user = CustomerBussinessLogic.getUser(user);
                Customer c = CustomerBussinessLogic.getCustomer(user);
                Transaction deposit = new();
                deposit.Amount =amount;
                if(CustomerBussinessLogic.updateBalance(c,deposit))
                {
                    Console.WriteLine("Transaction Recorded!");
                }
            }
            else if(choice == "4")
            {
                user = CustomerBussinessLogic.getUser(user);
                Customer c = CustomerBussinessLogic.getCustomer(user);
                Console.WriteLine("user" +user.UserName+" "+user.id);
                Console.WriteLine("Account #" + c.AccountNum);
                Console.WriteLine("Balance "+ c.Balance);
                Console.WriteLine("Date :" + DateTime.Now.ToString("dd/MM/yyyy"));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Choice");
                Console.ResetColor();
            }
        }
    }
}
