//BSEF19M012 - IQRA SARWAR
using ATMPresentationLayer;

namespace ATM_Software
{
    class Program
    {
        static void Main()
        {
            //Presentation Layer Instance
            ATMView view = new();
            //Displaying main menu to Login OR register as Admin
            view.DisplayMenu();
        }
    }
}
