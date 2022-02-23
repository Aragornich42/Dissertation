using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto;

namespace Services
{
    public class EmployeeOperations : IEmployeeOperations
    {
        readonly DataConnector _db = new DataConnector();

        public List<Employee> GetEmployees()
        {
            var employees = _db.GetEmployees();

            foreach (var employee in employees)
            {
                employee.Relative = _db.GetRelative(employee.Id);
                employee.Educations = _db.GetEducations(employee.Id);
                employee.Professions = _db.GetProfessions(employee.Id);
                employee.ForeignPassports = _db.GetForeignPassports(employee.Id);
                employee.Languages = _db.GetLanguages(employee.Id);
            }

            return employees;
        }

        public void AddEmployees(List<Employee> employees)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployees(List<Employee> employees)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployees(List<long> employeeIds)
        {
            throw new NotImplementedException();
        }
    }
}
