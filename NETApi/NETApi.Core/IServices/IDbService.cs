using NETApi.Core.Models;

namespace NETApi.Core.IServices
{
    public interface IDbService
    {
        T Create<T>(T entity) where T : Entity;
        T Read<T>(T entity) where T : Entity;
        T Update<T>(T entity) where T : Entity;
        void Remove<T>(T entity) where T : Entity;
        List<T> GetAll<T>() where T : Entity;
        void RemoveAll<T>() where T : Entity;
    }
}
