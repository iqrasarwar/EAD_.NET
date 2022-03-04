using System.Collections.Generic;
using BussinessObjects;
using DataAccessLayer;

namespace BussinessLogicLayer
{
    public class EmpBussLaogic
    {
        EmpDAL dalObj = new EmpDAL();

        public void saveEmployee(EmpBussinessObjs obj)
        {
            obj.tax = calculateTax(obj.salary);
            dalObj.saveEmp(obj);
        }
        private decimal calculateTax(decimal salary)
        {
            decimal tax = salary > 1000 ? salary * 0.2M : salary * 0.1M;
            return tax;
        }
        public List<EmpBussinessObjs> retriveEmployee()
        {
            return dalObj.readEmp();
        }
    }
}
