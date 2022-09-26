using solvexTecnical.Core.Application.Interfaces.IRespositories;
using solvexTecnical.Core.Domain.Entities;
using solvexTecnical.Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace solvexTecnical.Infraestructure.Persistence.Repositories
{
    public class SuperMarketRepository : CommonRepository<SuperMarket>, ISuperMarketRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public SuperMarketRepository(ApplicationDbContext context) : base(context)
        {
            
        _dbContext = context;
        } 
    }
}
