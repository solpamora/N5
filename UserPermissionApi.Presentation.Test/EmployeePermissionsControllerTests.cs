using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UserPermissionApi.Aplication.Interfaces;
using UserPermissionApi.Aplication.Services;
using UserPermissionApi.Controllers;
using UserPermissionsApi.Domain.Entities;
using Xunit;

namespace UserPermissionApi.Tests.Controllers
{
    public class EmployeePermissionControllerTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsOkResultWithEmployeePermissions()
        {
            // Arrange
            var mockService = new Mock<IEmployeePermissionService>();
            mockService.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<EmployeePermissions> { new EmployeePermissions { EmployeePermissionId = 1, EmployeeId = 1, PermissionTypeId = 1 } });

            var mockLogger = new Mock<ILogger<EmployeePermissionController>>();
            var mockProduceService = new Mock<ProducerService>();

            var controller = new EmployeePermissionController(mockService.Object, mockLogger.Object, mockProduceService.Object);

            // Act
            var result = await controller.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var employeePermissions = Assert.IsAssignableFrom<IEnumerable<EmployeePermissions>>(okResult.Value);
            Assert.Single(employeePermissions); // Aseguramos que se devuelva una sola entidad
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsOkResultWithEmployeePermission()
        {
            // Arrange
            int employeeId = 1;
            int employeePermissionID=1;
            var mockService = new Mock<IEmployeePermissionService>();
            mockService.Setup(repo => repo.GetByEmployeeIdAndPermissionTypeIdAsync(employeeId,employeePermissionID))
                .ReturnsAsync(new EmployeePermissions { EmployeePermissionId = 1, EmployeeId = 1, PermissionTypeId = 1 });

            var mockLogger = new Mock<ILogger<EmployeePermissionController>>();
            var mockProduceService = new Mock<ProducerService>();


            var controller = new EmployeePermissionController(mockService.Object, mockLogger.Object, mockProduceService.Object);

            // Act
            var result = await controller.GetByEmployeeIdAndPermissionTypeIdAsync(employeeId,employeePermissionID);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var employeePermission = Assert.IsType<EmployeePermissions>(okResult.Value);
            Assert.Equal(employeePermissionID, employeePermission.EmployeePermissionId);
        }

        [Fact]
        public async Task AddAsync_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockService = new Mock<IEmployeePermissionService>();
            var mockLogger = new Mock<ILogger<EmployeePermissionController>>();
            var mockProduceService = new Mock<ProducerService>();

            var controller = new EmployeePermissionController(mockService.Object, mockLogger.Object, mockProduceService.Object);
            var employeePermission = new EmployeePermissions { EmployeePermissionId = 1, EmployeeId = 1, PermissionTypeId = 1 };

            // Act
            var result = await controller.AddAsync(employeePermission);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(controller.AddAsync), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsNoContent()
        {
            // Arrange
            var mockService = new Mock<IEmployeePermissionService>();
            var mockLogger = new Mock<ILogger<EmployeePermissionController>>();
            var mockProduceService = new Mock<ProducerService>();

            var controller = new EmployeePermissionController(mockService.Object, mockLogger.Object, mockProduceService.Object);
            var employeePermission = new EmployeePermissions { EmployeePermissionId = 1, EmployeeId = 1, PermissionTypeId = 1 };

            // Act
            var result = await controller.UpdateAsync(1, employeePermission);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsNoContent()
        {
            // Arrange
            var mockService = new Mock<IEmployeePermissionService>();
            var mockLogger = new Mock<ILogger<EmployeePermissionController>>();
            var mockProduceService = new Mock<ProducerService>();

            var controller = new EmployeePermissionController(mockService.Object, mockLogger.Object, mockProduceService.Object);

            // Act
            var result = await controller.DeleteAsync(1);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }       
    }
}
