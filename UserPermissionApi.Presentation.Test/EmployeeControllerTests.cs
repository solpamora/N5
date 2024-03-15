using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UserPermissionApi.Aplication.Services;
using UserPermissionApi.Application.Interfaces;

using UserPermissionsApi.Domain.Entities;
using UserPermissionsApi.Presentation.Controllers;
using Xunit;

namespace UserPermissionApi.Tests.Controllers
{
    public class EmployeeControllerTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsOkResultWithEmployees()
        {
            // Arrange
            var mockService = new Mock<IEmployeeService>();
            mockService.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Employee> { new Employee { EmployeeId = 1, FirstName = "Employee 1" } });

            var mockLogger = new Mock<ILogger<EmployeeController>>();
            var mockProduceService = new Mock<ProducerService>();

            var controller = new EmployeeController(mockService.Object, mockLogger.Object,mockProduceService.Object);

            // Act
            var result = await controller.GetEmployees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var employees = Assert.IsAssignableFrom<IEnumerable<Employee>>(okResult.Value);
            Assert.Single(employees); // Aseguramos que se devuelva una sola entidad
        }

        
    }
}
