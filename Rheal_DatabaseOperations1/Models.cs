using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rheal_DatabaseOperations1
{
    class Employee
    {
        public int EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeDeptId { get; set; }
        public int EmployeeSalary { get; set; }
        public string EmployeeDeptName { get; set; }
    }

    class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
