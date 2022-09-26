using solvexTecnical.Core.Application.Interfaces.IRespositories;
using solvexTecnical.Core.Domain.Entities;
using solvexTecnical.Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace solvexTecnical.Infraestructure.Persistence.Repositories
{
    public class ProductsRepository : CommonRepository<Products>, IProductsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductsRepository(ApplicationDbContext context) : base(context)
        {

            _dbContext = context;
        }
        public async Task<Products> GetByIdForListAsync(int id)
        {
            return await _dbContext.Set<Products>().FindAsync(id);
        }
    }
}
