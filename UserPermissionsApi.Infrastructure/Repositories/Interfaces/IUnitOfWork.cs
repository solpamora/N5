using UserPermissionsApi.Infrastructure.Data;

namespace UserPermissionsApi.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}
