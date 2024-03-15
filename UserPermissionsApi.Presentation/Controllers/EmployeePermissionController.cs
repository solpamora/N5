using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UserPermissionApi.Aplication.Interfaces;
using UserPermissionApi.Aplication.Services;
using UserPermissionsApi.Domain.Entities;
using UserPermissionsApi.Presentation.Controllers;

namespace UserPermissionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeePermissionController : ControllerBase
    {
        private readonly IEmployeePermissionService _employeePermissionService;
        private readonly ProducerService _producerService;
        private readonly ILogger<EmployeePermissionController> _logger;

        public EmployeePermissionController(IEmployeePermissionService employeePermissionService, ILogger<EmployeePermissionController> logger, ProducerService producerService)
        {
            _employeePermissionService = employeePermissionService;
            _producerService = producerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeePermissions>>> GetAllAsync()
        {
            var employeePermissions = await _employeePermissionService.GetAllAsync();
            var message = JsonSerializer.Serialize(employeePermissions);
            _logger.LogInformation(message);
            await _producerService.ProduceAsync("permissions", message);
            return Ok(employeePermissions);
        }

        [HttpGet("{employeeId}/{permissionTypeId}")]
        public async Task<ActionResult<EmployeePermissions>> GetByEmployeeIdAndPermissionTypeIdAsync(int employeeId, int permissionTypeId)
        {
            var employeePermission = await _employeePermissionService.GetByEmployeeIdAndPermissionTypeIdAsync(employeeId, permissionTypeId);
            if (employeePermission == null)
            {
                _logger.LogWarning("No se ha encontrado el Empleado-Permiso buscado");
                return NotFound();
            }
            var message = JsonSerializer.Serialize(employeePermission);
            _logger.LogInformation(message);
            await _producerService.ProduceAsync("permissions", message);
            return Ok(employeePermission);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] EmployeePermissions employeePermission)
        {
            await _employeePermissionService.AddAsync(employeePermission);
            var message = JsonSerializer.Serialize(employeePermission);
            _logger.LogInformation(message);
            await _producerService.ProduceAsync("permissions", message);
            return CreatedAtAction(nameof(GetByEmployeeIdAndPermissionTypeIdAsync), new { employeeId = employeePermission.EmployeeId, permissionTypeId = employeePermission.PermissionTypeId }, employeePermission);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] EmployeePermissions employeePermission)
        {
            if (id != employeePermission.EmployeeId)
            {
                _logger.LogWarning("No coinciden los registros enviados");
                return BadRequest();
            }

            await _employeePermissionService.UpdateAsync(employeePermission);
            var message = JsonSerializer.Serialize(employeePermission);
            await _producerService.ProduceAsync("permissions", message);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _employeePermissionService.DeleteAsync(id);
            _logger.LogInformation("Registro Eliminado ");
            return NoContent();
        }
    }
}
