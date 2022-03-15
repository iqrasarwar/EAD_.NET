using System;
using ATMDataAccessLayer;
using ATMBussinessObjects;
using ATMBussinessLogicLayer;
using System.Collections.Generic;
namespace ATMPresentationLayer
{
    public class ATMView
    {
        public static void DisplayMenu()
        {
            List<Tuple<string, int>> LoginTries = new();
            string choice="";
            while (choice != "3")
            {
                Console.WriteLine("Press 1 to Login");
                Console.WriteLine("Press 2 to Register New Admin");
                Console.Write("Press 3 to Exit\nEnter Your choice:");
                choice = Console.ReadLine();
                if (choice == "1")
                {
                    ATMUser user = InputLoginCredentials();
                    Tuple<int, Customer> t = ATMBussinessLogic.LoginVerification(user);
                    if (t.Item1 == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Login Successful!");
                        Console.ResetColor();
                        if (t.Item2.IsAdmin == 1)
                            AdminView.DisplayAdminMenu();
                        else
                            CustomerView.DisplayCustomerMenu(user);
                    }
                    else if (t.Item1 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        LoginTries.Add(new Tuple<string,int>(user.UserName,1));
                        int tries = CheckTriesCount(LoginTries, user.UserName);
                        Console.WriteLine("Wrong PinCode! You have made "+ tries + " Tries.");
                        if(tries >= 3)
                        {
                            if(ATMBussinessLogic.DisableUser(t.Item2))
                            Console.WriteLine("Your Account is Disabled.Contact Admin.");
                            Console.ResetColor();
                            break;
                        }
                        Console.ResetColor();
                    }
                    else if (t.Item1 == -1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("User Don't Exist! Try another UserName.");
                        Console.ResetColor();
                    }
                    else if (t.Item1 == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("This User is Disabled.");
                        Console.ResetColor();
                    }
                }
                else if (choice == "2")
                    RegisterNewAdmin();
                else if (choice == "3")
                    Environment.Exit(0);
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Choice");
                    Console.ResetColor();
                }
            }
        }
        //register a new adminand add in db if valid credentials are provided
        public static void RegisterNewAdmin(int status = 1)
        {
            ATMUser user = new();
            Console.Write("Enter User Name ");
            string name = Console.ReadLine();
            Console.Write("Enter Pin Code ");
            string pinCode = Console.ReadLine();
            user.UserName = name;
            user.PinCode = pinCode;
            user.IsAdmin = status;
            user.AdminAccountStatus = 1;
            if (UserInputValidation(user))
            {
                if (ATMBussinessLogic.AdminRegistration(user))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Congratulations! You registerd Successfully!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Registration in DataBase Failed!");
                    Console.ResetColor();
                }
            }
        }
        //validate username and pincode of user
        public static bool UserInputValidation(ATMUser user)
        {
            if (user.UserName.Length > 0 && user.PinCode.Length > 4)
                return true;
            Console.ForegroundColor = ConsoleColor.Red;
            if (user.PinCode.Length <= 4 && user.PinCode.Length != 0)
                Console.WriteLine("PinCode must be at least 5 character long!");
            else if (user.UserName == "" && user.PinCode == "")
                Console.WriteLine("UserName and PinCode Can't be empty!");
            else if (user.UserName == "")
                Console.WriteLine("UserName can't be empty!");
            else
                Console.WriteLine("PinCode Can't be empty!");
            Console.ResetColor();
            return false;
        }
        public static ATMUser InputLoginCredentials()
        {
            ATMUser user = new();
            Console.Write("Enter User Name ");
            string name = Console.ReadLine();
            Console.Write("Enter Pin Code ");
            string pinCode = Console.ReadLine();
            user.UserName = name;
            user.PinCode = pinCode;
            return user;
        }
        //return count of invalid login attempts
        private static int CheckTriesCount(List<Tuple<string, int>> LoginTries,string name)
        {
            int count = 0;
            foreach(Tuple<string,int> t in LoginTries)
            {
                if(t.Item1 == name)
                count++;
            }
            return count;
        }
    }
}
