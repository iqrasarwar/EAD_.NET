using System;
using ATMDataAccessLayer;
using ATMBussinessObjects;
using ATMBussinessLogicLayer;
using System.Collections.Generic;
namespace ATMPresentationLayer
{
    public class ATMView
    {
        public static void InputNewAdmin(int status=1)
        {
            ATMUser user = new ATMUser();
            Console.Write("Enter User Name ");
            string name = Console.ReadLine();
            Console.Write("Enter Pin Code ");
            string pinCode = Console.ReadLine();
            user.UserName = name;
            user.PinCode = pinCode;
            user.isAdmin = status;
            if(UserInputValidation(user))
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
        private static bool UserInputValidation(ATMUser user)
        {
            if (user.UserName.Length > 0 && user.PinCode.Length > 4)
                return true;
            Console.ForegroundColor = ConsoleColor.Red;
            if (user.PinCode.Length <= 4 && user.PinCode.Length != 0)
                Console.WriteLine("PinCode must be at least 5 character long!");
            if (user.UserName == "" && user.PinCode == "")
                Console.WriteLine("UserName and PinCode Can't be empty!");
            else if (user.UserName == "")
                Console.WriteLine("UserName can't be empty!");
            else
                Console.WriteLine("PinCode Can't be empty!");
            Console.ResetColor();
            return false;
        }
        public ATMUser InputLoginCredentials()
        {
            ATMUser user = new ATMUser();
            Console.Write("Enter User Name ");
            string name = Console.ReadLine();
            Console.Write("Enter Pin Code ");
            string pinCode = Console.ReadLine();
            user.UserName = name;
            user.PinCode = pinCode;
            return user;
        }
        public void DisplayMenu()
        {
            List<Tuple<string, int>> LoginTries = new List<Tuple<string,int>>();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Welcome To ATM Software! ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ ");
            Console.ResetColor();
            string choice="";
            int tries =0;
            while(choice != "3")
            {
                Console.WriteLine("Press 1 to Login");
                Console.WriteLine("Press 2 to Register Admin");
                Console.WriteLine("Press 3 to Exit");
                choice = Console.ReadLine();
                if (choice == "1")
                {
                        ATMUser user = InputLoginCredentials();
                        Tuple<int, Customer> t = ATMBussinessLogic.loginCredentialValidation(user);
                        if (t.Item1 == 1)
                        loginSuccess(t.Item2.isAdmin,user);
                        else if (t.Item1 == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            LoginTries.Add(new Tuple<string,int>(user.UserName,1));
                            tries = CheckTriesCount(LoginTries,user.UserName);
                            Console.WriteLine("Wrong PinCode! You have made "+ tries + " Tries.");
                            if(tries >= 3)
                            {
                                if(ATMBussinessLogic.disableUser(t.Item2))
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
                    InputNewAdmin();
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
        private void loginSuccess(int isAdmin,ATMUser user)
        {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Login Successful!");
                        Console.ResetColor();
                        if (isAdmin == 1)
                            AdminView.displayAdminMenu();
                        else
                            CustomerView.displayCustomerMenu(user);
        }
        private int CheckTriesCount(List<Tuple<string, int>> LoginTries,string name)
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
