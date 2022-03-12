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
            if (choice == "3")
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
