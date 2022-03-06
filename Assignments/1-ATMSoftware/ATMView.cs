using System;
using ATMDataAccessLayer;
using ATMBussinessObjects;
using ATMBussinessLogicLayer;
namespace ATMPresentationLayer
{
    public class ATMView
    {
        public void InputNewUser()
        {
            ATMUser user = new ATMUser();
            ATMBussinessLogic bl = new ATMBussinessLogic();
            Console.Write("Enter User Name ");
            string name = Console.ReadLine();
            Console.Write("Enter Pin Code ");
            string pinCode = Console.ReadLine();
            user.UserName = name;
            user.PinCode = pinCode;
            bl.NewUserValidation(user);
        }
        public void InputLoginCredentials()
        {
            ATMUser user = new ATMUser();
            ATMBussinessLogic bl = new ATMBussinessLogic();
            Console.Write("Enter User Name ");
            string name = Console.ReadLine();
            Console.Write("Enter Pin Code ");
            string pinCode = Console.ReadLine();
            user.UserName = name;
            user.PinCode = pinCode;
            bl.loginCredentialValidation(user);
        }
        public void DisplayMenu()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t~~~~~~~~~~~~~~~~ Welcome To ATM Software! ~~~~~~~~~~~~~~~~~~~~~~~ ");
            Console.ResetColor();
            Console.WriteLine("Press 1 to Login");
            Console.WriteLine("Press 2 to Register");
            string choice = Console.ReadLine();
            if (choice == "1")
                InputLoginCredentials();
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
