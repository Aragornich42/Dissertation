using System.Collections.Generic;
using Dto;

namespace Services
{
    interface IEmployeeOperations
    {
        /// <summary>
        /// Список ВСЕХ работников
        /// </summary>
        List<Employee> GetEmployees();

        /// <summary>
        /// Добавить новых работников
        /// </summary>
        void AddEmployees(List<Employee> employees);

        /// <summary>
        /// Обновить ТОЛЬКО доп. информацию о работниках по их Id
        /// </summary>
        void UpdateEmployeesAdditional(Dictionary<long, string> additional);

        /// <summary>
        /// Удалить работников по его ID
        /// </summary>
        void DeleteEmployees(List<long> employeeIds);
    }
}
