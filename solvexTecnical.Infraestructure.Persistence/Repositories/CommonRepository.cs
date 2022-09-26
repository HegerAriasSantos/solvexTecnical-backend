using Microsoft.EntityFrameworkCore;
using solvexTecnical.Core.Application.Interfaces.IRespositories;
using solvexTecnical.Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solvexTecnical.Infraestructure.Persistence.Repositories
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public CommonRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;
        public virtual async Task<T> AddAsync(T t)
        {
            await _dbContext.AddAsync(t);
            await _dbContext.SaveChangesAsync();
            return t;
        }

        public async Task DeleteAsync(T t, int id)
        {
            if (typeof(T).GetProperty("IsDeleted") != null)
            {
                T entry = await _dbContext.Set<T>().FindAsync(id);
                entry.GetType().GetProperty("IsDeleted").SetValue(t, 1);
                entry.GetType().GetProperty("DeletedDate").SetValue(t, DateTime.Now);
                _dbContext.Entry(entry).CurrentValues.SetValues(t);
                await _dbContext.SaveChangesAsync();
                return; 
            }
            _dbContext.Set<T>().Remove(t);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            var entities =  await _dbContext.Set<T>()
                .AsNoTracking()
                .ToListAsync();
            
            if (typeof(T).GetProperty("IsDeleted") != null)
            {
                entities = entities.FindAll(x => (int)x.GetType().GetProperty("IsDeleted").GetValue(x) == 0);
            }
            return entities;
            
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity != null && typeof(T).GetProperty("IsDeleted") != null && (int)entity.GetType().GetProperty("IsDeleted").GetValue(entity) == 1)
                return null;
            return entity;
        }

        public virtual async Task<List<T>> GetAllWithIncludesAsync(List<string> props)
        {
             var query = _dbContext.Set<T>().AsNoTracking().AsQueryable();


            foreach (string prop in props)
            {
                query = query.Include(prop);
            }

            var entities =  await query.ToListAsync();
            if (typeof(T).GetProperty("IsDeleted") != null)
            {
                entities = entities.FindAll(x => (int)x.GetType().GetProperty("IsDeleted").GetValue(x) == 0);
            }
            return entities;
        }

        public virtual async Task<T> GetByIdWithIncludeAsync(int id, List<string> props, List<string> colls)
        {
            var query = await _dbContext.Set<T>().FindAsync(id);

            if (query.GetType().GetProperty("IsDeleted") != null && (int)query.GetType().GetProperty("IsDeleted").GetValue(query) == 1)
                return null;


            foreach (string prop in props)
            {
                _dbContext.Entry(query).Reference(prop).Load();
            }

            foreach (string coll in colls)
            {
                _dbContext.Entry(query).Collection(coll).Load();
            }

            return query;
        }

        public async Task UpdateAsync(T t, int id)
        {
            T entry = await _dbContext.Set<T>().FindAsync(id);
            _dbContext.Entry(entry).CurrentValues.SetValues(t);
            await _dbContext.SaveChangesAsync();
        }
    }
}
