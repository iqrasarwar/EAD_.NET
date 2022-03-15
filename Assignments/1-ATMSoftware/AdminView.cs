using System;
using ATMBussinessObjects;
using ATMBussinessLogicLayer;
using System.Globalization;
using System.Collections.Generic;

namespace ATMPresentationLayer
{
    public class AdminView
    {
        public static void DisplayAdminMenu()
        {
            string choice = "";
            while(choice!="6")
            {
                Console.WriteLine("Press 1 to Create New Account.");
                Console.WriteLine("Press 2 to Delete Existing Account.");
                Console.WriteLine("Press 3 to Update Account Information.");
                Console.WriteLine("Press 4 to Search for Account.");
                Console.WriteLine("Press 5 to View Reports");
                Console.Write("Press 6 to Exit\nEnter Your choice:");
                choice = Console.ReadLine();
                if (choice == "1")
                {
                    Customer c =  InuptNewAccountInfo();
                    ATMUser user = new();
                    user.UserName = c.UserName;
                    user.PinCode = c.PinCode;
                    user.IsAdmin = 0;
                    ValidateAndRegisterUser(c,ATMView.UserInputValidation(user),user);
                }
                else if (choice == "2")
                {
                    Tuple<int, bool> t = InputAccountNum();
                    if (t.Item2)
                    {
                        if(AdminBussinessLogic.VerifyDeletion(t.Item1))
                            Console.WriteLine("Account Deleteed!");
                        else
                            Console.WriteLine("Account Not Found!");
                    }
                    else
                        Console.WriteLine("Invalid Account Number!");
                }
                else if (choice == "3")
                {
                    Tuple<int, bool> t = InputAccountNum();
                    if (t.Item2)
                    {
                        Customer c = AdminBussinessLogic.ValidateAccountNumber(t.Item1);
                        if (c!=null)
                        {
                            string Heading = string.Format("{0,-10}\t{1,-10}\t{2,-10}\t{3,-10}\t{4,-10}", "Account #", "Holder's Name", "Type", "Balance", "Status");
                            Console.WriteLine(Heading);
                            PrintCustomer(c);
                            Console.WriteLine("Enter fileds you want to update");
                            Customer updated = InuptNewAccountInfo();
                            if(AdminBussinessLogic.UpdateAccount(c,updated))
                            {
                                Console.WriteLine("Account Updation Successful!");
                            }
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
                    if(AdminBussinessLogic.ValidateSearchParameters(c) == 1)
                    {
                        List<Customer> list = AdminBussinessLogic.ExecuteSearchQuery(c);
                        if(list.Count <= 0 )
                        {
                            Console.WriteLine("No Matching Record Found!");
                        }
                        string Heading = string.Format("{0,-10}\t{1,-10}\t{2,-10}\t{3,-10}\t{4,-10}", "Account #", "Holder's Name", "Type", "Balance", "Status");
                        Console.WriteLine(Heading); 
                        foreach (Customer cus in list)
                            PrintCustomer(cus);
                    }
                    else if(AdminBussinessLogic.ValidateSearchParameters(c) == 0)
                        Console.WriteLine("Inavlid Account Status!");
                    else if(AdminBussinessLogic.ValidateSearchParameters(c) == -1)
                        Console.WriteLine("Inavlid Account type!");
                }
                else if(choice == "5")
                {
                    Console.WriteLine("Specify Report Type\n1- Accounts By Amount\n2- Accounts By Date");
                    string reportType = Console.ReadLine();
                    PrintReports(reportType);
                }
                else if (choice == "6")
                    Environment.Exit(0);
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Choice");
                    Console.ResetColor();
                }
            }
        }
        private static Tuple<int,bool> InputAccountNum()
        {
                Console.Write("Enter Account Number ");
                string input = Console.ReadLine();
                bool success = int.TryParse(input, out int AccountNumber);
                return new Tuple<int,bool>(AccountNumber, success);
        }
        public static void PrintCustomer(Customer c)
        {
            string str = string.Format($"{c.AccountNum,-10}\t{c.HolderName,-10}\t{c.Type,-10}\t{c.Balance,-10}\t{c.Status,-10}");
            Console.WriteLine(str);
        }
        private static void PrintReports(string reportType)
        {
                if(reportType == "1")
                {
                    Console.WriteLine("Enter Minimum Amount");
                    string input = Console.ReadLine();
                    bool success = int.TryParse(input, out int minAmount);
                    if (!success)
                    {
                        Console.WriteLine("Inavlid Minimum Amount!");
                        return;
                    }
                    Console.WriteLine("Enter Maximum Amount");
                    input = Console.ReadLine();
                    success = int.TryParse(input, out int maxAmount);
                    if (!success)
                    {
                        Console.WriteLine("Inavlid Maximum Amount!");
                        return;
                    }
                    Query q = new();
                    q.IntA=minAmount;
                    q.IntB=maxAmount;
                    List<Customer> list =  AdminBussinessLogic.BalanceBasedReport(q);
                    if(list.Count <= 0 )
                    {
                        Console.WriteLine("No Matching Record Found!");
                    }
                    string Heading = string.Format("{0,-10}\t{1,-10}\t{2,-10}\t{3,-10}\t{4,-10}", "Account #", "Holder's Name", "Type", "Balance", "Status");
                    Console.WriteLine(Heading);
                    foreach (Customer cus in list)
                        PrintCustomer(cus);

                }
                else if(reportType == "2")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Strictly follow dd/MM/yyyy format!");
                    Console.ResetColor();
                    Console.WriteLine("Enter Starting Date: ");
                    string inputStart = Console.ReadLine();
                    try
                    {
                        DateTime startDate = DateTime.ParseExact(inputStart, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Inavlid Date Format!");
                        return;
                    }
                    Console.WriteLine("Enter Ending Date: ");
                    string inputEnd = Console.ReadLine();
                    try
                    {
                        DateTime endDate = DateTime.ParseExact(inputEnd, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Inavlid Date Format!");
                        return;
                    }
                    Query q = new();
                    q.StrA=inputStart;
                    q.StrB=inputEnd;
                    List<Transaction> list =  AdminBussinessLogic.DateBasedReport(q);
                    if(list.Count <= 0 )
                    {
                        Console.WriteLine("No Matching Record Found!");
                    }
                    string Heading = string.Format("{0,-10}\t{1,-10}\t{2,-10}\t{3,-10}\t{4,-10}", "SENDER ACC #", "DATE", "TYPE", "BALANCE", "RECIPENT ACC #");
                    Console.WriteLine(Heading);
                    foreach (Transaction t in list)
                    {
                        string str = string.Format($"{t.AccountNum,-10}\t{t.Date,-10}\t{t.TransactionType,-10}\t{t.Amount,-10}\t{t.RecipientAccount,-10}");
                        Console.WriteLine(str);
                    }
                }
                else
                Console.WriteLine("Invalid Choice!");
        }
        //Input data for new account
        public static Customer InuptNewAccountInfo()
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
            bool success = int.TryParse(input, out int number);
            if (success)
                c.Balance = number;
            else
                c.Balance = -1;
            Console.Write("Enter Account Status(1/0 for active/disabled) ");
            input = Console.ReadLine();
            if (int.TryParse(input, out int id))
                c.Status = id;
            else
                c.Status = -1;
            c.AdminAccountStatus = 1;
            return c;
        }
        //Input data to search on basis of it
        public static Customer InuptSearchCriteria()
        {
            Customer c = new();
            Console.Write("Enter Account ID ");
            string input = Console.ReadLine();
            bool success = int.TryParse(input, out int id);
            if (success)
                c.AccountNum = id;
            else
                c.AccountNum = -1;
            Console.Write("Enter User ID ");
            input = Console.ReadLine();
            success = int.TryParse(input, out id);
            if (success)
                c.UserID = id;
            else
                c.UserID = -1;
            Console.Write("Enter Holder's Name ");
            c.HolderName = Console.ReadLine();
            Console.Write("Enter Account Type ");
            c.Type = Console.ReadLine();
            Console.Write("Enter Balance ");
            input = Console.ReadLine();
            success = int.TryParse(input, out int number);
            if (success)
                c.Balance = number;
            else
                c.Balance = -1;
            Console.Write("Enter Account Status(1/0 for active/disabled) ");
            input = Console.ReadLine();
            if (int.TryParse(input, out id))
                c.Status = id;
            else
                c.Status = Int32.MinValue;
            return c;
        }
        //Checking validatiy of account information
        public static void ValidateAndRegisterUser(Customer c,bool validUser,ATMUser user)
        {
            bool validInfo = true;
            Console.ForegroundColor = ConsoleColor.Red;
            if (c.Balance < 0)
                Console.WriteLine("Invalid Balance!");
            if (c.Status != 0 && c.Status != 1)
                {
                    Console.WriteLine("Invalid Status!");
                    validInfo=false;
                }
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
                if (validUser && ATMBussinessLogic.AdminRegistration(user)) //registering customer in user table
                {
                    c.UserName = user.UserName;
                    c.PinCode = user.PinCode;
                    if (AdminBussinessLogic.AddCustomer(c)) //reistering customer in [Customer] table
                        Console.WriteLine("New Account Created!");
                    else
                        Console.WriteLine("Account Creation Failed!");
                }
            }
            Console.ResetColor();
        }
    }
}
