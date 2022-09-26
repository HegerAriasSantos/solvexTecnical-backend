using solvexTecnical.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace solvexTecnical.Core.Application.Interfaces.IRespositories
{
    public interface IProductsBrandsRepository : ICommonRepository<ProductsBrands>
    {
        Task<ProductsBrands> GetByIdForListAsync(int id);
    }
}
