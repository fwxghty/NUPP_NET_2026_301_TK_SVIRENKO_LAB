using System.Collections;
using System.Collections.Concurrent;
using System.Text.Json;

public class CrudServiceAsync<T> : ICrudServiceAsync<T> where T : class, IEntity
{
    private ConcurrentDictionary<Guid, T> _collection;
    private readonly SemaphoreSlim _fileLock = new SemaphoreSlim(1, 1);
    public string FilePath { get; }

    public CrudServiceAsync(string filePath)
    {
        FilePath = filePath;
        _collection = new ConcurrentDictionary<Guid, T>();
    }

    public Task CreateAsync(T item)
    {
        _collection.TryAdd(item.Id, item);
        return Task.CompletedTask;
    }

    public Task<T?> ReadAsync(Guid id)
    {
        _collection.TryGetValue(id, out T? item);
        return Task.FromResult(item);
    }

    // Пагінація
    public Task<IEnumerable<T>> ReadAllAsync(int pageNumber, int pageSize)
    {
        var result = _collection.Values
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return Task.FromResult<IEnumerable<T>>(result);
    }

    public Task UpdateAsync(T item)
    {
        _collection.AddOrUpdate(item.Id, item, (key, oldValue) => item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _collection.TryRemove(id, out _);
        return Task.CompletedTask;
    }

    // Асинхронне збереження з блокуванням файлу
    public async Task SaveToFileAsync()
    {
        await _fileLock.WaitAsync();
        try
        {
            using FileStream createStream = File.Create(FilePath);
            await JsonSerializer.SerializeAsync(createStream, _collection.Values);
        }
        finally
        {
            _fileLock.Release();
        }
    }

    // Асинхронне завантаження
    public async Task LoadFromFileAsync()
    {
        if (!File.Exists(FilePath)) return;

        await _fileLock.WaitAsync();
        try
        {
            using FileStream openStream = File.OpenRead(FilePath);
            var items = await JsonSerializer.DeserializeAsync<IEnumerable<T>>(openStream);

            _collection.Clear();
            if (items != null)
            {
                foreach (var item in items)
                {
                    _collection.TryAdd(item.Id, item);
                }
            }
        }
        finally
        {
            _fileLock.Release();
        }
    }

    // Реалізація IEnumerable<T>
    public IEnumerator<T> GetEnumerator() => _collection.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}