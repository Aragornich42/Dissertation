using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Dto;
using Services;

namespace REST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeOperations _employeeOperations;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeOperations employeeOperations)
        {
            _logger = logger;
            _employeeOperations = employeeOperations;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Get() 
        {
            try
            {
                var result = _employeeOperations.GetEmployees();
                _logger.LogInformation("Получение сотрудников успешно");
                return Ok(result);
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.StackTrace;
                _logger.LogError($"Произошла ошибка:    {message}");
                return StatusCode(500, message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] IEnumerable<Employee> employees)
        {
            try
            {
                _employeeOperations.AddEmployees(employees.ToList());
                _logger.LogInformation("Добавление сотрудников успешно");
                return Ok();
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.StackTrace;
                _logger.LogError($"Произошла ошибка:    {message}");
                return StatusCode(500, message);
            }
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Patch([FromBody] IEnumerable<PatchDto> additional)
        {
            try
            {
                _employeeOperations.UpdateEmployeesAdditional(additional.ToList());
                _logger.LogInformation("Изменение сотрудников успешно");
                return Ok();
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.StackTrace;
                _logger.LogError($"Произошла ошибка:    {message}");
                return StatusCode(500, message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromBody] IEnumerable<long> ids)
        {
            try
            {
                _employeeOperations.DeleteEmployees(ids.ToList());
                _logger.LogInformation("Удаление сотрудников успешно");
                return Ok();
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.StackTrace;
                _logger.LogError($"Произошла ошибка:    {message}");
                return StatusCode(500, message);
            }
        }
    }
}
