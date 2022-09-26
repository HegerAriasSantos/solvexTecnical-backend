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
    public class ShoppingListRepository : CommonRepository<ShoppingList>, IShoppingListRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ShoppingListRepository(ApplicationDbContext context) : base(context)
        {
            
        _dbContext = context;
        }

        public async Task<List<ShoppingList>> GetAllShoppingListByUser(int userId)
        {
            return await _dbContext.Set<ShoppingList>()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}
