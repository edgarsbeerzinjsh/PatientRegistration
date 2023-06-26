using NETApi.Core.IServices;
using NETApi.Core.Models;
using NETApi.Data;

namespace NETApi.Services
{
    public class DbService<T> : IDbService<T> where T : Entity
    {
        protected readonly INetApiDbContext _dbContext;
        public DbService(INetApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public T Read(int id)
        {
            return _dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public T Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }
        public void RemoveAll()
        {
            _dbContext.Set<T>().RemoveRange(_dbContext.Set<T>());
            _dbContext.SaveChanges();
        }
    }
}
