using System.Collections.Generic;
using System.Linq;
using HotChocolate;
using Dto;
using Services;

namespace GraphQL
{
    public class Mutation
    {
        public int AddEmployees(IEnumerable<Employee> input, [Service] IEmployeeOperations service)
        {
            service.AddEmployees(input.ToList());
            return 0;
        }

        public int UpdateEmployees(IEnumerable<PatchDto> input, [Service] IEmployeeOperations service) 
        { 
            service.UpdateEmployeesAdditional(input.ToList());
            return 0;
        }

        public int DeleteEmployees(IEnumerable<long> input, [Service] IEmployeeOperations service)
        {
            service.DeleteEmployees(input.ToList());
            return 0;
        }
    }
}
