using System;
using ATMBussinessObjects;
using ATMBussinessLogicLayer;
using System.Collections.Generic;

namespace ATMPresentationLayer
{
    public class AdminView
    {
        public static Customer inputUserInfo()
        {
            Customer c = new();
            Console.Write("Enter Login Name ");
            c.UserName = Console.ReadLine();
            Console.Write("Enter Pin Code ");
            c.PinCode = Console.ReadLine();
            Console.Write("Enter Holder's Name ");
            c.HolderName = Console.ReadLine();
            Console.Write("Enter Account Type ");
            c.Type = Console.ReadLine();
            Console.Write("Enter Starting Balance ");
            string input = Console.ReadLine();
            int number;
            bool success = int.TryParse(input, out number);
            if (success)
                c.Balance = number;
            else
                c.Balance = -1;
                return c;
        }
        public static Customer InuptNewAccountInfo()
        {
            Customer c = inputUserInfo();
            c.Status = 1;
            return c;
        }
        public static Customer inputUpdateInfo()
        {
            Customer c = inputUserInfo();
            Console.Write("Enter Account Status(1/0 for active/disabled) ");
            string input =  Console.ReadLine();
            int id;
            if (int.TryParse(input, out id))
                c.Status = id;
            else
                c.Status = -1;
            return c;
        }
        public static Customer InuptSearchCriteria()
        {
            Customer c = new();
            Console.Write("Enter Account ID ");
            int id;
            string input = Console.ReadLine();
            bool success = int.TryParse(input, out id);
            if (success)
                c.AccountNum = id;
            else
                c.AccountNum = -1;
            Console.Write("Enter User ID ");
            input = Console.ReadLine();
            success = int.TryParse(input, out id);
            if (success)
                c.userID = id;
            else
                c.userID = -1;
            Console.Write("Enter Holder's Name ");
            c.HolderName = Console.ReadLine();
            Console.Write("Enter Account Type ");
            c.Type = Console.ReadLine();
            Console.Write("Enter Balance ");
            input = Console.ReadLine();
            int number;
            success = int.TryParse(input, out number);
            if (success)
                c.Balance = number;
            else
                c.Balance = -1;
            Console.Write("Enter Account Status(1/0 for active/disabled) ");
            input =  Console.ReadLine();
            if (int.TryParse(input, out id))
                c.Status = id;
            else
                c.Status = -1;
            return c;
        }
        public static void displayAdminMenu()
        {
            Console.WriteLine("Press 1 to Create New Account.");
            Console.WriteLine("Press 2 to Delete Existing Account.");
            Console.WriteLine("Press 3 to Update Account Information.");
            Console.WriteLine("Press 4 to Search for Account.");
            Console.WriteLine("Press 5 to View Reports");
            Console.WriteLine("Press 6 to Exit");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Customer c =  InuptNewAccountInfo();
                AdminBussinessLogic.validateAccountInfo(c);
            }
            else if (choice == "2")
            {
                Tuple<int, bool> t = inputAccountNum();
                if (t.Item2)
                    AdminBussinessLogic.validateAccountDeletion(t.Item1);
                else
                    Console.WriteLine("Invalid Account Number!");
            }
            else if (choice == "3")
            {
                Tuple<int, bool> t = inputAccountNum();
                if (t.Item2)
                {
                    Customer c = AdminBussinessLogic.validAccountUpdate(t.Item1);
                    if (c!=null)
                    {
                        printCustomer(c);
                        Console.WriteLine("Enter fileds you wnat to update");
                        Customer updated = inputUpdateInfo();
                        AdminBussinessLogic.UpdateAccount(c,updated);
                    }
                    else
                    Console.WriteLine("No Such Account Exist!");
                }
                else
                    Console.WriteLine("Invalid Account Number!");
            }
            else if(choice == "4")
            {
                Customer c = InuptSearchCriteria();
                List<Customer> list = AdminBussinessLogic.ExecuteSearchQuery(c);
                foreach(Customer cus in list)
                printCustomer(cus);
            }
            else if(choice == "5")
            {
                Console.WriteLine("Specify Report Type\n1- Accounts By Amount\n2- Accounts By Date");
                string reportType = Console.ReadLine();
                if(reportType == "1")
                {
                    Console.WriteLine("Enter Minimum Amount");
                    int minAmount = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter Maximum Amount");
                    int maxAmount = Convert.ToInt32(Console.ReadLine());
                    Query q = new Query();
                    q.IntA=minAmount;
                    q.IntB=maxAmount;
                    List<Customer> list =  AdminBussinessLogic.BalanceBasedReport(q);
                    foreach(Customer cus in list)
                    printCustomer(cus);

                }
                else if(reportType == "2")
                {
                    Console.WriteLine("Enter Starting Date: ");
                    string startDate = Console.ReadLine();
                    Console.WriteLine("Enter Ending Date: ");
                    string endDate = Console.ReadLine();
                    Query q = new();
                    q.StrA=startDate;
                    q.StrB=endDate;
                    List<Transaction> list =  AdminBussinessLogic.DateBasedReport(q);
                    foreach(Transaction t in list)
                    {
                        Console.WriteLine("Account # "+ t.AccountNum);
                        Console.WriteLine("Date "+ t.Date);
                        Console.WriteLine("Type "+ t.TransactionType);
                        Console.WriteLine("Balance "+ t.Amount);
                        Console.WriteLine("To Account Num "+ t.ToAccount);
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Choice");
                Console.ResetColor();
            }
        }
        private static Tuple<int,bool> inputAccountNum()
        {
                Console.Write("Enter Account Number ");
                string input = Console.ReadLine();
                int AccountNumber;
                bool success = int.TryParse(input, out AccountNumber);
                return new Tuple<int,bool>(AccountNumber, success);
        }
        public static void printCustomer(Customer c)
        {
            Console.WriteLine("Account # "+ c.AccountNum);
            Console.WriteLine("Holder's Name "+ c.HolderName);
            Console.WriteLine("Type "+ c.Type);
            Console.WriteLine("Balance "+ c.Balance);
            Console.WriteLine("Status "+ c.Status);
        }
    }
}
