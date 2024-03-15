using System.Collections.Generic;
using System.Threading.Tasks;
using UserPermissionsApi.Domain.Entities;

namespace UserPermissionsApi.Domain.Interfaces
{
    public interface IEmployeePermissionRepository
    {
        Task<IEnumerable<EmployeePermissions>> GetAllAsync();
        Task<EmployeePermissions> GetByEmployeeIdAndPermissionTypeIdAsync(int employeeId, int permissionTypeId);
        Task AddAsync(EmployeePermissions employeePermission);
        Task UpdateAsync(EmployeePermissions employeePermission);
        Task DeleteAsync(int employeePermissionId);
    }
}
