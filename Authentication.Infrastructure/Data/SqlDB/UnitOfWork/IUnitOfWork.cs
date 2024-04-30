using Authentication.Infrastructure.Data.SqlDB.Entities;
using Authentication.Infrastructure.Data.SqlDB.Repository;

namespace Authentication.Infrastructure.Data.SqlDB.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<UserEntity> UserRepository { get; }
        void Save();
    }
}
