using EConsult_T.DAL.EF;
using EConsult_T.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EConsult_T.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {

        private readonly EConsultDbContext dbContext;
        private readonly DbSet<User> dbSet;

        public UserRepository(EConsultDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Users;
        }

        public void Add(User entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<User> entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public void Delete(User entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<User> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public User Find(Func<User, bool> predicate)
        {
            return dbSet.FirstOrDefault(predicate);
        }

        public IEnumerable<User> FindAll(Func<User, bool> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }

        public async Task<User> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await dbSet.FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<User> GetAll()
        {
            return dbSet.ToList();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public void Update(User entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
