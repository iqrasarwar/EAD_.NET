using Dependency_Injection.Models.Interfaces;

namespace Dependency_Injection.Models.Repostries
{
    public class IEmployeeRepositry : IEmployee
    {
        public List<Employee> GetAllEmployee()
        {
            Employee[] employees = new Employee[10];
            List<Employee> employeeList = new List<Employee>();
            int index = 0;
            foreach (Employee emp in employees)
            {
                emp.id = index++;
                emp.Name = "iqra";
                employeeList.Add(emp);
            }
            return employeeList;
        }
    }
}
