using MongoDB.Driver;
using ClassLibrary1; 

public class MongoRepository<T> : IRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(string connectionString, string databaseName, string collectionName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    // Інші методи потребують рефлексії або загального базового класу (наприклад IEntity з Id), 
    // оскільки MongoDB використовує фільтри за полями для пошуку/оновлення.
    public Task<T> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task UpdateAsync(T entity) => throw new NotImplementedException();
    public Task DeleteAsync(int id) => throw new NotImplementedException();
}