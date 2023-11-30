public interface IEmployeeDataAccess
{
    void InitializeDatabase();
    void InsertHelper(string name, string department);
    void AddEmployee();
    void AddEmployee(string name, string department);
    void ViewEmployees();
    void UpdateEmployee(int empId);
    void RemoveEmployee(int employeeId);
    void DropTable();

   
}