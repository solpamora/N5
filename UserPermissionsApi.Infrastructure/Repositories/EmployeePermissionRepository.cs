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
    public class EmployeePermissionRepository : IEmployeePermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeePermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeePermissions>> GetAllAsync()
        {
            return await _context.EmployeePermissions.ToListAsync();
        }

        public async Task<EmployeePermissions> GetByEmployeeIdAndPermissionTypeIdAsync(int employeeId, int permissionTypeId)
        {
            return await _context.EmployeePermissions
                .FirstOrDefaultAsync(ep => ep.EmployeeId == employeeId && ep.PermissionTypeId == permissionTypeId);
        }

        public async Task AddAsync(EmployeePermissions employeePermission)
        {
            _context.EmployeePermissions.Add(employeePermission);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeePermissions employeePermission)
        {
            _context.Entry(employeePermission).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int employeePermissionId)
        {
            var employeePermission = await _context.EmployeePermissions.FindAsync(employeePermissionId);
            if (employeePermission != null)
            {
                _context.EmployeePermissions.Remove(employeePermission);
                await _context.SaveChangesAsync();
            }
        }
    }
}
