
using UserPermissionsApi.Domain.Entities;

namespace UserPermissionApi.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
    }
}
