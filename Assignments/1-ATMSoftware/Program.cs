using ATMPresentationLayer;
using System;

namespace ATM_Software
{
    class Program
    { 
        static void Main(string[] args)
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~ Welcome To ATM! ~~~~~~~~~~~~~~~~~~~~~~~ ");
            ATMView view = new ATMView();
            view.InputLoginCredentials();
        }
    }
}
