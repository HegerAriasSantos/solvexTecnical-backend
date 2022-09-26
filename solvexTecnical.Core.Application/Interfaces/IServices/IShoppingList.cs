using solvexTecnical.Core.Application.DTOs;
using solvexTecnical.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace solvexTecnical.Core.Application.Interfaces.IServicies
{
    public interface IShoppingList : ICommonService<ShoppingList, ShoppingListDTO>
    {
        Task<ShoppingListDTO> AddShoppingList(ShoppingListDTO shoppingList);
        Task<List<ShoppingListDTO>> GetAllByUser(int userId);
        Task<List<ShoppingListDTO>> GetAll();
    }
}
