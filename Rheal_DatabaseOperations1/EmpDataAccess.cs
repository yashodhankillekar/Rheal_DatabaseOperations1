using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rheal_DatabaseOperations1
{
    class EmpDataAccess
    {
        SqlConnection conn;
        SqlCommand cmd;

        public EmpDataAccess()
        {
            conn = new SqlConnection("Data Source=.;Initial Catalog=Rheal_DBOps;Integrated Security=SSPI");
        }

        //read all data from database
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                conn.Open();
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "getEmployees";
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    employees.Add(new Employee()
                    {
                        EmployeeNo = Convert.ToInt32(sqlDataReader["EmployeeNo"]),
                        EmployeeName = sqlDataReader["EmployeeName"].ToString(),
                        EmployeeDeptId = Convert.ToInt32(sqlDataReader["EmployeeDeptId"]),
                        EmployeeSalary = Convert.ToInt32(sqlDataReader["EmployeeSalary"]),
                        EmployeeDeptName = sqlDataReader["DepartmentName"].ToString(),
                    });
                }
                sqlDataReader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured: {ex.Message}");
            }

            return employees;
        }

        //insert new values in database
        public void insertEmployee(Employee emp)
        {
            conn.Open();
            try
            {
                cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlParameter empName = new SqlParameter()
                {
                    ParameterName = "@EmployeeName",
                    SqlDbType = SqlDbType.VarChar,
                    Value = emp.EmployeeName
                };
                SqlParameter empDept = new SqlParameter()
                {
                    ParameterName = "@EmployeeDeptId",
                    SqlDbType = SqlDbType.Int,
                    Value = emp.EmployeeDeptId
                };
                SqlParameter empSal = new SqlParameter()
                {
                    ParameterName = "@EmployeeSalary",
                    SqlDbType = SqlDbType.Int,
                    Value = emp.EmployeeSalary
                };
                cmd.Parameters.Add(empName);
                cmd.Parameters.Add(empDept);
                cmd.Parameters.Add(empSal);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insertEmployee";
                int result = cmd.ExecuteNonQuery();
                Console.WriteLine($"{result} Row(s) Inserted");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured: {ex.Message}");
            }

            conn.Close();
        }

        //update values in database
        public void updateEmployee(Employee emp)
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand();
                cmd.Connection = conn;

                SqlParameter pEmpNo = new SqlParameter()
                {
                    ParameterName = "@EmployeeNo",
                    SqlDbType = SqlDbType.Int,
                    Value = emp.EmployeeNo
                };
                SqlParameter pEmpName = new SqlParameter()
                {
                    ParameterName = "@EmployeeName",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 20,
                    Value = emp.EmployeeName
                };
                SqlParameter pEmpDept = new SqlParameter()
                {
                    ParameterName = "@EmployeeDeptId",
                    SqlDbType = SqlDbType.Int,
                    Value = emp.EmployeeDeptId
                };
                SqlParameter pEmpSal = new SqlParameter()
                {
                    ParameterName = "@EmployeeSalary",
                    SqlDbType = SqlDbType.Int,
                    Value = emp.EmployeeSalary
                };
                cmd.Parameters.Add(pEmpNo);
                cmd.Parameters.Add(pEmpName);
                cmd.Parameters.Add(pEmpDept);
                cmd.Parameters.Add(pEmpSal);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "updateEmployee";
                int result = cmd.ExecuteNonQuery();
                Console.WriteLine($"{result} Row(s) Updated!");
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }


        }

        //delete values in database
        public void deleteEmployee(Employee emp)
        {
            conn.Open();
            try
            {
                cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlParameter empNo = new SqlParameter()
                {
                    ParameterName = "@EmployeeNo",
                    SqlDbType = SqlDbType.Int,
                    Value = emp.EmployeeNo
                };
                cmd.Parameters.Add(empNo);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "deleteEmployee";
                int result = cmd.ExecuteNonQuery();
                Console.WriteLine($"{result} Row(s) Deleted.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }
            conn.Close();
        }

        //function to print resulted rows
        public void printResult(List<Employee> employees)
        {
            Console.WriteLine("ID\tEmployeeName\tEmployeeDeptId\tDepartmentName\tEmployeeSalary");
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.EmployeeNo}\t{employee.EmployeeName}\t\t{employee.EmployeeDeptId}\t\t{employee.EmployeeDeptName}\t\t{employee.EmployeeSalary}");
            }

        }
    }
}
