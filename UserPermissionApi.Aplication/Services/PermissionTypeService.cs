
using UserPermissionApi.Aplication.Interfaces;
using UserPermissionsApi.Domain.Entities;
using UserPermissionsApi.Domain.Interfaces;

namespace UserPermissionApi.Application.Services
{
    public class PermissionTypeService : IPermissionTypeService
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;

        public PermissionTypeService(IPermissionTypeRepository permissionTypeRepository)
        {
            _permissionTypeRepository = permissionTypeRepository;
        }

        public async Task<IEnumerable<PermissionType>> GetAllAsync()
        {
            return await _permissionTypeRepository.GetAllAsync();
        }
    }
}
