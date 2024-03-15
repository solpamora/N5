using UserPermissionApi.Application.Interfaces;
using UserPermissionsApi.Domain.Entities;
using UserPermissionsApi.Domain.Interfaces;

namespace UserPermissionApi.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }
    }
}
