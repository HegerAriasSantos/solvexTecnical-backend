using Microsoft.EntityFrameworkCore;
using solvexTecnical.Core.Application.Interfaces.IRespositories;
using solvexTecnical.Core.Domain.Entities;
using solvexTecnical.Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solvexTecnical.Infraestructure.Persistence.Repositories
{
    public class ShoppingListProductsRepository : CommonRepository<ShoppingListProducts>, IShoppingListProductsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ShoppingListProductsRepository(ApplicationDbContext context) : base(context)
        {
            
        _dbContext = context;
        }

        public async Task<List<ShoppingListProducts>> GetAllByShoppingListId(int shoppingListId)
        {
            return await _dbContext.Set<ShoppingListProducts>()
                .Where(x => x.ShoppingListId == shoppingListId)
                .ToListAsync();
        }
    }
}
