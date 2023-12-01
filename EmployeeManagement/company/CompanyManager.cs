using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeManagement.company
{
    internal class CompanyManager : ICompanyManager
    {
        private string connectionString;

        public CompanyManager(string dbPath)
        {
            connectionString = dbPath;
        }

        public void AddCompany()
        {
            //gets input values for new employee
            Console.Write("Enter Company Name: ");
            string companyName = Console.ReadLine();
            Console.Write("Enter Company Industry: ");
            string industry = Console.ReadLine();


            if (companyName == null || industry == null)
            {
                Console.WriteLine("One or more values not specified, please try again");
                return;
            }

            InsertHelper(companyName, industry);
        }

        public void AddCompany(string companyName, string industry)
        {
            InsertHelper(companyName, industry);
        }

        public void DropCompanyTable()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                //drop company table
                string dropQuery = "DROP TABLE IF EXISTS Companies";
                SQLiteCommand command = new SQLiteCommand(dropQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public void InitializeDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // Open the connection and execute SQL commands
                connection.Open();

                // Your initialization SQL commands here
                string createTableQuery = "CREATE TABLE IF NOT EXISTS Companies (CompanyId INTEGER PRIMARY KEY, CompanyName NVARCHAR(100), Industry NVARCHAR(100))";
                SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();

                // Other initialization commands if required
            }
        }

        public void InsertCompany(string companyName, string industry)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Companies (CompanyName, Industry) VALUES (@CompanyName, @Industry)";
                SQLiteCommand command = new SQLiteCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@CompanyName", companyName);
                command.Parameters.AddWithValue("@Industry", industry);
                command.ExecuteNonQuery();
            }
        }

        public void InsertHelper(string companyName, string industry)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Companies (CompanyName, Industry) VALUES (@CompanyName, @Industry)";
                SQLiteCommand command = new SQLiteCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@CompanyName", companyName);
                command.Parameters.AddWithValue("@Industry", industry);
                command.ExecuteNonQuery();
            }

            Console.WriteLine("Company added successfully!");
        }

        public void RemoveCompany(int companyId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Employees WHERE CompanyId = @CompanyId";
                using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", companyId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCompany(int companyId)
        {
            throw new NotImplementedException();
        }

        public void ViewCompanies()
        {
            // Implement code to view companies from the database
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string viewQuery = "SELECT * FROM Companies";
                SQLiteCommand command = new SQLiteCommand(viewQuery, connection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Company ID: {reader["CompanyId"]}, Name: {reader["CompanyName"]}, Industry: {reader["Industry"]}");
                }

                reader.Close();
            }
        }
    }
}
