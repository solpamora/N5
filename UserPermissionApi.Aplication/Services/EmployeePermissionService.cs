using UserPermissionApi.Aplication.Interfaces;
using UserPermissionsApi.Domain.Entities;
using UserPermissionsApi.Domain.Interfaces;

namespace UserPermissionApi.Aplication.Services
{
    public class EmployeePermissionService : IEmployeePermissionService
    {
        private readonly IEmployeePermissionRepository _employeePermissionRepository;

        public EmployeePermissionService(IEmployeePermissionRepository employeePermissionRepository)
        {
            _employeePermissionRepository = employeePermissionRepository;
        }

        public async Task<IEnumerable<EmployeePermissions>> GetAllAsync()
        {
            return await _employeePermissionRepository.GetAllAsync();
        }

        public async Task<EmployeePermissions> GetByEmployeeIdAndPermissionTypeIdAsync(int employeeId, int permissionTypeId)
        {
            return await _employeePermissionRepository.GetByEmployeeIdAndPermissionTypeIdAsync(employeeId, permissionTypeId);
        }

        public async Task AddAsync(EmployeePermissions employeePermission)
        {
            await _employeePermissionRepository.AddAsync(employeePermission);
        }

        public async Task UpdateAsync(EmployeePermissions employeePermission)
        {
            await _employeePermissionRepository.UpdateAsync(employeePermission);
        }

        public async Task DeleteAsync(int employeePermissionId)
        {
            await _employeePermissionRepository.DeleteAsync(employeePermissionId);
        }
    }
}
