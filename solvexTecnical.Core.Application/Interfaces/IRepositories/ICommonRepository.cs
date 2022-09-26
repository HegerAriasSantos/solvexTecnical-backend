using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace solvexTecnical.Core.Application.Interfaces.IRespositories
{
    public interface ICommonRepository<T> where T : class
    {
        Task<T> AddAsync(T t);
        Task DeleteAsync(T t, int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllWithIncludesAsync(List<string> props);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdWithIncludeAsync(int id, List<string> props, List<string> colls);
        Task UpdateAsync(T t, int id);
    }
}
