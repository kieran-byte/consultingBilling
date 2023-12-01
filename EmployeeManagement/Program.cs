using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EmployeeManagement.employee;
using EmployeeManagement.company;

namespace EmployeeManagement
{

    
    class Program
    {
        static string connectionString = "Data Source=employee.db;";

        private readonly IEmployeeDataAccess _employeeAccess;
        private readonly ICompanyManager _companyManager;

        //expand to hold other access inclu company and junction table
        public Program(IEmployeeDataAccess employeeAccess, ICompanyManager companyAccess)
        {
            this._employeeAccess = employeeAccess;
            this._companyManager = companyAccess;
        }

        
        static void Main(string[] args)
            {
                bool continueExecution = true;

            IEmployeeDataAccess employeeDataAccess = new EmployeeDataAccess(connectionString);
            ICompanyManager companyAccess = new CompanyManager(connectionString);

            Program program = new Program(employeeDataAccess, companyAccess);

            program._employeeAccess.DropTable();
            program._companyManager.DropCompanyTable();

            program._companyManager.InitializeDatabase();
            program._employeeAccess.InitializeDatabase();

            InitialDataset(program);


            while (continueExecution)
                {
                
                

                  Console.WriteLine("\nEmployee Management System");
                    Console.WriteLine("1. Add Employee");
                    Console.WriteLine("2. View Employees");
                    Console.WriteLine("3. Update Employee");
                    Console.WriteLine("4. Delete Employee");
                    Console.WriteLine("5. Exit");
                    Console.Write("Enter your choice: ");
                    int choice = int.Parse(Console.ReadLine());

                int id = 0;
                    switch (choice)
                    {
                        case 1:
                            program._employeeAccess.AddEmployee();
                            break;
                        case 2:
                            program._employeeAccess.ViewEmployees();
                            break;
                        case 3:
                        Console.WriteLine("Employee Id: ");
                        id = int.Parse(Console.ReadLine());
                        program._employeeAccess.UpdateEmployee(id);
                            break;
                        case 4:
                        Console.WriteLine("Employee Id: ");
                        id = int.Parse(Console.ReadLine());
                        program._employeeAccess.RemoveEmployee(id);
                            break;
                        case 5:
                            continueExecution = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }


        //places the intial data to work with in the employee table
        public static void InitialDataset(Program program)
        {
            program._employeeAccess.AddEmployee("John Doe", "IT Department");
            program._employeeAccess.AddEmployee("Jane Smith", "HR Department");
            program._employeeAccess.AddEmployee("Michael Johnson", "Finance Department");
            program._employeeAccess.AddEmployee("Emily Brown", "Marketing Department");
            program._employeeAccess.AddEmployee("Robert Williams", "Operations Department");
            program._employeeAccess.AddEmployee("Sophia Miller", "Sales Department");

            program._companyManager.AddCompany("Company A", "Technology");
            program._companyManager.AddCompany("Company B", "Finance");
            program._companyManager.AddCompany("Company C", "Healthcare");
            program._companyManager.AddCompany("Company D", "Manufacturing");

        }




        /*static void InitializeDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                //create employee table
                string createTableQuery = "CREATE TABLE IF NOT EXISTS Employees (EmployeeId INTEGER PRIMARY KEY, Name TEXT, Department TEXT)";
                SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();

                //Create company table
                createTableQuery = "CREATE TABLE IF NOT EXISTS Companies (CompanyId INTEGER PRIMARY KEY, Industry TEXT)";
                command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();


                //junction table showing which employee works on which project
                createTableQuery = @"CREATE TABLE IF NOT EXISTS EmployeeProjects
                        (EmployeeId INTEGER,ProjectId INTEGER,FOREIGN KEY(EmployeeId) REFERENCES Employees(EmployeeId),
                        FOREIGN KEY(ProjectId) REFERENCES Projects(ProjectId)
                            )";
                command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();

            }
            Console.WriteLine("Database created");
        }*/



        /*        static void DropTables()
                {
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        //Create company table
                        dropQuery = "DROP TABLE IF EXISTS Companies";
                        command = new SQLiteCommand(dropQuery, connection);
                        command.ExecuteNonQuery();

                        //junction table showing which employee works on which project
                        dropQuery = "DROP TABLE IF EXISTS EmployeeProjects";
                        command = new SQLiteCommand(dropQuery, connection);
                        command.ExecuteNonQuery();

                    }
                }*/



    }
}
