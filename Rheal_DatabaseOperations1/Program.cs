using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rheal_DatabaseOperations1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program obj = new Program();
            obj.start();
        }

        //function to display main menu
        public void mainMenu()
        {
            Console.WriteLine("===============================================================");
            Console.WriteLine("Database Operations Program");
            Console.WriteLine("===============================================================");
            Console.WriteLine("1. Display All Data");
            Console.WriteLine("2. Insert New Data");
            Console.WriteLine("3. Update Data");
            Console.WriteLine("4. Delete Data");
            Console.WriteLine("===============================================================");
            Console.WriteLine("5. Get all Employees based on DeptName");
            Console.WriteLine("6. Get Employees Having Max Salary Per Department");
            Console.WriteLine("7. Tax Information");
            Console.WriteLine();
            Console.WriteLine("X: Close");
            Console.WriteLine("===============================================================");
        }

        //Function to show sub-menu
        public void subMenu()
        {
            Console.WriteLine("===============================================================");
            Console.WriteLine("Select Table");
            Console.WriteLine("===============================================================");
            Console.WriteLine("1. Employee Table");
            Console.WriteLine("2. Department Table");
        }

        //function to display footer message
        public void footerMessage()
        {
            Console.WriteLine();
            Console.WriteLine("press any key to go back...");
            Console.ReadKey();
            Console.Clear();
        }

        //function to display error message
        public void errorMessage()
        {
            Console.WriteLine("ERROR!");
            Console.WriteLine("Something Went Wrong!");
            Console.WriteLine();
            Console.WriteLine("Press any key to go back....");
            Console.ReadKey();
            Console.Clear();
        }

        //function containing switch
        public void start()
        {
            bool quit = false;

            while (!quit)
            {
                mainMenu();
                string ch = Console.ReadLine();
                Console.Clear();
                switch (ch)
                {
                    case "1":
                        Console.Clear();
                        subMenu();
                        switch (Console.ReadLine())
                        {
                            case "1":
                                EmpDataAccess empDataAccess = new EmpDataAccess();
                                Console.Clear();
                                empDataAccess.printResult(empDataAccess.GetEmployees());
                                footerMessage();
                                break;
                            case "2":
                                DeptDataAccess deptDataAccess = new DeptDataAccess();
                                Console.Clear();
                                deptDataAccess.printResult(deptDataAccess.GetDepartments());
                                footerMessage();
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Please Give Valid Input");
                                break;
                        }
                        break;
                    case "2":
                        Console.Clear();
                        subMenu();
                        switch (Console.ReadLine())
                        {
                            case "1":
                                EmpDataAccess empDataAccess = new EmpDataAccess();
                                Employee emp = new Employee();
                                Console.Clear();
                                Console.WriteLine("Please Enter Employee Name");
                                emp.EmployeeName = Console.ReadLine();
                                Console.WriteLine("Please Enter Employee Department Id");
                                emp.EmployeeDeptId = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Please Enter Employee Salary");
                                emp.EmployeeSalary = Convert.ToInt32(Console.ReadLine());
                                empDataAccess.insertEmployee(emp);
                                footerMessage();
                                break;
                            case "2":
                                DeptDataAccess obj = new DeptDataAccess();
                                Console.Clear();
                                Console.WriteLine("Please Enter New Department Name");
                                Department dept = new Department() 
                                { 
                                    DepartmentName = Console.ReadLine() 
                                };
                                obj.InsertDepartment(dept);
                                footerMessage();
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Please Give Valid Input");
                                break;
                        }
                        break;
                    case "3":
                        Console.Clear();
                        subMenu();
                        switch (Console.ReadLine())
                        {
                            case "1":
                                EmpDataAccess empDataAccess = new EmpDataAccess();
                                Employee emp = new Employee();
                                Console.Clear();
                                Console.WriteLine("Enter Employee No");
                                emp.EmployeeNo = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter New Employee Name");
                                emp.EmployeeName = Console.ReadLine();
                                Console.WriteLine("Enter New Employee Department Id");
                                emp.EmployeeDeptId = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter New Employee Salary");
                                emp.EmployeeSalary = Convert.ToInt32(Console.ReadLine());
                                empDataAccess.updateEmployee(emp);
                                footerMessage();
                                break;
                            case "2":
                                DeptDataAccess obj = new DeptDataAccess();
                                Console.Clear();
                                Console.WriteLine("Enter Department Id");
                                int id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter New Department Name");
                                string name = Console.ReadLine();
                                Department dept = new Department()
                                {
                                    DepartmentId = id,
                                    DepartmentName = name,  
                                };
                                obj.UpdateDepartment(dept);
                                footerMessage();
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Please Give Valid Input");
                                break;
                        }
                        break;
                    case "4":
                        Console.Clear();
                        subMenu();
                        switch (Console.ReadLine())
                        {
                            case "1":
                                EmpDataAccess empDataAccess = new EmpDataAccess();
                                Console.Clear();
                                Console.WriteLine("Please Enter Employee No to Delete");
                                Employee emp = new Employee() 
                                { 
                                    EmployeeNo = Convert.ToInt32(Console.ReadLine())
                                };
                                empDataAccess.deleteEmployee(emp);
                                footerMessage();
                                break;
                            case "2":
                                DeptDataAccess obj = new DeptDataAccess();
                                Console.Clear();
                                Console.WriteLine("Please enter DepartmentID to Delete");
                                Department dept = new Department()
                                {
                                    DepartmentId = Convert.ToInt32(Console.ReadLine()),
                                };
                                obj.DeleteDepartment(dept);
                                footerMessage();
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Please Give Valid Input");
                                break;
                        }
                        break;
                    case "5":
                        Console.Clear();
                        Report rp = new Report();
                        Console.WriteLine("Get All Employees Based on Department Name");
                        Console.WriteLine("Enter Department Name");
                        List<Employee> empList = rp.getAllEmployeesByDepartment(Console.ReadLine());
                        Console.WriteLine("EmployeeNO\tEmployeeName\tDepartment\tSalary");
                        foreach (var item in empList)
                        {
                            Console.WriteLine($"{item.EmployeeNo}\t\t{item.EmployeeName}\t\t{item.EmployeeDeptName}\t\t{item.EmployeeSalary}");
                        }
                        footerMessage();
                        break;
                    case "6":
                        Console.Clear();
                        Report rp2 = new Report();
                        List<Employee> empList2 = new List<Employee>();
                        empList = rp2.getMaxSalaryPerDepartment();
                        Console.WriteLine("Department\tMaxSalary");
                        foreach (var item in empList)
                        {
                            Console.WriteLine($"{item.EmployeeDeptName}\t\t{item.EmployeeSalary}");
                        }
                        footerMessage();
                        break;
                    case "7":
                        Console.Clear();
                        Report rp3 = new Report();
                        rp3.getEmployeeTax();
                        footerMessage();
                        break;
                    case "x":
                    case "X":
                        quit = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Please Give Valid Input");
                        footerMessage();
                        break;
                }
            }
        }




    }
}
