using solvexTecnical.Core.Application.DTOs;
using solvexTecnical.Core.Application.Interfaces.IRespositories;
using solvexTecnical.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace solvexTecnical.Core.Application.Interfaces.IServicies
{
    public interface IProductsServices : ICommonService<FinalProducts, FinalProductDTO>
    {
        Task<FinalProductDTO> SetName(FinalProductDTO finalProductDTO);
        Task<FinalProductDTO> AddProduct(FinalProductDTO finalProductDTO);
        Task<List<FinalProductDTO>> GetAllProducts();
        Task<FinalProductDTO> GetOne(int id);
        Task<FinalProductDTO> GetOneForShoppingList(int id);


    }
    
}
