using System;
using System.Collections.Generic;
using ATMBussinessObjects;
using ATMDataAccessLayer;

namespace ATMBussinessLogicLayer
{
    public class ATMBussinessLogic
    {
        public void NewUserValidation(ATMUser user)
        {
            if(UserInputValidation(user))
            {
                ATMDataLayer dl = new ATMDataLayer();
                encryptyUser(user);
                if(dl.StoreNewUser(user))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Congratulations! You registerd Successfully!");
                    Console.ResetColor();
                }
            }
        }
        private bool UserInputValidation(ATMUser user)
        {
            if (user.UserName.Length > 0 && user.PinCode.Length > 2)
                return true;
            Console.ForegroundColor = ConsoleColor.Red;
            if (user.PinCode.Length <= 2 && user.PinCode.Length != 0)
                Console.WriteLine("PinCode must be at least 3 character long!"); 
            if (user.UserName == "" && user.PinCode == "")
                Console.WriteLine("UserName and PinCode Can't be empty!");
            else if (user.UserName == "")
                Console.WriteLine("UserName can't be empty!");
            else
                Console.WriteLine("PinCode Can't be empty!");
            Console.ResetColor();
            return false;
        }
        private void encryptyUser(ATMUser user)
        {
            int convert(char s)
            {
                if (s >= 65 && s <= 90)
                    return 90 - ((s + 25) % 90);
                if (s >= 97 && s <= 122)
                    return 122 - ((s + 25) % 122);
                if (s >= 0 && s <= 9)
                    return 9 - ((s + 9) % 9);
                else
                    return s;
            }
            string username = "",pinCode = "";
            foreach (char s in user.UserName)
                username += Convert.ToChar(convert(s));
            foreach (char s in user.PinCode)
                pinCode += Convert.ToChar(convert(s));
            user.UserName = username;
            user.PinCode = pinCode;
        }
        public bool loginCredentialValidation(ATMUser user_)
        {
            ATMDataLayer dl = new ATMDataLayer();
            List<ATMUser> list = dl.ReadUser();
            foreach(ATMUser user in list)
            {
                if (user.UserName == user_.UserName && user.PinCode == user_.PinCode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Login Successful!");
                    Console.ResetColor();
                    return true;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid Credentials!");
            Console.ResetColor();
            return false;
        }
    }
}
