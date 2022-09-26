using solvexTecnical.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace solvexTecnical.Core.Domain.Entities
{
    public class FinalProducts : CommonEntity
    {
        public int ProductId { get; set; }
        public int BrandId { get; set; }
        public decimal Price { get; set; }
        
        public Products Product { get; set; }
        public ProductsBrands Brand { get; set; }
        public ICollection<ShoppingListProducts> ShoppingListProducts { get; set; }

    }
}
