using NETApi.Core.Models;

namespace NETApi.Core.IServices
{
    public interface IDbService<T> where T : Entity
    {
        T Create(T entity);
        T Read(int id);
        T Update(T entity);
        void Remove(T entity);
        List<T> GetAll();
        void RemoveAll();
    }
}
