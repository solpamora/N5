using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security;
using UserPermissionsApi.Domain.Entities;

namespace UserPermissionsApi.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }
        public DbSet<EmployeePermissions> EmployeePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // base.OnModelCreating(modelBuilder);

            // Configuraciones adicionales del modelo, como relaciones y restricciones, pueden ir aquí

            // Configurar relación muchos a muchos entre Employee y PermissionType a través de EmployeePermission
            //modelBuilder.Entity<EmployeePermissions>()
            //    .HasKey(ep => new { ep.EmployeeId, ep.PermissionTypeId });

            //modelBuilder.Entity<EmployeePermissions>()
            //    .HasOne(ep => ep.Employee)
            //    .WithMany(e => e.EmployeePermissions)
            //    .HasForeignKey(ep => ep.EmployeeId);

            //modelBuilder.Entity<EmployeePermissions>()
            //    .HasOne(ep => ep.PermissionType)
            //    .WithMany(pt => pt.EmployeePermissions)
            //    .HasForeignKey(ep => ep.PermissionTypeId);
        }
    }
}
