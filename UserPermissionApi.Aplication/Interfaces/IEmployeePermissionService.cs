using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserPermissionsApi.Domain.Entities;

namespace UserPermissionApi.Aplication.Interfaces
{
    public interface IEmployeePermissionService
    {
        Task<IEnumerable<EmployeePermissions>> GetAllAsync();
        Task<EmployeePermissions> GetByEmployeeIdAndPermissionTypeIdAsync(int employeeId, int permissionTypeId);
        Task AddAsync(EmployeePermissions employeePermission);
        Task UpdateAsync(EmployeePermissions employeePermission);
        Task DeleteAsync(int employeePermissionId);
    }
}
