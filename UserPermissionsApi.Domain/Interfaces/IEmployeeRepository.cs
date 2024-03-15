using System.Collections.Generic;
using System.Threading.Tasks;
using UserPermissionsApi.Domain.Entities;

namespace UserPermissionsApi.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();      
    }
}
