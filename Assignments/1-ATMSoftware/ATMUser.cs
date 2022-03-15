﻿//BSEF19M012 - IQRA SARWAR
/// <summary>
/// "ATMUser" is the Image of "User" table from database.
/// </summary>
namespace ATMBussinessObjects
{
    public class ATMUser
    {
        public string UserName { get; set; }
        public string PinCode { get; set; }
        public int IsAdmin { get; set; }
        public int Id { get; set; }
        public int AdminAccountStatus { get; set; }
    }
}
