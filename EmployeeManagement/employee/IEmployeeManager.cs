namespace EmployeeManagement.employee
{

    public interface IEmployeeDataAccess
    {
        void InitializeDatabase();
        void InsertHelper(string name, string department, int hourly);
        void AddEmployee();
        void AddEmployee(string name, string department, int hourly);
        void ViewEmployees();
        void UpdateEmployee(int empId);
        void RemoveEmployee(int employeeId);
        void DropTable();


    }
}