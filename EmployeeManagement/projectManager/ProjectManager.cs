using System.Data.SQLite;

namespace EmployeeManagement.projectManager
{


    internal class ProjectManager : IProjectManager
    {

        private string connectionString;

        public ProjectManager(string dbPath)
        {
            connectionString = dbPath;
        }

        public void InitializeDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // Open the connection and execute SQL commands
                connection.Open();

                //junction table showing which employee works on which project
                string createTableQuery = @"CREATE TABLE IF NOT EXISTS EmployeeProjects
                        (EmployeeId INTEGER,CompanyId INTEGER,FOREIGN KEY(EmployeeId) REFERENCES Employees(EmployeeId),
                        FOREIGN KEY(CompanyId) REFERENCES Companies(CompanyId)
                            )";
                SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();

                Console.WriteLine("Database created");
            }
        }


        public void AssignEmployeeToCompany(int companyId, int employeeId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE Employees SET CompanyId = @CompanyId WHERE EmployeeId = @EmployeeId";
                using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@CompanyId", companyId);
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.ExecuteNonQuery();


                }

                string insertQuery = "INSERT INTO EmployeeProjects (EmployeeId, CompanyId) VALUES (@EmployeeId, @CompanyId)";
                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {

                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.Parameters.AddWithValue("@CompanyId", companyId);
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
                string dropQuery = "DROP TABLE IF EXISTS EmployeeProjects";
                SQLiteCommand command = new SQLiteCommand(dropQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public int GetCompanyByEmployee(int employeeId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT CompanyId FROM Employee WHERE EmployeeId = @EmployeeId";
                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            return -1; // Indicates no company found for the employee
        }

        public List<int> GetEmployeesByCompany(int companyId)
        {
            List<int> employees = new List<int>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT EmployeeId FROM Employee WHERE CompanyId = @CompanyId";
                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@CompanyId", companyId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(Convert.ToInt32(reader["EmployeeId"]));
                        }
                    }
                }
            }

            return employees;
        }

        public void RemoveEmployeeFromCompany(int companyId, int employeeId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE Employee SET CompanyId = NULL WHERE EmployeeId = @EmployeeId AND CompanyId = @CompanyId";
                using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@CompanyId", companyId);
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
