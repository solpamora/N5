using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserPermissionsApi.Domain.Entities;
using UserPermissionsApi.Domain.Interfaces;
using UserPermissionsApi.Infrastructure.Data;
using UserPermissionsApi.Infrastructure.Repositories.Interfaces;

namespace UserPermissionsApi.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.Include(e=>e.EmployeePermissions).ToListAsync();
        }
    }

}
