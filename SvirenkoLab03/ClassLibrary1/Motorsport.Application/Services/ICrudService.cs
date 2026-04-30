public interface ICrudService<T> where T : class
{
    Task<IEnumerable<T>> GetAllEntitiesAsync();
    Task CreateEntityAsync(T entity);
}