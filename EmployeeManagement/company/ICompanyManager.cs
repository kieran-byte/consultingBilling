using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.company
{
    internal interface ICompanyManager
    {
        void InitializeDatabase();
        void InsertHelper(string companyName, string industry);
        void AddCompany();
        void AddCompany(string companyName, string industry);
        void ViewCompanies();
        void UpdateCompany(int companyId);
        void RemoveCompany(int companyId);
        void DropCompanyTable();
    }
}

