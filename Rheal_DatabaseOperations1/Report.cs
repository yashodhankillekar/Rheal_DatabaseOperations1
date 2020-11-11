using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rheal_DatabaseOperations1
{
    class Report
    {
        SqlConnection conn;
        SqlCommand cmd;

        public Report()
        {
            conn = new SqlConnection("Data Source=.;Initial Catalog=Rheal_DBOps;Integrated Security=SSPI");
        }

        //get employees based on department name
        public List<Employee> getAllEmployeesByDepartment(string dept)
        {
            List<Employee> empList = new List<Employee>();
            try
            {
                conn.Open();

                cmd = new SqlCommand();
                SqlParameter deptName = new SqlParameter()
                {
                    ParameterName = "@DepartmentName",
                    SqlDbType = SqlDbType.VarChar,
                    Value = dept
                };
                cmd.Parameters.Add(deptName);
                cmd.Connection = conn;
                cmd.CommandText = "select EmployeeNo,EmployeeName,DepartmentName,EmployeeSalary from Employee inner join Department On EmployeeDeptId = DepartmentId where DepartmentName=@DepartmentName";
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    empList.Add(new Employee()
                    {
                        EmployeeNo = Convert.ToInt32(sqlDataReader["EmployeeNo"]),
                        EmployeeName = sqlDataReader["EmployeeName"].ToString(),
                        EmployeeDeptName = sqlDataReader["DepartmentName"].ToString(),
                        EmployeeSalary = Convert.ToInt32(sqlDataReader["EmployeeSalary"]),
                    });
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured: {ex.Message}");
            }

            return empList;
        }

        //get max salaried employees per department
        public List<Employee> getMaxSalaryPerDepartment()
        {
            List<Employee> empList = new List<Employee>();
            try
            {
                conn.Open();
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select DepartmentName, Max(EmployeeSalary) as MaxSal from Employee inner join Department On EmployeeDeptId = DepartmentId group by DepartmentName order by MaxSal DESC";
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    empList.Add(new Employee()
                    {
                        EmployeeDeptName = sqlDataReader["DepartmentName"].ToString(),
                        EmployeeSalary = Convert.ToInt32(sqlDataReader["MaxSal"] == null ? "0" : sqlDataReader["MaxSal"]),
                    });
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured: {ex.Message}");
            }

            return empList;
        }

        //get all employees with tax details
        public void getEmployeeTax()
        {
            EmpDataAccess ed = new EmpDataAccess();
            List<Employee> empList = ed.GetEmployees();
            Console.WriteLine("EmployeeNo\tEmployeeName\tDepartment\tSalary\t\tTax");
            foreach (var item in empList)   
            {
                string tax;
                if(item.EmployeeSalary > 1000000)
                {
                    tax = (item.EmployeeSalary * 0.30).ToString() + "(30%)";
                }
                else if(item.EmployeeSalary > 500001)
                {
                    tax = (item.EmployeeSalary * 0.20).ToString() + "(20%)";
                }
                else if(item.EmployeeSalary > 100000)
                {
                    tax = (item.EmployeeSalary * 0.15).ToString() + "(15%)";
                }
                else
                {
                    tax = "0 (0%)";
                }
                Console.WriteLine($"{item.EmployeeNo}\t\t{item.EmployeeName}\t\t{item.EmployeeDeptName}\t\t{item.EmployeeSalary}\t\t{tax}");
            }
        }

    }
}
