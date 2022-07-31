using ProductManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.GenericRepository
{
    public interface IGenericRepo<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<int> CreateAsync(T entity, bool isSave = true);
        Task<int> UpdateAsync(T entity, bool isSave = true);
        Task<int> DeleteAsync(int id, bool isSave = true);
        Task<int> SaveChangesToDbAsync();
        Task<bool> EntityExistsAsync(int id);
        IQueryable<T> GetAll();
    }
}
