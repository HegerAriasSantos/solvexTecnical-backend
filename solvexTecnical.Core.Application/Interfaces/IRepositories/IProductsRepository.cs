using solvexTecnical.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace solvexTecnical.Core.Application.Interfaces.IRespositories
{
    public interface IProductsRepository : ICommonRepository<Products>
    {
        Task<Products> GetByIdForListAsync(int id);
    }
}
