using System.Collections.Generic;
using System.Threading.Tasks;
using UserPermissionsApi.Domain.Entities;

namespace UserPermissionsApi.Domain.Interfaces
{
    public interface IPermissionTypeRepository
    {
        Task<IEnumerable<PermissionType>> GetAllAsync();       
    }
}
