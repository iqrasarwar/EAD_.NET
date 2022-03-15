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
            EncryptUser(user);
            return ATMDataLayer.AddUser(user);
        }
        /// <summary>
        /// encrypt username and password map A to Z => Z to A and 0 to 9 => 0 to 9
        /// </summary>
        /// <param name="user">user to be encrypted</param>
        private static void EncryptUser(ATMUser user)
        {
            int Convert(char s)
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
                username += System.Convert.ToChar(Convert(s));
            foreach (char s in user.PinCode)
                pinCode += System.Convert.ToChar(Convert(s));
            user.UserName = username;
            user.PinCode = pinCode;
        }
        /// <summary>
        /// returns the login staus(disabled,failed etc) and the customer if login sucessfull.
        /// </summary>
        /// <param name="user_">user whos username pincode is to be validated</param>
        /// <returns>int=> reflects login status -> 1 if login successfull -> 2 if account disabled 
        /// -> 0 if pincode wrong -> -1 if username don't exist.</returns>
        public static Tuple<int,Customer> LoginVerification(ATMUser user_)
        {
            EncryptUser(user_);
            List<Customer> list = ATMDataLayer.ReadAccounts();
            foreach(Customer user in list)
            {
                if ((user.IsAdmin == 1 && user.AdminAccountStatus == 0) || (user.UserName == user_.UserName && user.Status == 0))
                    return new Tuple<int, Customer>(2, user); 
                else if (user.UserName == user_.UserName && user.PinCode != user_.PinCode)
                    return new Tuple<int, Customer>(0, user);
                else if (user.UserName == user_.UserName && user.PinCode == user_.PinCode)
                    return new Tuple<int,Customer>(1,user);
            }
            return new Tuple<int, Customer>(-1, null);
        }
        public static bool DisableUser(Customer c)
        {
            return ATMDataLayer.DisableAccount(c);
        }
    }
}
