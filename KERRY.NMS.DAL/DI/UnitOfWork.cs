using KERRY.NMS.DAL.Respository;
using System.Data;

namespace KERRY.NMS.DAL
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
    }

    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection connection = null;
        private readonly IDbTransaction transaction = null;

        public UnitOfWork(IDbConnection connection, IDbTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        private IUserRepository userRepository = null;
        public IUserRepository UserRepository => userRepository = userRepository ?? new UserRepository(connection, transaction);
    }
}
