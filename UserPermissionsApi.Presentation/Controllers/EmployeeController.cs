using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UserPermissionApi.Aplication.Services;
using UserPermissionApi.Application.Interfaces;
using UserPermissionsApi.Domain.Entities;

namespace UserPermissionsApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ProducerService _producerService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController( IEmployeeService employeeService, ILogger<EmployeeController> logger, ProducerService producerService)
        {
            _employeeService = employeeService;
            _logger = logger;
            _producerService = producerService; 
        }

        // GET: api/employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            
            var employees = await _employeeService.GetAllAsync();
            var message = JsonSerializer.Serialize(employees);
            _logger.LogInformation(message);
            await _producerService.ProduceAsync("permissions", message);           

            return Ok(employees);
        }    
    }
}
