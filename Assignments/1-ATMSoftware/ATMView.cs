using System;
using ATMDataAccessLayer;
using ATMBussinessObjects;
using ATMBussinessLogicLayer;
namespace ATMPresentationLayer
{
    public class ATMView
    {
        public static void InputNewUser(int status=1)
        {
            ATMUser user = new ATMUser();
            Console.Write("Enter User Name ");
            string name = Console.ReadLine();
            Console.Write("Enter Pin Code ");
            string pinCode = Console.ReadLine();
            user.UserName = name;
            user.PinCode = pinCode;
            user.isAdmin = status;
            if (ATMBussinessLogic.NewUserValidation(user))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations! You registerd Successfully!");
                Console.ResetColor();
            }
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
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("\t\t~~~~~~~~~~~~~~~~ Welcome To ATM Software! ~~~~~~~~~~~~~~~~~~~~~~~ ");
            Console.ResetColor();
            Console.WriteLine("Press 1 to Login");
            Console.WriteLine("Press 2 to Register");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                ATMUser user = InputLoginCredentials();
                Tuple<bool, ATMUser> t = ATMBussinessLogic.loginCredentialValidation(user);
                if (t.Item1 == true)
                {
                    if (t.Item2.isAdmin == 1)
                        AdminView.displayAdminMenu();
                    else
                        CustomerView.displayCustomerMenu(user);
                }
            }
            else if (choice == "2")
                InputNewUser();
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Choice");
                Console.ResetColor();
            }

        }
    }
}
