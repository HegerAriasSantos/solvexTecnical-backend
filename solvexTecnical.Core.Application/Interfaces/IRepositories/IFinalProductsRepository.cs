using solvexTecnical.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace solvexTecnical.Core.Application.Interfaces.IRespositories
{
    public interface IFinalProductsRepository : ICommonRepository<FinalProducts>
    {
        Task<FinalProducts> SetPrice(decimal price, ProductsBrands productsBrands, SuperMarket superMarket);

        Task<FinalProducts> GetOneForListById(int id);
    }
}
