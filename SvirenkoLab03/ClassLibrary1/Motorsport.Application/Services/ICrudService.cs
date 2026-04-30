using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassLibrary1.Repositories 
{
    public interface ICrudService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllEntitiesAsync();
        Task<T> GetByIdAsync(int id);           
        Task CreateEntityAsync(T entity);
        Task UpdateEntityAsync(T entity);      
        Task DeleteEntityAsync(int id);        
    }
}