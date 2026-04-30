using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassLibrary1.Repositories
{
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

        // --- НОВЫЕ МЕТОДЫ НИЖЕ ---

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateEntityAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateEntityAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteEntityAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}