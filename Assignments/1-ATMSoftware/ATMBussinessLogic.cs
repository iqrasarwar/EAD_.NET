using System;
using System.Collections.Generic;
using ATMBussinessObjects;
using ATMDataAccessLayer;

namespace ATMBussinessLogicLayer
{
    public class ATMBussinessLogic
    {
        public static bool AdminRegistration(ATMUser user)
        {
            encryptyUser(user);
            return ATMDataLayer.StoreNewUser(user);
        }
        private static void encryptyUser(ATMUser user)
        {
            int convert(char s)
            {
                if (s >= 65 && s <= 90)
                    return 90 - ((s + 25) % 90);
                if (s >= 97 && s <= 122)
                    return 122 - ((s + 25) % 122);
                if (s >= 48 && s <= 57)
                    return 57 - ((s + 9) % 57);
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
        public static Tuple<int,Customer> loginCredentialValidation(ATMUser user_)
        {
            encryptyUser(user_);
            List<Customer> list = ATMDataLayer.ReadAccounts();
            foreach(Customer user in list)
            {
                if (user.UserName == user_.UserName && user.PinCode != user_.PinCode)
                    return new Tuple<int,Customer>(0,user);
                else if (user.UserName == user_.UserName && user.Status == 0)
                return new Tuple<int,Customer>(2,user);
                else if (user.UserName == user_.UserName && user.PinCode == user_.PinCode)
                    return new Tuple<int,Customer>(1,user);
            }
            return new Tuple<int, Customer>(-1, null);
        }
        public static bool disableUser(Customer c)
        {
            return ATMDataLayer.disableAccount(c);
        }
    }
}
