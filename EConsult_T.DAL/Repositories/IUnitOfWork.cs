using EConsult_T.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace EConsult_T.DAL.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<User> UserRepository { get; }

        void Save();
        Task SaveAsync();
    }
}
