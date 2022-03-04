using System;
using BussinessObjects;
using BussinessLogicLayer;
using System.Collections.Generic;

namespace PresenatationLayer
{
    public class EmpView
    {

        EmpBussLaogic manager = new EmpBussLaogic();
        public void InputEmpData()
        {
            EmpBussinessObjs BussObj = new EmpBussinessObjs();
            Console.Write("Enter Name ");
            BussObj.name = Console.ReadLine();
            Console.Write("Enter Age ");
            BussObj.age = int.Parse(Console.ReadLine());
            Console.Write("Enter Salary ");
            BussObj.salary = decimal.Parse(Console.ReadLine());
            manager.saveEmployee(BussObj);
        }
        public void DisplayEmpData()
        {
            List<EmpBussinessObjs> BussObj = manager.retriveEmployee();
            foreach(EmpBussinessObjs Bussobj in BussObj)
            {
                Console.WriteLine("Name " + Bussobj.name);
                Console.WriteLine("Age " + Bussobj.age);
                Console.WriteLine("Salary " + Bussobj.salary);
                Console.WriteLine("Tax " + Bussobj.tax);
                Console.WriteLine();
            }
           
        }
    }
}
