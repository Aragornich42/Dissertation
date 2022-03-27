using System.Collections.Generic;
using HotChocolate;
using Dto;
using Services;

namespace GraphQL
{
    public class Query
    {
        public List<Employee> GetEmployees([Service] IEmployeeOperations service) => service.GetEmployees();
    }
}
