using Microsoft.EntityFrameworkCore;
using solvexTecnical.Core.Application.Interfaces.IRespositories;
using solvexTecnical.Core.Domain.Entities;
using solvexTecnical.Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace solvexTecnical.Infraestructure.Persistence.Repositories
{
    public class FinalProductsRepository : CommonRepository<FinalProducts>, IFinalProductsRepository
    {
        private readonly ApplicationDbContext _context;
        public FinalProductsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<FinalProducts> SetPrice(decimal price, ProductsBrands productsBrands, SuperMarket superMarket)
        {
            var entry = await _context.Set<FinalProducts>().FindAsync(productsBrands.Id, superMarket.Id);
            _context.Entry(entry).CurrentValues.SetValues(new FinalProducts { Price = price });
            await _context.SaveChangesAsync();
            return entry;
        }
        public async Task<FinalProducts> GetOneForListById(int id)
        {
                return await _context.Set<FinalProducts>().FindAsync(id);
        }


    }
}
