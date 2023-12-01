namespace EmployeeManagement.projectManager
{
    internal interface IProjectManager
    {

        void InitializeDatabase();

        void AssignEmployeeToCompany(int companyId, int employeeId);

        List<int> GetEmployeesByCompany(int companyId);

        int GetCompanyByEmployee(int employeeId);

        void RemoveEmployeeFromCompany(int companyId, int employeeId);

        void DropTable();


    }
}
