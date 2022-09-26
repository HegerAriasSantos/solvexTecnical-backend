using System;
using System.Collections.Generic;
using System.Text;

namespace solvexTecnical.Core.Application.DTOs
{
    public class ShoppingListDTO
    {
        public int Id { get; set; }
        public int SuperMarketId { get; set; }
        public string SuperMarketName { set; get; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public UserDTO User { get; set; }
        public List<int> ProductsIds { get; set; }
        public List<FinalProductDTO> Products { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
