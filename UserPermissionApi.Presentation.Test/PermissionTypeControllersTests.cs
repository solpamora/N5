
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
    public class PermissionTypeControllerTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsOkResultWithPermissionTypes()
        {
            // Arrange
            var mockService = new Mock<IPermissionTypeService>();
            mockService.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<PermissionType> { new PermissionType { PermissionTypeId = 1, TypeName = "Permission 1" } });

            var mockLogger = new Mock<ILogger<PermissionTypeController>>();
            var mockProduceService = new Mock<ProducerService>();

            var controller = new PermissionTypeController(mockService.Object,mockLogger.Object,mockProduceService.Object);

            // Act
            var result = await controller.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var permissionTypes = Assert.IsAssignableFrom<IEnumerable<PermissionType>>(okResult.Value);
            Assert.Single(permissionTypes); // Aseguramos que se devuelva una sola entidad
        }
    }
}
