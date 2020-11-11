using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Rheal_DatabaseOperations1
{
    class DeptDataAccess
    {
        SqlConnection conn;
        SqlCommand cmd;

        public DeptDataAccess()
        {
            conn = new SqlConnection("Data Source=.;Initial Catalog=Rheal_DBOps;Integrated Security=SSPI");
        }

        //read all data from database
        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            conn.Open();
            cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"Select * from Department";
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                departments.Add(
                    new Department()
                    {
                        DepartmentId = Convert.ToInt32(dataReader["DepartmentId"]),
                        DepartmentName = dataReader["DepartmentName"].ToString()
                    });
            }
            dataReader.Close();
            conn.Close();

            return departments;
        }

        //insert new values in database
        public void InsertDepartment(Department dept)
        {
            conn.Open();
            try
            {
                SqlParameter deptName = new SqlParameter()
                {
                    ParameterName = "@DepartmentName",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.VarChar,
                    Value = dept.DepartmentName
                };
                cmd = new SqlCommand();
                cmd.Parameters.Add(deptName);
                cmd.Connection = conn;
                cmd.CommandText = "Insert into Department(DepartmentName) values (@DepartmentName)";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured {ex.Message}");
            }
            conn.Close();
        }

        //update values in database
        public void UpdateDepartment(Department dept)
        {
            conn.Open();
            try
            {
                SqlParameter deptId = new SqlParameter()
                {
                    ParameterName = "@DepartmentId",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Int,
                    Value = dept.DepartmentId,
                };
                SqlParameter deptName = new SqlParameter()
                {
                    ParameterName = "@DepartmentName",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.VarChar,
                    Value = dept.DepartmentName,
                };

                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Add(deptId);
                cmd.Parameters.Add(deptName);
                cmd.CommandText = "Update Department set DepartmentName=@DepartmentName where DepartmentId=@DepartmentId";
                int result = cmd.ExecuteNonQuery();
                Console.WriteLine($"{result} Row(s) updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured {ex.Message}"); ;
            }

            conn.Close();
        }

        //delete values in database
        public void DeleteDepartment(Department dept)
        {
            conn.Open();
            try
            {
                cmd = new SqlCommand();
                SqlParameter deptId = new SqlParameter()
                {
                    ParameterName = "@DepartmentId",
                    SqlDbType = SqlDbType.Int,
                    Value = dept.DepartmentId,
                };
                cmd.Parameters.Add(deptId);
                cmd.Connection = conn;
                cmd.CommandText = "Delete from Department where DepartmentId=@DepartmentId";
                int result = cmd.ExecuteNonQuery();
                Console.WriteLine($"{result} Row(s) affected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured {ex.Message}"); 
            }
            conn.Close();
        }

        //function to print resulted rows
        public void printResult(List<Department> departments)
        {
            Console.WriteLine("DepartmentId\tDepartmentName");
            foreach (var department in departments)
            {
                Console.WriteLine($"{department.DepartmentId}\t\t{department.DepartmentName}");
            }

        }
    }
}
