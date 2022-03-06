using ATMPresentationLayer;
using System;

namespace ATM_Software
{
    class Program
    { 
        static void Main(string[] args)
        {
            ATMView view = new ATMView();
            view.DisplayMenu();
        }
    }
}

