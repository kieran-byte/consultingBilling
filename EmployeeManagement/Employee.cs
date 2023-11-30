using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    
        class Employee
        {
            public int? EmployeeId { get; set; }
            public string? Name { get; set; }
            public string? Department { get; set; }
            

            public Employee() { }
            

            public Employee(int employeeId, string name, string department)
            {
                EmployeeId = employeeId;
                Name = name;
                Department = department;
            }

            public override string ToString()
            {
                return $"Employee ID: {EmployeeId}, Name: {Name}, Department: {Department}";
            }
        }
    
}
