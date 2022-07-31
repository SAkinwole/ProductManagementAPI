using Microsoft.Extensions.Logging;
using ProductManagementAPI.DataContext;
using ProductManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.GenericRepository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class, new()
    {
        protected AppDbContext _db;
        protected ILogger _logger;
        public GenericRepo(AppDbContext db, ILogger<GenericRepo<T>> logger)
        {
            _logger = logger;
            _db = db;
        }
        

        public async Task<int> CreateAsync(T entity, bool isSave = true)
        {
            if(entity == null)
            {
                _logger.LogError("Entity cannot be null");
                return 3;
            }

            _db.Set<T>().Add(entity);
            if (isSave)
            {
                return await SaveChangesToDbAsync();
            }

            return 1;
        }

        public async Task<int> DeleteAsync(int id, bool isSave = true)
        {
            T entity = await GetByIdAsync(id);
            if(entity == null)
            {
                _logger.LogError("Entity cannot be null");
                return -1;
            }
            _db.Set<T>().Remove(entity);

            if (isSave)
            {
                await SaveChangesToDbAsync();
            }

            return 1;

        }

        public async Task<bool> EntityExistsAsync(int id)
        {
            T entityFound = await _db.Set<T>().FindAsync(id);
            if(entityFound == null)
            {
                return false;
            }
            return true;
        }

        public IQueryable<T> GetAll()
        {
            return  _db.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _db.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<int> SaveChangesToDbAsync()
        {
            _logger.LogInformation("Save process started");
            int saveResult;

            try
            {
                int tempResult = await _db.SaveChangesAsync();
                saveResult = 1;
            }
            catch(Exception ex)
            {
                _logger.LogError("Cannot Save");
                saveResult = -1;
                throw;
            }
            return saveResult;
        }

        public async Task<int> UpdateAsync(T entity, bool isSave = true)
        {
            _db.Set<T>().Update(entity);
            if (isSave)
            {
                await SaveChangesToDbAsync();
            }

            return 1;
        }

       
    }
}
