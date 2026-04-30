public class CrudService<T> : ICrudService<T> where T : class
{
    private readonly IRepository<T> _repository;

    public CrudService(IRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<T>> GetAllEntitiesAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task CreateEntityAsync(T entity)
    {
        await _repository.AddAsync(entity);
    }
}