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
    }
}
