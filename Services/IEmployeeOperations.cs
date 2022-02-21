using System.Collections.Generic;
using Dto;

namespace Services
{
    interface IEmployeeOperations
    {
        List<Employee> GetEmployees();

        void AddEmployees(List<Employee> employees);

        void UpdateEmployees(List<Employee> employees);

        void DeleteEmployees(List<long> employeeIds);
    }
}
