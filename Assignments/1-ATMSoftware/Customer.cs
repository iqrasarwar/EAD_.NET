﻿
namespace ATMBussinessObjects
{
    public class Customer : ATMUser
    {
        public string HolderName { get; set; }
        public string Type { get; set; }
        public int Balance { get; set; }
        public int Status { get; set; }
        public int AccountNum { get; set; }
        public int userID { get; set; }
    }
}
