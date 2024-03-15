using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserPermissionsApi.Domain.Entities;

namespace UserPermissionApi.Aplication.Interfaces
{
    public interface IPermissionTypeService
    {
        Task<IEnumerable<PermissionType>> GetAllAsync();
    }
}
