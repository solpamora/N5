using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using UserPermissionApi.Aplication.Interfaces;
using UserPermissionApi.Aplication.Services;
using UserPermissionsApi.Domain.Entities;
using UserPermissionsApi.Presentation.Controllers;

namespace UserPermissionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionTypeController : ControllerBase
    {
        private readonly IPermissionTypeService _permissionTypeService;
        private readonly ProducerService _producerService;
        private readonly ILogger<PermissionTypeController> _logger;

        public PermissionTypeController(IPermissionTypeService permissionTypeService, ILogger<PermissionTypeController> logger, ProducerService producerService)
        {
            _permissionTypeService = permissionTypeService;
            _producerService = producerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionType>>> GetAllAsync()
        {
            var permissionTypes = await _permissionTypeService.GetAllAsync();
            var message = JsonSerializer.Serialize(permissionTypes);
            _logger.LogInformation(message);
            await _producerService.ProduceAsync("permissions", message);
            return Ok(permissionTypes);
        }
    }
}
