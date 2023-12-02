using EmployeeManagement.employee;
using EmployeeManagement.company;
using EmployeeManagement.projectManager;


namespace EmployeeManagement
{
    class Program
    {
        static string connectionString = "Data Source=employee.db;";

        private readonly IEmployeeDataAccess _employeeAccess;
        private readonly ICompanyManager _companyManager;
        private readonly IProjectManager _projectManager;

        // Expand to hold other access including company and junction table
        public Program(IEmployeeDataAccess employeeAccess, ICompanyManager companyAccess, IProjectManager projectManager)
        {
            this._employeeAccess = employeeAccess;
            this._companyManager = companyAccess;
            this._projectManager = projectManager;
        }

        static void Main(string[] args)
        {
            bool continueExecution = true;

            IEmployeeDataAccess employeeDataAccess = new EmployeeDataAccess(connectionString);
            ICompanyManager companyAccess = new CompanyManager(connectionString);
            IProjectManager junctionAccess = new ProjectManager(connectionString);

            Program program = new Program(employeeDataAccess, companyAccess, junctionAccess);

            program._employeeAccess.DropTable();
            program._companyManager.DropCompanyTable();
            program._projectManager.DropTable();

            program._companyManager.InitializeDatabase();
            program._employeeAccess.InitializeDatabase();
            program._projectManager.InitializeDatabase();

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

        // Places the initial data to work with in the employee table
        public static void InitialDataset(Program program)
        {
            Random random = new Random();

            program._employeeAccess.AddEmployee("John Doe", "IT Department", random.Next(20, 101));
            program._employeeAccess.AddEmployee("Jane Smith", "HR Department", random.Next(20, 101));
            program._employeeAccess.AddEmployee("Michael Johnson", "Finance Department", random.Next(20, 101));
            program._employeeAccess.AddEmployee("Emily Brown", "Marketing Department", random.Next(20, 101));
            program._employeeAccess.AddEmployee("Robert Williams", "Operations Department", random.Next(20, 101));
            program._employeeAccess.AddEmployee("Sophia Miller", "Sales Department", random.Next(20, 101));
            Console.Write("\n");

            program._companyManager.AddCompany("Company A", "Technology");
            program._companyManager.AddCompany("Company B", "Finance");
            program._companyManager.AddCompany("Company C", "Healthcare");
            program._companyManager.AddCompany("Company D", "Manufacturing");
            Console.Write("\n");

            program._projectManager.AssignEmployeeToCompany(1, 1);


        }
    }


}
