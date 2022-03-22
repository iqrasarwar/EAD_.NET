using System;
using PresenatationLayer;
namespace SemesterVI
{
    class Program
    {

        static void Main(string[] args)
        {
            EmpView v = new EmpView();
            Console.WriteLine("\t\tStoring New Emplooyes to File");
            string choice = "";
            do
            {
               v.InputEmpData();
               Console.Write("\nWanna Enter next Object(1/0): ");
               choice = Console.ReadLine();
            } while (choice == "1");
            Console.WriteLine("\t\tReading Emplooyes from File");
            v.DisplayEmpData();
        }
    }
}
