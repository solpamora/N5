using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserPermissionsApi.Domain.Entities;
using UserPermissionsApi.Domain.Interfaces;
using UserPermissionsApi.Infrastructure.Data;

namespace UserPermissionsApi.Infrastructure.Repositories
{
    public class PermissionTypeRepository : IPermissionTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PermissionType>> GetAllAsync()
        {
            return await _context.PermissionTypes.ToListAsync();
        }
    }
}
