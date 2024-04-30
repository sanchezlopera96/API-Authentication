using Authentication.Infrastructure.Data.SqlDB.Context;
using Authentication.Infrastructure.Data.SqlDB.Entities;
using Authentication.Infrastructure.Data.SqlDB.Repository;

namespace Authentication.Infrastructure.Data.SqlDB.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DBContext Context;

        public IGenericRepository<UserEntity> UserRepository { get; }

        public UnitOfWork(DBContext context,
             IGenericRepository<UserEntity> userRepository
            )
        {
            this.Context = context;
            UserRepository = userRepository;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
