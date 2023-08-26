namespace Contracts;
public interface IRepositoryBase<T>
{
    Task<IEnumerable<T>> FindAll();
    Task<T> FindByID(object id);
    Task Create(T entity);
}
