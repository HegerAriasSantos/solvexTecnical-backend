using solvexTecnical.Core.Application.Interfaces.IRespositories;
using solvexTecnical.Core.Domain.Entities;
using solvexTecnical.Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace solvexTecnical.Infraestructure.Persistence.Repositories
{
    public class ProductsBrandsRepository : CommonRepository<ProductsBrands>, IProductsBrandsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductsBrandsRepository(ApplicationDbContext context) : base(context)
        {
            
        _dbContext = context;
        }
        public async Task<ProductsBrands> GetByIdForListAsync(int id)
        {
            return await _dbContext.Set<ProductsBrands>().FindAsync(id);
        }
    }
}
