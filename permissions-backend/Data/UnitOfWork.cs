using permissions_backend.Models.Interface;
using permissions_backend.Models.Repository;

namespace permissions_backend.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IPermissionRepository Permissions { get; }
        IPermissionTypeRepository PermissionTypes { get; }
        int Complete();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Permissions = new PermissionRepository(_context);
            PermissionTypes = new PermissionTypeRepository(_context);
        }

        public IPermissionRepository Permissions { get; private set; }
        public IPermissionTypeRepository PermissionTypes { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}