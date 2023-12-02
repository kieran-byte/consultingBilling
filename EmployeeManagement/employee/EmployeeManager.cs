using System;
using System.Data.SQLite;

namespace EmployeeManagement.employee
{
    public class EmployeeDataAccess : IEmployeeDataAccess
    {
        private string connectionString;

        public EmployeeDataAccess(string dbPath)
        {
            connectionString = dbPath;
        }

        public void InitializeDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();


                //create employee table
                string createTableQuery = "CREATE TABLE IF NOT EXISTS Employees (EmployeeId INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Department TEXT, Hourly INT, CompanyId INT)";
                SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();


            }
            Console.WriteLine("Database created");
        }

        public void InsertHelper(string name, string department, int hourly)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Employees (Name, Department, Hourly) VALUES (@Name, @Department, @Hourly)";
                SQLiteCommand command = new SQLiteCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Department", department);
                command.Parameters.AddWithValue("@Hourly", hourly);
                command.ExecuteNonQuery();
            }

            Console.WriteLine("Employee added successfully!");
        }

        public void AddEmployee()
        {
            Employee employee = new Employee();

            //gets input values for new employee
            Console.Write("Enter Employee Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Employee Department: ");
            string department = Console.ReadLine();
            Console.WriteLine("Enter employee wage");
            int hourly = int.Parse(Console.ReadLine());


            if (name == null || department == null || hourly < 0)
            {
                Console.WriteLine("One or more values not specified, please try again");
                return;
            }

            InsertHelper(name, department, hourly);
        }

        public void AddEmployee(string name, string department, int hourly)
        {
            InsertHelper(name, department, hourly);
        }

        public void ViewEmployees()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string viewQuery = "SELECT * from EMPLOYEES";
                using (SQLiteCommand command = new SQLiteCommand(viewQuery, connection))
                {

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Output employee details to the console
                            Console.WriteLine($"EmployeeId: {reader["EmployeeId"]}, Name: {reader["Name"]}, Department: {reader["Department"]}");
                        }
                    }
                }
            }
        }


        //given an employee id, retrieve and update whichever values the user wants
        public void UpdateEmployee(int empId)
        {
            Employee updateEmploy = new Employee();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string findQuery = "SELECT Name, Department FROM Employees WHERE EmployeeId = @EmployeeId";
                using (SQLiteCommand command = new SQLiteCommand(findQuery, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", empId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            updateEmploy.Name = reader["Name"].ToString();
                            updateEmploy.Department = reader["Department"].ToString();

                            Console.WriteLine($"Current employee Name is: {updateEmploy.Name}");
                            Console.Write("Enter new employee Name: ");
                            updateEmploy.Name = Console.ReadLine();

                            Console.WriteLine($"Current department is: {updateEmploy.Department}");
                            Console.Write("Enter new employee Department: ");
                            updateEmploy.Department = Console.ReadLine();

                            string updateQuery = "UPDATE Employees SET Name = @UpdatedName, Department = @UpdatedDepartment WHERE EmployeeId = @EmployeeId";
                            using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@UpdatedName", updateEmploy.Name);
                                updateCommand.Parameters.AddWithValue("@UpdatedDepartment", updateEmploy.Department);
                                updateCommand.Parameters.AddWithValue("@EmployeeId", empId);

                                int rowsAffected = updateCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    Console.WriteLine("Employee details updated successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Employee not found or update failed.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No employee found with ID: {empId}, cancelling update");
                        }
                    }
                }
            }
        }


        public void RemoveEmployee(int employeeId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Employees WHERE EmployeeId = @EmployeeId";
                using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DropTable()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                //create employee table
                string dropQuery = "DROP TABLE IF EXISTS Employees";
                SQLiteCommand command = new SQLiteCommand(dropQuery, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
